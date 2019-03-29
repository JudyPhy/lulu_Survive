package game_hall

import (
	"server/pb"
	"server/playerManager"

	"github.com/golang/protobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func RecvC2GSCreateRoom(args []interface{}) {
	log.Debug("RecvC2GSCreateRoom=>%v", args)
	m := args[0].(*pb.C2GSCreateRoom)
	a := args[1].(gate.Agent)
	playerId := playerManager.GetPlayerId(a)
	if playerId == 0 {
		log.Error("can't find playerid of agent[%v]", a)
	} else {
		roomInfo := newRoom(playerId, m.GetPay(), m.GetRound(), m.GetExitPay(), m.GetPassword())
		sendGS2CEnterRoomRet(a, roomInfo.roomId, 0, pb.GS2CEnterRoomRet_Success.Enum())
	}
}

func RecvC2GSEnterRoom(args []interface{}) {
	log.Debug("RecvC2GSEnterRoom=>%v", args)
	m := args[0].(*pb.C2GSEnterRoom)
	a := args[1].(gate.Agent)
	playerId := playerManager.GetPlayerId(a)
	if playerId == 0 {
		log.Error("[hall]can't find playerid of agent[%v]", a)
	} else {
		if m.GetRoomId() == 0 {
			//robot room
			enterRobotRoom(a, playerId)
		} else {
			//			//input roomid to enter
			//			roomInfo := getRoomById(m.GetRoomId())
			//			if roomInfo == nil {
			//				sendGS2CEnterRoomRet(a, 0, pb.GS2CEnterRoomRet_Fail.Enum())
			//			} else if roomInfo.state != RoomState_Idle {
			//				sendGS2CEnterRoomRet(a, 0, pb.GS2CEnterRoomRet_GameStart.Enum())
			//			} else {
			//				if roomInfo.password != 0 {
			//					if m.GetPassword() == roomInfo.password {
			//						//password right
			//						roomInfo.players = append(roomInfo.players, playerId)
			//						sendGS2CEnterRoomRet(a, roomInfo.roomId, pb.GS2CEnterRoomRet_Success.Enum())
			//					} else if m.GetPassword() == 0 {
			//						//need input password
			//						sendGS2CEnterRoomRet(a, m.GetRoomId(), pb.GS2CEnterRoomRet_NeedPassword.Enum())
			//					} else {
			//						//password error
			//						sendGS2CEnterRoomRet(a, 0, pb.GS2CEnterRoomRet_Fail.Enum())
			//					}
			//				} else {
			//					//room has no password
			//					roomInfo.players = append(roomInfo.players, playerId)
			//					sendGS2CEnterRoomRet(a, roomInfo.roomId, pb.GS2CEnterRoomRet_Success.Enum())
			//				}
			//			}
		}
	}
}

func RecvC2GSBet(args []interface{}) {
	log.Debug("RecvC2GSEnterRoom=>%v", args)
	m := args[0].(*pb.C2GSBet)
	a := args[1].(gate.Agent)
	roomId := m.GetRoomId()
	roomInfo, ok := roomMap[roomId]
	if ok {
		go roomInfo.bet(a, m.GetRountIndex(), m.GetBetSide(), m.GetBet())
	}
}

func sendGS2CEnterRoomRet(a gate.Agent, roomId int64, roundIndex int32, errorCode *pb.GS2CEnterRoomRet_ErrorCode) {
	log.Debug("sendGS2CEnterRoomRet: roomId=%v, roundIndex=%v", roomId, roundIndex)
	ret := &pb.GS2CEnterRoomRet{}
	ret.ErrorCode = errorCode
	ret.RoomId = proto.Int64(roomId)
	ret.RountIndex = proto.Int32(roundIndex)
	a.WriteMsg(ret)
}

func sendGS2CTurnToBet(a gate.Agent, roomId int64) {
	log.Debug("sendGS2CTurnToBet")
	ret := &pb.GS2CTurnToBet{}
	ret.RoomId = proto.Int64(roomId)
	a.WriteMsg(ret)
}

func sendGS2CBetRet(a gate.Agent, errorCode *pb.GS2CBetRet_ErrorCode) {
	log.Debug("sendGS2CBetRet: errorCode=%v", errorCode)
	ret := &pb.GS2CBetRet{}
	ret.ErrorCode = errorCode
	a.WriteMsg(ret)
}

func sendGS2CGameResults(a gate.Agent, result bool, winCoin int64) {
	log.Debug("sendGS2CGameResults: result=%v, winCoin=%v", result, winCoin)
	ret := &pb.GS2CGameResults{}
	ret.Results = proto.Bool(result)
	ret.WinCoin = proto.Int64(winCoin)
	a.WriteMsg(ret)
}

func sendGS2CNewRoundStart(a gate.Agent, roundIndex int32) {
	log.Debug("sendGS2CNewRoundStart: %v", roundIndex)
	ret := &pb.GS2CNewRoundStart{}
	ret.RountIndex = proto.Int32(roundIndex)
	a.WriteMsg(ret)
}

func sendGS2CGameOver(a gate.Agent) {
	log.Debug("sendGS2CGameOver")
	ret := &pb.GS2CGameOver{}
	a.WriteMsg(ret)
}
