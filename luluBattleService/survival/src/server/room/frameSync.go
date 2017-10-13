package room

import (
	"server/msgSendHandler"
	"server/pb"
	"server/role"
	"time"

	"code.google.com/p/goprotobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func AddNewFrameData(a gate.Agent, m *pb.C2GSSyncPkg) {
	clientAct := m.GetClientAct()
	log.Debug("Add new frame[%v] data", clientAct)
	roomId := role.PlayerMap[a].RoomID
	roomData := mgrRoom.rooms[roomId]

	curPlayer := roomData.getRoleData(a)
	if curPlayer != nil {
		curPlayer.FrameMap[clientAct] = m.ProcData
	}
}

func (roomData *RoomData) getRoleData(a gate.Agent) *role.Role {
	playerId := role.PlayerMap[a].OID
	for i := 0; i < len(roomData.Roles); i++ {
		oid := role.PlayerMap[roomData.Roles[i].A].OID
		if oid == playerId {
			return roomData.Roles[i]
		}
	}
	return nil
}

func RemoveShownedFrameData(a gate.Agent, index uint32) {
	log.Debug("Remove frame index=%v", index)
	roomId := role.PlayerMap[a].RoomID
	roomData := mgrRoom.rooms[roomId]

	curPlayer := roomData.getRoleData(a)
	delete(curPlayer.FrameMap, index)
}

func (roomData *RoomData) syncStart() {
	//Add first frame
	for i := 0; i < len(roomData.Roles); i++ {
		curPlayer := roomData.Roles[i]
		frame := &pb.FrameData{}
		frame.Index = proto.Uint32(1)
		frame.Move = nil
		frame.Attr = nil
		curPlayer.FrameMap[1] = frame
	}
	//start timer
	go roomData.sendFrame()
}

func (roomData *RoomData) sendFrame() {
	timer := time.NewTicker(99 * time.Millisecond)
	roomData.curFrameIndex = 1
	for _ = range timer.C {
		log.Debug("Send Frame -> room[%v]: %v  -----------%v", roomData.roomId, roomData.curFrameIndex, time.Now().UnixNano()/1e6)
		roleList := roomData.getFrameData(roomData.curFrameIndex)
		for i := 0; i < len(roomData.Roles); i++ {
			msgSendHandler.SendGS2CSyncPkg(roomData.curFrameIndex, roleList, roomData.Roles[i].A)
		}
		roomData.curFrameIndex++
	}
}

func (roomData *RoomData) getFrameData(index uint32) []*pb.SyncRoleData {
	list := make([]*pb.SyncRoleData, 0)
	for i := 0; i < len(roomData.Roles); i++ {
		curPlayer := roomData.Roles[i]
		roleData := &pb.SyncRoleData{}
		roleData.PlayerID = proto.Uint32(role.PlayerMap[curPlayer.A].OID)
		frameList := make([]*pb.FrameData, 0)
		for frameIndex, data := range curPlayer.FrameMap {
			if frameIndex <= index {
				frameList = append(frameList, data)
			}
		}
		log.Debug("player[%v] send frameList[%v] to client.", roleData.GetPlayerID(), len(frameList))
		roleData.FrameList = frameList
		list = append(list, roleData)
	}
	return list
}
