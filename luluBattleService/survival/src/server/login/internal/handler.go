package internal

import (
	"reflect"
	"server/msgSendHandler"
	"server/pb"
	"time"

	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func handleMsg(m interface{}, h interface{}) {
	skeleton.RegisterChanRPC(reflect.TypeOf(m), h)
}

func init() {
}

func recvC2GSReqSyncTime(args []interface{}) {
	//test delay send time
	//time.Sleep(5 * time.Second)

	m := args[0].(*pb.C2GSReqSyncTime)
	a := args[1].(gate.Agent)
	log.Debug("========>>>>>> recvC2GSReqSyncTime, client send time=%v", m.GetTime())
	tm := time.Unix(m.GetTime(), 0)
	log.Debug("client send time=%v", tm.Format("2006-01-02 03:04:05"))

	now := time.Now()
	log.Debug("service current time=%v", now)
	dur, _ := time.ParseDuration("-2s")
	now = now.Add(dur)
	timestamp := now.Unix()
	log.Debug("service current timestamp=%v", timestamp)
	msgSendHandler.SendGS2CRevSyncTime(timestamp, a)
}

func recvC2GSMove(args []interface{}) {
	m := args[0].(*pb.C2GSMove)
	a := args[1].(gate.Agent)
	log.Debug("========>>>>>> recvC2GSMove, client send time=%v, service current time=%v", m.GetTime(), time.Now().Unix())
	if m.GetTime() > time.Now().Unix() {
		log.Debug("need adjust time")
		msgSendHandler.SendGS2CSyncTimeAgain(a)
	}
}
