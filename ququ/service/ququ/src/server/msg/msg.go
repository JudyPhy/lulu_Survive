package msg

import (
	"server/pb"

	//	"github.com/name5566/leaf/network"
	"github.com/name5566/leaf/network/protobuf"
)

//var Processor network.Processor
var Processor = protobuf.NewProcessor()

func init() {
	Processor.Register(&pb.C2GSLogin{})         //0
	Processor.Register(&pb.GS2CLoginRet{})      //1
	Processor.Register(&pb.C2GSCreateRoom{})    //2
	Processor.Register(&pb.C2GSEnterRoom{})     //3
	Processor.Register(&pb.GS2CEnterRoomRet{})  //4
	Processor.Register(&pb.GS2CTurnToBet{})     //5
	Processor.Register(&pb.C2GSBet{})           //6
	Processor.Register(&pb.GS2CBetRet{})        //7
	Processor.Register(&pb.GS2CBetInfo{})       //8
	Processor.Register(&pb.GS2CGameResults{})   //9
	Processor.Register(&pb.GS2CNewRoundStart{}) //10
	Processor.Register(&pb.GS2CGameOver{})      //11
	// GM
	Processor.Register(&pb.C2GSGMAddCoin{})    //12
	Processor.Register(&pb.GS2CGMAddCoinRet{}) //13
}
