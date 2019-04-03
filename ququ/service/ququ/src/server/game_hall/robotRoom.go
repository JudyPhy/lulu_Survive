package game_hall

import (
	"math/rand"
	"server/pb"
	"time"

	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func enterRobotRoom(a gate.Agent, playerId int64) {
	var roomInfo *RoomInfo
	for _, info := range roomMap {
		if info.state == RoomState_Idle && info.password == 0 && len(info.players) < 2 {
			roomInfo = info
			break
		}
	}
	if roomInfo != nil {
		newPlayer := &RoomPlayer{}
		newPlayer.playerId = playerId
		newPlayer.bet = 0
		robotSide := roomInfo.players[0].side
		if robotSide == pb.Side_BLUE {
			newPlayer.side = pb.Side_RED
		} else {
			newPlayer.side = pb.Side_BLUE
		}
		roomInfo.players = append(roomInfo.players, newPlayer)
		sendGS2CEnterRoomRet(a, roomInfo, pb.GS2CEnterRoomRet_Success.Enum())

		go roomInfo.enterRobotRoomOver()
	} else {
		sendGS2CEnterRoomRet(a, nil, pb.GS2CEnterRoomRet_Fail.Enum())
	}
}

func (roomInfo *RoomInfo) enterRobotRoomOver() {
	t := time.NewTimer(time.Second * 3)
	for {
		select {
		case <-t.C:
			roomInfo.startGame()
			t.Stop()
		}
		break
	}
}

func getRobotBet() int64 {
	betValue := []int64{200, 400, 800, 1600}
	rnd := rand.New(rand.NewSource(time.Now().UnixNano()))
	index := rnd.Intn(4)
	return betValue[index]
}

func (roomInfo *RoomInfo) robotBet(player *RoomPlayer) {
	player.bet_side = getSide()
	player.bet = getRobotBet()
	log.Debug("robot bet: side=%v, value=%v", player.bet_side, player.bet)
	// broadcast bet info
	roomInfo.broadcastGS2CBetInfo()
}
