package msg

import (
	"server/pb"

	//	"github.com/name5566/leaf/network"
	"github.com/name5566/leaf/network/protobuf"
)

//var Processor network.Processor
var Processor = protobuf.NewProcessor()

func init() {
	Processor.Register(&pb.C2GSLogin{})        //0
	Processor.Register(&pb.GS2CLoginRet{})     //1
	Processor.Register(&pb.C2GSCreateRoom{})   //2
	Processor.Register(&pb.C2GSEnterRoom{})    //3
	Processor.Register(&pb.GS2CEnterRoomRet{}) //4
}
