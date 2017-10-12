package room

import (
	"server/msgSendHandler"
	"server/pb"
	"server/role"
	"time"

	"code.google.com/p/goprotobuf/proto"
	//	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

type FrameSync struct {
	serviceFrameList []*pb.FrameRoleData
	curFrameIndex    uint32
}

func (sync *FrameSync) start(roomData *RoomData) {
	go sync.sendFrame(roomData)
}

func (sync *FrameSync) sendFrame(roomData *RoomData) {
	timer := time.NewTicker(99 * time.Millisecond)
	roomSync.curFrameIndex = 1
	for _ = range timer.C {
		log.Debug("Send Frame -> room[%v]: %v  -----------%v", roomSync.roomId, roomSync.curFrameIndex, time.Now().UnixNano()/1e6)
		roomSync.serviceFrameList = getFrameData(roleList)
		for i := 0; i < len(roleList); i++ {
			msgSendHandler.SendGSSyncPkgSend(roomSync.curFrameIndex, roomSync.serviceFrameList, roleList[i].A)
		}
		roomSync.curFrameIndex++
	}
}

func getFrameData(roleList []*role.Role) []*pb.FrameRoleData {
	for i := 0; i < len(roleList); i++ {
		curRole := &pb.FrameRoleData{}
		curRole.PlayerID = proto.Uint32(uint32(roleList[i].PlayerId))
		curRole.Hp = proto.Uint32(roleList[i].Attr.Hp)
	}
	return nil
}
