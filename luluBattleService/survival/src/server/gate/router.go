package gate

import (
	"server/login"
	"server/msg"
	"server/pb"
)

func init() {

	msg.Processor.SetRouter(&pb.GSSyncPkgRecv{}, game.ChanRPC)

}
