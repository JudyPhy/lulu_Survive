package internal

import (
	"reflect"
	"server/msgSendHandler"
	"server/pb"
	"server/role"
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
	errorCode, playerInfo := role.PlayerLogin(a, m.GetAccount(), m.GetPassword())
	msgSendHandler.SendGS2CLoginRet(errorCode.Enum(), playerInfo, a)
}

func recvC2GSChooseRole(args []interface{}) {
	m := args[0].(*pb.C2GSChooseRole)
	a := args[1].(gate.Agent)
	log.Debug("C2GSChooseRole <<<<<<======== nickname=%v, headicon=%v", m.GetNickName(), m.GetHeadIcon())
	errorCode, playerInfo := role.ChooseRole(a, m.GetNickName(), m.GetHeadIcon())
	msgSendHandler.SendGS2CChooseRoleRet(errorCode.Enum(), playerInfo, a)
}
