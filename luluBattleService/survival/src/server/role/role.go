package role

import (
	//"code.google.com/p/goprotobuf/proto"
	"github.com/name5566/leaf/gate"
	//"github.com/name5566/leaf/log"
)

type RoleAttr struct {
	Hp uint32
}

type Role struct {
	A        gate.Agent
	Name     string
	RoleType int32
	Attr     *RoleAttr
	RoomId   string
}

func (role *Role) InitAttr() {
	attr := &RoleAttr{}
	attr.Hp = 1000
	role.Attr = attr
}
