package gate

import (
	//	"server/game"
	"server/login"
	"server/msg"
	"server/pb"
)

func init() {
	// 这里指定消息, 路由到login,game模块
	msg.Processor.SetRouter(&pb.C2GSLogin{}, login.ChanRPC)
}
