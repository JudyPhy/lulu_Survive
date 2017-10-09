package frameSync

import (
	//	"server/msgSendHandler"
	"server/pb"
	"time"

	//	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

type FrameData struct {
	trs *pb.RoleTrs
}

var ServiceFrameList []*FrameData
var curFrameIndex int32

func Init() {
	//create frame sync timer
	log.Debug("init frame sync")
	go sendFrame()
}

func sendFrame() {
	timer := time.NewTicker(99 * time.Millisecond)
	curFrameIndex = 999999
	for _ = range timer.C {
		log.Debug("Send Frame -> %v", curFrameIndex)
		curFrameIndex++
	}
}
