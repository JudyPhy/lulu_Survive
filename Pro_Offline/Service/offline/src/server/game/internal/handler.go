package internal

import (
	"reflect"
	//	"server/pb"

	//"github.com/name5566/leaf/gate"
	//"github.com/name5566/leaf/log"
)

func init() {
}

func handler(m interface{}, h interface{}) {
	skeleton.RegisterChanRPC(reflect.TypeOf(m), h)
}

func init() {
}

func recvC2GSStartGame(args []interface{}) {
	//m := args[0].(*pb.C2GSStartGame)
	//a := args[1].(gate.Agent)
	//log.Debug("C2GSStartGame <<<<<<======== mode=%v, mapId=%v, roleId=%v", m.GetMode(), m.GetMapID(), m.GetRoleID())
	//room.EnterRoom(a, m.GetMode(), m.GetMapID(), m.GetRoleID())
}
