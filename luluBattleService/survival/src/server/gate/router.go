package gate

import (
	"server/login"
	"server/msg"
	"server/pb"
)

func init() {

	msg.Processor.SetRouter(&pb.C2GSReqSyncTime{}, login.ChanRPC)
	msg.Processor.SetRouter(&pb.C2GSMove{}, login.ChanRPC)

}
