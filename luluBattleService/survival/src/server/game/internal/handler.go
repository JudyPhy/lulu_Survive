package internal

import (
	"reflect"
	"server/pb"
	"server/room"

	//	"github.com/name5566/leaf/gate"
	//	"github.com/name5566/leaf/log"
)

func init() {
}

func handler(m interface{}, h interface{}) {
	skeleton.RegisterChanRPC(reflect.TypeOf(m), h)
}

func init() {
	handler(&pb.C2GSStartGame{}, recvC2GSStartGame)
	handler(&pb.GSSyncPkgRecv{}, recvGSSyncPkgRecv)
}

func recvC2GSStartGame(args []interface{}) {
	m := args[0].(*pb.C2GSStartGame)
	a := args[1].(gate.Agent)
	room.EnterRoom(a)
}

func recvGSSyncPkgRecv(args []interface{}) {
	//	m := args[0].(*pb.GSSyncPkgRecv)
	//a := args[1].(gate.Agent)

}
