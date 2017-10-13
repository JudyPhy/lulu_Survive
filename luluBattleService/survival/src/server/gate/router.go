package gate

import (
	"server/game"
	"server/login"
	"server/msg"
	"server/pb"
)

func init() {

	msg.Processor.SetRouter(&pb.C2GSLogin{}, login.ChanRPC)
	msg.Processor.SetRouter(&pb.C2GSChooseRole{}, login.ChanRPC)

	msg.Processor.SetRouter(&pb.C2GSStartGame{}, game.ChanRPC)
	msg.Processor.SetRouter(&pb.C2GSSyncPkg{}, game.ChanRPC)

}
