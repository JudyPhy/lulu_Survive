package internal

import (
	"reflect"
	"server/pb"

	"github.com/name5566/leaf/gate"
	//	"github.com/name5566/leaf/log"
)

func init() {
}

func handler(m interface{}, h interface{}) {
	skeleton.RegisterChanRPC(reflect.TypeOf(m), h)
}

func init() {
	handler(&pb.GSSyncPkgRecv{}, recvGSSyncPkgRecv)
}

func recvGSSyncPkgRecv(args []interface{}) {
	m := args[0].(*pb.GSSyncPkgRecv)
	a := args[1].(gate.Agent)

}
