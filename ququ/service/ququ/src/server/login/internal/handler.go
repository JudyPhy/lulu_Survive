package internal

import (
	"reflect"
	"server/pb"

	"github.com/golang/protobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func handleMsg(m interface{}, h interface{}) {
	skeleton.RegisterChanRPC(reflect.TypeOf(m), h)
}

func init() {
	// 向当前模块注册消息处理函数
	handleMsg(&pb.C2GSLogin{}, recvC2GSLogin)
}

func recvC2GSLogin(args []interface{}) {
	log.Debug("recvC2GSLogin=>%v", args)
	m := args[0].(*pb.C2GSLogin)
	log.Debug("User=%v", m.GetUser())
	a := args[1].(gate.Agent)

	//	// get data from db
	//	var playerOid int32 = 10000
	//	player := &pb.PlayerInfo{
	//		Oid:      proto.Int32(playerOid),
	//		NickName: proto.String(m.GetAccount()),
	//		HeadIcon: proto.String(""),
	//		Gold:     proto.Int32(99),
	//		Diamond:  proto.Int32(100)}

	//	chanPlayer := roomMgr.NewPlayer(player)
	//	roomMgr.AddChanPlayerInfo(a, chanPlayer)

	//ret to client
	ret := &pb.GS2CLoginRet{}
	ret.ErrorCode = pb.GS2CLoginRet_Success.Enum()
	ret.User = proto.String(m.GetUser())
	a.WriteMsg(ret)
}
