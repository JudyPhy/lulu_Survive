package msg

import (
	"server/pb"

	"github.com/name5566/leaf/log"
	"github.com/name5566/leaf/network/protobuf"
)

var Processor = protobuf.NewProcessor()

func init() {
	log.Debug("Register msg->")

	Processor.Register(&pb.C2GSLogin{})         //0
	Processor.Register(&pb.GS2CLoginRet{})      //1
	Processor.Register(&pb.C2GSChooseRole{})    //2
	Processor.Register(&pb.GS2CChooseRoleRet{}) //3
	Processor.Register(&pb.C2GSStartGame{})     //4
	Processor.Register(&pb.GS2CStartGameRet{})  //5
	Processor.Register(&pb.C2GSSyncPkg{})       //6
	Processor.Register(&pb.GS2CSyncPkg{})       //7

	log.Debug("Register msg over, msg count=8")
}
