package game_hall

import (
	"math/rand"
	"server/database"
	"server/pb"
	"server/playerManager"
	"time"

	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

type RoomState int
type Side int32

const (
	RoomState_Idle    = 1
	RoomState_BlueBet = 2
	RoomState_RedBet  = 3
	RoomState_Racing  = 4
	RoomState_Result  = 5
)

type RoomPlayer struct {
	playerId int64
	bet_side pb.Side
	bet      int64
	side     pb.Side
}

type RoomInfo struct {
	roomId      int64
	owner       int64
	payMode     pb.PayMode
	round       pb.GameRound
	exit_pay    bool
	password    int64
	players     []*RoomPlayer
	state       RoomState
	round_index int32
}

var roomMap map[int64]*RoomInfo

func init() {
	roomMap = make(map[int64]*RoomInfo)
	createRobotRoom()
}

func getSide() pb.Side {
	betSides := []pb.Side{pb.Side_BLUE, pb.Side_RED}
	rnd := rand.New(rand.NewSource(time.Now().UnixNano()))
	index := rnd.Intn(2)
	return betSides[index]
}

func createRobotRoom() {
	count := make([]int, 10)
	for _ = range count {
		id := getRoomId()
		roomMap[id] = &RoomInfo{}
		roomMap[id].roomId = id
		roomMap[id].owner = 0
		roomMap[id].payMode = pb.PayMode_OWNER
		roomMap[id].round = pb.GameRound_ROUND4
		roomMap[id].exit_pay = false
		roomMap[id].password = 0
		roomMap[id].round_index = 1
		roomMap[id].state = RoomState_Idle
		roomMap[id].players = make([]*RoomPlayer, 0)
		robot := &RoomPlayer{}
		robot.playerId = 0
		robot.bet_side = 0
		robot.bet = 0
		robot.side = getSide()
		roomMap[id].players = append(roomMap[id].players, robot)
	}
}

func getRoomId() int64 {
	rnd := rand.New(rand.NewSource(time.Now().UnixNano()))
	id := rnd.Int63n(1000000)
	_, ok := roomMap[id]
	if ok {
		return getRoomId()
	} else {
		return id
	}
}

func newRoom(playerId int64, payMode pb.PayMode, round pb.GameRound, exit_pay bool, password int64) *RoomInfo {
	id := getRoomId()
	log.Debug("create new room:%v", id)
	room := &RoomInfo{}
	room.roomId = id
	room.owner = playerId
	room.payMode = payMode
	room.round = round
	room.exit_pay = exit_pay
	room.password = password
	room.state = RoomState_Idle
	room.round_index = 0
	room.players = make([]*RoomPlayer, 0)
	player := &RoomPlayer{}
	player.playerId = 0
	player.bet = 0
	player.side = getSide()
	player.bet_side = 0
	room.players = append(room.players, player)
	return room
}

func getRoomById(roomId int64) *RoomInfo {
	roomInfo, ok := roomMap[roomId]
	if ok {
		return roomInfo
	} else {
		return nil
	}
}

func getResults() bool {
	rnd := rand.New(rand.NewSource(time.Now().UnixNano()))
	num1 := rnd.Int63n(1000000)
	num2 := rnd.Int63n(1000000)
	return num1 >= num2
}

func (roomInfo *RoomInfo) startGame() {
	roomInfo.toBet(pb.Side_BLUE)
}

func (roomInfo *RoomInfo) toBet(side pb.Side) {
	log.Debug("toBet: side=%v", side)
	if side == pb.Side_BLUE {
		roomInfo.state = RoomState_BlueBet
	} else if side == pb.Side_RED {
		roomInfo.state = RoomState_RedBet
	}
	for _, player := range roomInfo.players {
		log.Debug("player: id=%v, side=%v", player.playerId, player.side)
		if player.side == side {
			if player.playerId == 0 {
				roomInfo.robotBet(player)
				go roomInfo.betOver()
			} else {
				a := playerManager.GetAgent(player.playerId)
				sendGS2CTurnToBet(a, roomInfo.roomId)
			}
			break
		}
	}
}

func (roomInfo *RoomInfo) playingGame() {
	log.Debug("playing game:")
	roomInfo.state = RoomState_Racing
	result := getResults()
	var win_side pb.Side
	if result {
		win_side = pb.Side_BLUE
	} else {
		win_side = pb.Side_RED
	}
	log.Debug("roomId[%v]: winSide[%v]", roomInfo.roomId, win_side)
	win_players := make([]int64, 0)
	all_bet := float64(0)
	for _, player := range roomInfo.players {
		all_bet += float64(player.bet)
		if player.bet_side == win_side {
			win_players = append(win_players, player.playerId)
		} else if player.playerId != 0 {
			playerInfo, _ := database.GetPlayerInfo(player.playerId)
			if playerInfo != nil {
				a := playerManager.GetAgent(player.playerId)
				sendGS2CGameResults(a, false, playerInfo)
			}
		}
	}
	all_bet = all_bet * 0.9
	ave_coin := int64(all_bet / float64(len(win_players)))
	for _, playerId := range win_players {
		if playerId != 0 {
			playerInfo, _ := database.GetPlayerInfo(playerId)
			if playerInfo != nil {
				playerInfo.Coin += ave_coin
				database.UpdatePlayerInfo(playerInfo) //update database
				a := playerManager.GetAgent(playerId)
				sendGS2CGameResults(a, true, playerInfo)
			}
		}
	}
	log.Debug("win side=%v, win players=%v, win bet=%v", win_side, win_players, ave_coin)
	go roomInfo.overGame()
}

func (roomInfo *RoomInfo) overGame() {
	roomInfo.state = RoomState_Result
	for _, player := range roomInfo.players {
		player.bet = 0
	}
	t := time.NewTimer(time.Second * 3)
	for {
		select {
		case <-t.C:
			roomInfo.toNewRound()
			t.Stop()
		}
		break
	}
}

func (roomInfo *RoomInfo) toNewRound() {
	roomInfo.state = RoomState_Idle
	roomInfo.round_index += 1
	roundMax := int32(0)
	if roomInfo.round == pb.GameRound_ROUND4 {
		roundMax = 4
	} else if roomInfo.round == pb.GameRound_ROUND8 {
		roundMax = 8
	}
	if roomInfo.round_index > roundMax {
		for _, player := range roomInfo.players {
			if player.playerId != 0 {
				a := playerManager.GetAgent(player.playerId)
				sendGS2CGameOver(a)
			}
		}
		delete(roomMap, roomInfo.roomId)
		roomInfo = nil
	} else {
		for _, player := range roomInfo.players {
			if player.playerId != 0 {
				a := playerManager.GetAgent(player.playerId)
				sendGS2CNewRoundStart(a, roomInfo.round_index)
			}
		}
		roomInfo.startGame()
	}
}

func (roomInfo *RoomInfo) playerBet(a gate.Agent, roundIndex int32, betSide pb.Side, betValue int64) {
	playerId := playerManager.GetPlayerId(a)
	if roomInfo.round_index == roundIndex {
		playerInfo, _ := database.GetPlayerInfo(playerId)
		if playerInfo != nil {
			if playerInfo.Coin >= betValue {
				log.Debug("bet:  playerId[%v],betValue[%v],betSide[%v]", playerId, betValue, betSide)
				for _, player := range roomInfo.players {
					if player.playerId == playerId {
						player.bet_side = betSide
						player.bet = betValue
						playerInfo.Coin -= betValue
						err := database.UpdatePlayerInfo(playerInfo) //update database
						if err == nil {
							sendGS2CBetRet(a, pb.GS2CBetRet_Success.Enum())
							roomInfo.broadcastGS2CBetInfo()
							go roomInfo.betOver()
						} else {
							log.Error("can't update [%v] info in database", playerId)
							sendGS2CBetRet(a, pb.GS2CBetRet_Fail.Enum())
						}
						break
					}
				}
			} else {
				sendGS2CBetRet(a, pb.GS2CBetRet_CoinLess.Enum())
			}
		} else {
			log.Error("player[%v] has no info in database", playerId)
			sendGS2CBetRet(a, pb.GS2CBetRet_Fail.Enum())
		}
	} else {
		log.Error("player[%v] bet round index[%v] error: need[%v]", playerId, roundIndex, roomInfo.round_index)
		sendGS2CBetRet(a, pb.GS2CBetRet_IndexError.Enum())
	}
}

func (roomInfo *RoomInfo) betOver() {
	t := time.NewTimer(time.Second * 3)
	for {
		select {
		case <-t.C:
			if roomInfo.state == RoomState_RedBet {
				roomInfo.playingGame()
			} else if roomInfo.state == RoomState_BlueBet {
				roomInfo.toBet(pb.Side_RED)
			}
			t.Stop()
		}
		break
	}
}
