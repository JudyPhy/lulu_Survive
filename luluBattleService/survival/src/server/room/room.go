package room

import (
	"math/rand"
	"server/msgSendHandler"
	"server/pb"
	"server/role"
	"strconv"
	"strings"
	"sync"
	"time"

	"code.google.com/p/goprotobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

const (
	ROOM_MEMBER_COUNT int = 1
)

type RoomData struct {
	roomId        string
	mode          pb.GameMode
	Roles         []*role.Role
	Map           *SceneMap
	curFrameIndex uint32
}

type RoomMgr struct {
	lock  sync.Mutex
	rooms map[string]*RoomData
}

var mgrRoom *RoomMgr

func init() {
	mgrRoom = &RoomMgr{}
	mgrRoom.rooms = make(map[string]*RoomData)
}

func EnterRoom(a gate.Agent, mode pb.GameMode, mapId int32, roleId int32) {
	playerInfo, ok := role.PlayerMap[a]
	if !ok {
		log.Error("[room] Player not login.")
		return
	}
	enterRoom := reqRoom()
	playerInfo.RoomID = enterRoom
	log.Debug("Player[%v] enter room[%v].", playerInfo.OID, playerInfo.RoomID)
	_, ok = mgrRoom.rooms[enterRoom]
	if !ok {
		mgrRoom.rooms[enterRoom] = &RoomData{}
		mgrRoom.rooms[enterRoom].roomId = enterRoom
		mgrRoom.rooms[enterRoom].mode = mode
		mgrRoom.rooms[enterRoom].Roles = make([]*role.Role, 0)
	}
	addRoleToRoom(enterRoom, roleId, a)

	//check member count
	if len(mgrRoom.rooms[enterRoom].Roles) == ROOM_MEMBER_COUNT {
		log.Debug("Room[%v] prepare start game.", enterRoom)
		mgrRoom.rooms[enterRoom].Map = &SceneMap{}
		mgrRoom.rooms[enterRoom].Map.createScene(mapId)
		mgrRoom.rooms[enterRoom].sendRoomBattleStart(mode, mapId, roleId)
		//Frame sync start
		mgrRoom.rooms[enterRoom].syncStart()
	}
}

func (roomData *RoomData) sendRoomBattleStart(mode pb.GameMode, mapId int32, roleId int32) {
	bornInfo := roomData.prepareBornInfo()
	expInfo := roomData.prepareExpInfo()
	buffInfo := roomData.prepareBuffInfo()
	for i := 0; i < len(roomData.Roles); i++ {
		msgSendHandler.SendGS2CStartGameRet(mode, mapId, bornInfo, expInfo, buffInfo, roomData.Roles[i].A)
	}
}

func (roomData *RoomData) prepareBornInfo() []*pb.BornInfo {
	list := make([]*pb.BornInfo, 0)
	for i := 0; i < len(roomData.Roles); i++ {
		curPlayer := roomData.Roles[i]
		info := &pb.BornInfo{}
		info.OID = proto.Uint32(role.PlayerMap[curPlayer.A].OID)
		info.NickName = proto.String(role.PlayerMap[curPlayer.A].NickName)
		info.RoleID = proto.Int32(curPlayer.RoleType)
		info.PosX = proto.Int32(int32(roomData.Map.BornPos[i][0]))
		info.PosY = proto.Int32(int32(roomData.Map.BornPos[i][1]))
		info.Attr = &pb.BaseAttr{}
		info.Attr.Hp = proto.Uint32(1000)
		list = append(list, info)
	}
	return list
}

func (roomData *RoomData) prepareExpInfo() []*pb.ExpCN {
	list := make([]*pb.ExpCN, 0)
	for i := 0; i < len(roomData.Map.ExpPos); i++ {
		pos := &pb.ExpCN{}
		pos.Status = proto.Uint32(0)
		pos.PosX = proto.Uint32(roomData.Map.ExpPos[i][0])
		pos.PosY = proto.Uint32(roomData.Map.ExpPos[i][1])
		list = append(list, pos)
	}
	return list
}

func (roomData *RoomData) prepareBuffInfo() []*pb.BuffCN {
	list := make([]*pb.BuffCN, 0)
	for i := 0; i < len(roomData.Map.BuffPos); i++ {
		pos := &pb.BuffCN{}
		pos.Status = proto.Uint32(0)
		pos.PosX = proto.Uint32(roomData.Map.BuffPos[i][0])
		pos.PosY = proto.Uint32(roomData.Map.BuffPos[i][1])
		list = append(list, pos)
	}
	return list
}

func addRoleToRoom(roomId string, roleId int32, a gate.Agent) {
	player := &role.Role{}
	player.A = a
	player.RoleType = roleId
	player.FrameMap = make(map[uint32]*pb.FrameData)
	mgrRoom.lock.Lock()
	mgrRoom.rooms[roomId].Roles = append(mgrRoom.rooms[roomId].Roles, player)
	mgrRoom.lock.Unlock()
}

func reqRoom() string {
	for roomId, roomData := range mgrRoom.rooms {
		count := len(roomData.Roles)
		if count < ROOM_MEMBER_COUNT {
			return roomId
		}
	}
	return reqNewRoom()
}

func reqNewRoom() string {
	log.Debug("ReqNewRoom")
	newRoomId := getRandomRoomId(6)
	for {
		mgrRoom.lock.Lock()
		_, ok := mgrRoom.rooms[newRoomId]
		if ok {
			newRoomId = getRandomRoomId(6)
		} else {
			break
		}
	}
	mgrRoom.lock.Unlock()
	return newRoomId
}

func getRandomRoomId(length int) string {
	rand.Seed(time.Now().UnixNano())
	rs := make([]string, length)
	for start := 0; start < length; start++ {
		rs = append(rs, strconv.Itoa(rand.Intn(10)))
	}
	return strings.Join(rs, "") //使用""拼接rs切片
}
