package frameSync

import (
	"server/msgSendHandler"
	"server/pb"
	"time"

	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

var timer *Ticker

type FrameData struct {
	trs *pb.Transform
}

var ServiceFrameList []*FrameData
var curFrameIndex int

func init() {
	//create frame sync timer
	timer = time.NewTicker(99 * time.Millisecond)
	go sendFrame()
}

func sendFrame() {
	for _ = range timer.C {
		log.Debug("Send Frame -> %v", curFrameIndex)
		curFrameIndex++
	}
}
