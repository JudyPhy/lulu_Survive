package msgSendHandler

import (
	"server/pb"

	"code.google.com/p/goprotobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func SendGS2CSyncPkg(serviceIndex uint32, roleList []*pb.SyncRoleData, a gate.Agent) {
	log.Debug("GS2CSyncPkg ========>>>>>> act:%v", serviceIndex)
	data := &pb.GS2CSyncPkg{}
	data.Act = proto.Uint32(serviceIndex)
	data.RoleList = roleList
	a.WriteMsg(data)
}
