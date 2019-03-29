package internal

import (
	"reflect"
	"server/game_hall"
	"server/gm"
	"server/pb"
)

func handleMsg(m interface{}, h interface{}) {
	skeleton.RegisterChanRPC(reflect.TypeOf(m), h)
}

func init() {
	handleMsg(&pb.C2GSCreateRoom{}, game_hall.RecvC2GSCreateRoom)
	handleMsg(&pb.C2GSEnterRoom{}, game_hall.RecvC2GSEnterRoom)
	handleMsg(&pb.C2GSBet{}, game_hall.RecvC2GSBet)
	//GM
	handleMsg(&pb.C2GSGMAddCoin{}, gm.RecvC2GSGMAddCoin)
}
