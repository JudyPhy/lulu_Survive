package game_hall

import (
	"math/rand"
	"server/pb"
	"server/playerManager"
	"time"

	"github.com/golang/protobuf/proto"
	"github.com/name5566/leaf/log"
)

const (
	CommonRoomState_Paying  = 1
	CommonRoomState_Playing = 2
)

type CommonRoom struct {
	players     []int64
	state       int
	round_index int
}

var commonRoom *CommonRoom

var results map[int]bool

func init() {
	commonRoom = &CommonRoom{}
	commonRoom.players = make([]int64, 0)
	commonRoom.state = RoomState_Idle
	results = make(map[int]bool)
	go proGame()
}

func getResults() bool {
	rnd := rand.New(rand.NewSource(time.Now().UnixNano()))
	num1 := rnd.Int63n(1000000)
	num2 := rnd.Int63n(1000000)
	return num1 >= num2
}

func proGame() {
	timer := time.NewTicker(1 * time.Second)
	for {
		select {
		case <-timer.C:
			currentTime := time.Now()
			hour := currentTime.Hour()
			min := currentTime.Minute()
			sec := currentTime.Second()
			index := hour * 12
			index += min / 5
			commonRoom.round_index = index + 1
			if min%5 == 4 && sec >= 55 {
				commonRoom.state = CommonRoomState_Playing
				results[commonRoom.round_index] = getResults()
				sendGS2CCommonRoomResults(commonRoom.round_index, results[commonRoom.round_index])
				log.Debug("round_index:%v, results:%v", commonRoom.round_index, results[commonRoom.round_index])
			} else {
				commonRoom.state = CommonRoomState_Paying
			}
			log.Debug("currentTime:%v", currentTime)
		}
	}
}

func enterCommonRoom(playerId int64) {
	log.Debug("common room round[%v]", commonRoom.round_index)
	commonRoom.players = append(commonRoom.players, playerId)
	log.Debug("common room player count=%v", len(commonRoom.players))
}

func sendGS2CCommonRoomResults(roundIndex int, results bool) {
	log.Debug("sendGS2CCommonRoomResults: %v, %v", roundIndex, results)
	ret := &pb.GS2CCommonRoomResults{}
	ret.RountIndex = proto.Int(roundIndex)
	ret.Results = proto.Bool(results)
	for _, a := range playerManager.GetAllAgent() {
		a.WriteMsg(ret)
	}
}
