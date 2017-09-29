package msg

import (
	"server/pb"

	"github.com/name5566/leaf/log"
	"github.com/name5566/leaf/network/protobuf"
)

var Processor = protobuf.NewProcessor()

func init() {
	log.Debug("Register msg->")
	Processor.Register(&pb.C2GSReqSyncTime{})   //0
	Processor.Register(&pb.GS2CRevSyncTime{})   //1
	Processor.Register(&pb.GS2CSyncTimeAgain{}) //2

	Processor.Register(&pb.C2GSMove{}) //3
	log.Debug("Register msg over, msg count=4")
}
