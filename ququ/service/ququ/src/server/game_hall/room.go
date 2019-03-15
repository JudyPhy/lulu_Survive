package game_hall

import (
	"math/rand"
	"server/pb"
	"time"

	"github.com/name5566/leaf/log"
)

type RoomState int

const (
	RoomState_Idle    = 1
	RoomState_Playing = 2
)

type RoomInfo struct {
	roomId      int64
	owner       int64
	payMode     pb.PayMode
	round       pb.GameRound
	exit_pay    bool
	password    int64
	players     []int64
	state       int
	round_index int
}

var roomMap map[int64]*RoomInfo

func init() {
	roomMap = make(map[int64]*RoomInfo)
}

func getRoomId() int64 {
	rnd := rand.New(rand.NewSource(time.Now().UnixNano()))
	id := rnd.Int63n(1000000)
	_, ok := roomMap[id]
	if ok {
		return getRoomId()
	} else {
		return id
	}
}

func newRoom(playerId int64, payMode pb.PayMode, round pb.GameRound, exit_pay bool, password int64) *RoomInfo {
	id := getRoomId()
	log.Debug("create new room:%v", id)
	room := &RoomInfo{}
	room.roomId = id
	room.owner = playerId
	room.payMode = payMode
	room.round = round
	room.exit_pay = exit_pay
	room.password = password
	room.players = make([]int64, 0)
	room.players = append(room.players, playerId)
	room.state = RoomState_Idle
	room.round_index = 0
	return room
}

func getRoomById(roomId int64) *RoomInfo {
	roomInfo, ok := roomMap[roomId]
	if ok {
		return roomInfo
	} else {
		return nil
	}
}

func enterRandomRoom(playerId int64) *RoomInfo {
	for _, roomInfo := range roomMap {
		if roomInfo.password == 0 && roomInfo.state == RoomState_Idle {
			roomInfo.players = append(roomInfo.players, playerId)
			return roomInfo
		}
	}
	return nil
}
