package msg

import (
	"server/pb"

	"github.com/name5566/leaf/log"
	"github.com/name5566/leaf/network/protobuf"
)

var Processor = protobuf.NewProcessor()

func init() {
	log.Debug("Register msg->")

	Processor.Register(&pb.GSSyncPkgRecv{}) //0
	Processor.Register(&pb.GSSyncPkgSend{}) //1

	log.Debug("Register msg over, msg count=2")
}
