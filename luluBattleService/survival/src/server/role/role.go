package role

import (
	"server/pb"

	"code.google.com/p/goprotobuf/proto"
	"github.com/name5566/leaf/gate"
	//"github.com/name5566/leaf/log"
)

type RoleAttr struct {
	Hp uint32
}

type Role struct {
	A        gate.Agent
	RoleType int32
	BornAttr *pb.BaseAttr
	FrameMap map[uint32]*pb.FrameData
}

func (role *Role) InitAttr() {
	role.BornAttr = &pb.BaseAttr{}
	role.BornAttr.Hp = proto.Uint32(1000)
}

func (attr *RoleAttr) ToPbData() *pb.BaseAttr {
	pbData := &pb.BaseAttr{}
	pbData.Hp = proto.Uint32(attr.Hp)
	return pbData
}

func (attr *RoleAttr) Update(pbData *pb.BaseAttr) {
	attr.Hp = pbData.GetHp()
}

func (player *Role) RemoveProcedFrame(index uint32) {
	for {
		if player.needRemoveFrame(index) {
			for frameIndex, _ := range player.FrameMap {
				if frameIndex <= index {
					delete(player.FrameMap, frameIndex)
					break
				}
			}
		} else {
			break
		}
	}
}

func (player *Role) needRemoveFrame(index uint32) bool {
	for frameIndex, _ := range player.FrameMap {
		if frameIndex <= index {
			return true
		}
	}
	return false
}
