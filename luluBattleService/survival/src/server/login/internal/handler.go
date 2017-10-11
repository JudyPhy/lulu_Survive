package internal

import (
	"reflect"
	"server/msgSendHandler"
	"server/pb"
	"server/room"

	"code.google.com/p/goprotobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func handler(m interface{}, h interface{}) {
	skeleton.RegisterChanRPC(reflect.TypeOf(m), h)
}

func init() {
	handler(&pb.C2GSLogin{}, recvC2GSLogin)
	handler(&pb.C2GSChooseRole{}, recvC2GSChooseRole)
}

func recvC2GSLogin(args []interface{}) {
	m := args[0].(*pb.C2GSLogin)
	a := args[1].(gate.Agent)
	log.Debug("C2GSLogin <<<<<<======== acoount=%v, password=%v", m.GetAccount(), m.GetPassword())

	msgSendHandler.SendGS2CLoginRet(pb.ErrorCode_SUCCESS.Enum(), nil, a)
}

func recvC2GSChooseRole(args []interface{}) {
	m := args[0].(*pb.C2GSChooseRole)
	a := args[1].(gate.Agent)
	log.Debug("C2GSChooseRole <<<<<<======== nickname=%v, headicon=%v", m.GetNickName(), m.GetHeadIcon())

	playerInfo := &pb.PlayerInfo{}
	playerInfo.OID = proto.Int32(111)
	playerInfo.NickName = m.NickName
	playerInfo.HeadIcon = m.HeadIcon
	msgSendHandler.SendGS2CChooseRoleRet(pb.ErrorCode_SUCCESS.Enum(), playerInfo, a)

	room.EnterGame(a, int32(111))
}
