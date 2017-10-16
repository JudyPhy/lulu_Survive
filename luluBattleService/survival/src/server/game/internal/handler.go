package internal

import (
	"reflect"
	"server/pb"
	"server/room"

	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func init() {
}

func handler(m interface{}, h interface{}) {
	skeleton.RegisterChanRPC(reflect.TypeOf(m), h)
}

func init() {
	handler(&pb.C2GSStartGame{}, recvC2GSStartGame)
	handler(&pb.C2GSSyncPkg{}, recvC2GSSyncPkg)
}

func recvC2GSStartGame(args []interface{}) {
	m := args[0].(*pb.C2GSStartGame)
	a := args[1].(gate.Agent)
	log.Debug("C2GSStartGame <<<<<<======== mode=%v, mapId=%v, roleId=%v", m.GetMode(), m.GetMapID(), m.GetRoleID())
	room.EnterRoom(a, m.GetMode(), m.GetMapID(), m.GetRoleID())
}

func recvC2GSSyncPkg(args []interface{}) {
	m := args[0].(*pb.C2GSSyncPkg)
	a := args[1].(gate.Agent)
	log.Debug("C2GSSyncPkg <<<<<<======== clientAct=%v, serviceAct=%v", m.GetClientAct(), m.GetAct())
	if m.GetClientAct() != 0 {
		room.AddNewFrameData(a, m)
	}
	room.RemoveShownedFrameData(a, m.GetAct())
}
