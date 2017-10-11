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
	PlayerId int32
	Name     string
	HeadIcon string
	Attr     *RoleAttr
	RoomId   string
}

func (role *Role) InitInfo() {
	role.Name = "lulu"
	role.HeadIcon = "lulu"
}

func (role *Role) InitAttr() {
	attr := &RoleAttr{}
	attr.Hp = 1000
	role.Attr = attr
}
