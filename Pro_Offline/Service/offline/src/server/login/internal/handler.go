package internal

import (
	"reflect"
	"server/pb"
	"server/user"

	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func handler(m interface{}, h interface{}) {
	skeleton.RegisterChanRPC(reflect.TypeOf(m), h)
}

func init() {
	handler(&pb.C2GSLogin{}, recvC2GSLogin)
}

func recvC2GSLogin(args []interface{}) {
	m := args[0].(*pb.C2GSLogin)
	a := args[1].(gate.Agent)
	log.Debug("C2GSLogin <<<======== oid=%v", m.GetOID())
	ret := user.UserLogin(a, m.GetOID())
	a.WriteMsg(ret)
}
