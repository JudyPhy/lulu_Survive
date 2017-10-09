package msgSendHandler

import (
	"server/pb"

	//	"code.google.com/p/goprotobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func SendGS2CLoginRet(errorCode *pb.ErrorCode, playerInfo *pb.PlayerInfo, a gate.Agent) {
	log.Debug("SendGS2CLoginRet ========>>>>>> ")
	data := &pb.GS2CLoginRet{}
	data.ErrorCode = errorCode
	data.PlayerInfo = playerInfo
	a.WriteMsg(data)
}
