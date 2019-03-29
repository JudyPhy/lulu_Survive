package game_hall

import (
	"math/rand"
	"server/pb"
	"time"

	"github.com/name5566/leaf/gate"
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
		if robotSide == Side_Blue {
			newPlayer.side = Side_Red
		} else {
			newPlayer.side = Side_Blue
		}
		roomInfo.players = append(roomInfo.players, newPlayer)
		sendGS2CEnterRoomRet(a, roomInfo.roomId, roomInfo.round_index, pb.GS2CEnterRoomRet_Success.Enum())
		go roomInfo.startGame()
	} else {
		sendGS2CEnterRoomRet(a, 0, 0, pb.GS2CEnterRoomRet_Fail.Enum())
	}
}

func getRobotBet() int64 {
	betValue := []int64{200, 400, 800, 1600}
	rnd := rand.New(rand.NewSource(time.Now().UnixNano()))
	index := rnd.Intn(4)
	return betValue[index]
}

func getRobotBetSide() Side {
	betValue := []Side{Side_Blue, Side_Red}
	rnd := rand.New(rand.NewSource(time.Now().UnixNano()))
	index := rnd.Intn(2)
	return betValue[index]
}
