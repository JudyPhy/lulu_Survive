package msgSendHandler

import (
	"server/pb"

	"code.google.com/p/goprotobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func SendGS2CStartGameRet(mode pb.GameMode, mapId int32, bornInfo []*pb.BornInfo, expInfo []*pb.ExpCN, buffInfo []*pb.BuffCN, a gate.Agent) {
	log.Debug("GS2CStartGameRet ========>>>>>> mode:%v, mapId:%v, bornInfo:%v, expInfo:%v, buffInfo:%v",
		mode, mapId, len(bornInfo), len(expInfo), len(buffInfo))
	data := &pb.GS2CStartGameRet{}
	data.Mode = mode.Enum()
	data.MapID = proto.Int32(mapId)
	data.Players = bornInfo
	data.Exp = expInfo
	data.Buff = buffInfo
	a.WriteMsg(data)
}
