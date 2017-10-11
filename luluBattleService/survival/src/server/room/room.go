package room

import (
	"math/rand"
	"server/frameSync"
	"server/msgSendHandler"
	"server/role"
	"strconv"
	"strings"
	"sync"
	"time"

	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

const (
	ROOM_MEMBER_COUNT int = 10
)

type RoomMgr struct {
	lock    sync.Mutex
	roomMap map[string][]*role.Role
}

var mgrRoom *RoomMgr

func init() {
	mgrRoom = &RoomMgr{}
	mgrRoom.roomMap = make(map[string][]*role.Role)
}

func EnterGame(a gate.Agent, playerId int32) {
	log.Debug("EnterGame playerId=%v", playerId)
	enterRoom := reqRoom()
	frameSyncStarted := true
	if enterRoom == "" {
		frameSyncStarted = false
		enterRoom = createRoom(a, playerId)
	}
	log.Debug("11111111member count=%v", len(mgrRoom.roomMap[enterRoom]))
	player := addRoleToRoom(enterRoom, playerId, a)
	log.Debug("member count=%v", len(mgrRoom.roomMap[enterRoom]))
	for i := 0; i < len(mgrRoom.roomMap[enterRoom]); i++ {
		msgSendHandler.SendGS2CEnterGame(player, player.A)
	}

	if !frameSyncStarted {
		frameSync.SyncStart(enterRoom, mgrRoom.roomMap[enterRoom])
	}
}

func addRoleToRoom(roomId string, playerId int32, a gate.Agent) *role.Role {
	log.Debug("Add player[%v] to room[%v]", playerId, roomId)
	_, ok := mgrRoom.roomMap[roomId]
	if ok {
		player := &role.Role{}
		player.A = a
		player.PlayerId = playerId
		player.InitInfo()
		player.InitAttr()
		player.RoomId = roomId
		mgrRoom.lock.Lock()
		mgrRoom.roomMap[roomId] = append(mgrRoom.roomMap[roomId], player)
		mgrRoom.lock.Unlock()
		return player
	} else {
		log.Error("Room[%v] not exist.", roomId)
		return nil
	}
}

func reqRoom() string {
	for roomId, roleList := range mgrRoom.roomMap {
		count := len(roleList)
		log.Debug("roomId[%v] : roleCount[%v]", roomId, count)
		if count < ROOM_MEMBER_COUNT {
			return roomId
		}
	}
	return ""
}

func createRoom(a gate.Agent, playerId int32) string {
	newRole := &role.Role{}
	newRole.A = a
	newRole.PlayerId = playerId
	newRole.RoomId = reqNewRoom()
	newRole.InitAttr()
	roleList := make([]*role.Role, 0)
	roleList = append(roleList, newRole)
	mgrRoom.roomMap[newRole.RoomId] = roleList
	return newRole.RoomId
}

func getRandomRoomId(length int) string {
	log.Debug("getRandomRoomId")
	rand.Seed(time.Now().UnixNano())
	rs := make([]string, length)
	for start := 0; start < length; start++ {
		rs = append(rs, strconv.Itoa(rand.Intn(10)))
	}
	return strings.Join(rs, "") //使用""拼接rs切片
}

func reqNewRoom() string {
	log.Debug("ReqNewRoom")
	newRoomId := getRandomRoomId(6)
	for {
		mgrRoom.lock.Lock()
		_, ok := mgrRoom.roomMap[newRoomId]
		if ok {
			newRoomId = getRandomRoomId(6)
		} else {
			break
		}
	}
	mgrRoom.lock.Unlock()
	return newRoomId
}
