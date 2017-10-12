package msgSendHandler

import (
	"server/pb"

	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func SendGS2CLoginRet(errorCode *pb.ErrorCode, playerInfo *pb.PlayerInfo, a gate.Agent) {
	log.Debug("GS2CLoginRet ========>>>>>> ")
	data := &pb.GS2CLoginRet{}
	data.ErrorCode = errorCode
	data.PlayerInfo = playerInfo
	a.WriteMsg(data)
}

func SendGS2CChooseRoleRet(errorCode *pb.ErrorCode, playerInfo *pb.PlayerInfo, a gate.Agent) {
	log.Debug("SendGS2CChooseRoleRet ========>>>>>> ")
	data := &pb.GS2CChooseRoleRet{}
	data.ErrorCode = errorCode
	data.PlayerInfo = playerInfo
	a.WriteMsg(data)
}
