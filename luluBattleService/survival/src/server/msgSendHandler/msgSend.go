package msgSendHandler

import (
	"server/pb"

	"code.google.com/p/goprotobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func SendGS2CRevSyncTime(time int64, a gate.Agent) {
	log.Debug("SendGS2CRevSyncTime ========>>>>>> time=%v", time)
	data := &pb.GS2CRevSyncTime{}
	data.Time = proto.Int64(time)
	a.WriteMsg(data)
}

func SendGS2CSyncTimeAgain(a gate.Agent) {
	log.Debug("SendGS2CSyncTimeAgain ========>>>>>>")
	data := &pb.GS2CSyncTimeAgain{}
	a.WriteMsg(data)
}
