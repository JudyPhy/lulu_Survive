package msgSendHandler

import (
	"server/pb"

	"code.google.com/p/goprotobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func SendGSSyncPkgSend(serviceIndex uint32, roleList []*pb.FrameRoleData, a gate.Agent) {
	log.Debug("GSSyncPkgSend ========>>>>>> act:%v", serviceIndex)
	data := &pb.GSSyncPkgSend{}
	data.Act = proto.Uint32(serviceIndex)
	data.Role = roleList
	a.WriteMsg(data)
}
