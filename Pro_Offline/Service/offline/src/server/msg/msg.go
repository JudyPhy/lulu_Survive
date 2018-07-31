package msg

import (
	"server/pb"

	"github.com/name5566/leaf/log"
	"github.com/name5566/leaf/network/protobuf"
)

var Processor = protobuf.NewProcessor()

func init() {
	log.Debug("Register msg->")

	Processor.Register(&pb.C2GSLogin{})        //0
	Processor.Register(&pb.GS2CLoginRet{})     //1
	Processor.Register(&pb.C2GSSwitchMap{})    //2
	Processor.Register(&pb.GS2CSwitchMapRet{}) //3
	Processor.Register(&pb.GS2CBattleInfo{})   //4

	log.Debug("Register msg over, msg count=8")
}
