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
		sendGS2CEnterRoomRet(a, roomInfo.roomId, pb.GS2CEnterRoomRet_Success.Enum())
	}
}

func sendGS2CEnterRoomRet(a gate.Agent, roomId int64, errorCode *pb.GS2CEnterRoomRet_ErrorCode) {
	log.Debug("sendGS2CEnterRoomRet: %v", roomId)
	ret := &pb.GS2CEnterRoomRet{}
	ret.ErrorCode = errorCode
	ret.RoomId = proto.Int64(roomId)
	a.WriteMsg(ret)
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
			//common room
			enterCommonRoom(playerId)
			sendGS2CEnterRoomRet(a, 0, pb.GS2CEnterRoomRet_Success.Enum())
		} else {
			//input roomid to enter
			roomInfo := getRoomById(m.GetRoomId())
			if roomInfo == nil {
				sendGS2CEnterRoomRet(a, 0, pb.GS2CEnterRoomRet_Fail.Enum())
			} else if roomInfo.state != RoomState_Idle {
				sendGS2CEnterRoomRet(a, 0, pb.GS2CEnterRoomRet_GameStart.Enum())
			} else {
				if roomInfo.password != 0 {
					if m.GetPassword() == roomInfo.password {
						//password right
						roomInfo.players = append(roomInfo.players, playerId)
						sendGS2CEnterRoomRet(a, roomInfo.roomId, pb.GS2CEnterRoomRet_Success.Enum())
					} else if m.GetPassword() == 0 {
						//need input password
						sendGS2CEnterRoomRet(a, m.GetRoomId(), pb.GS2CEnterRoomRet_NeedPassword.Enum())
					} else {
						//password error
						sendGS2CEnterRoomRet(a, 0, pb.GS2CEnterRoomRet_Fail.Enum())
					}
				} else {
					//room has no password
					roomInfo.players = append(roomInfo.players, playerId)
					sendGS2CEnterRoomRet(a, roomInfo.roomId, pb.GS2CEnterRoomRet_Success.Enum())
				}
			}
		}
	}
}
