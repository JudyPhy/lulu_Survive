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
	Processor.Register(&pb.GS2CSceneCreate{})   //4
	Processor.Register(&pb.C2GSStartGame{})     //5
	Processor.Register(&pb.GS2CStartGameRet{})  //6
	Processor.Register(&pb.GSSyncPkgRecv{})     //7
	Processor.Register(&pb.GSSyncPkgSend{})     //8

	log.Debug("Register msg over, msg count=9")
}
