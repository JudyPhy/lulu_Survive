package msgSendHandler

import (
	"server/pb"
	"server/role"

	"code.google.com/p/goprotobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func SendGS2CLoginRet(errorCode *pb.ErrorCode, playerInfo *pb.PlayerInfo, a gate.Agent) {
	log.Debug("GS2CLoginRet ========>>>>>> ")
	data := &pb.GS2CLoginRet{}
	data.ErrorCode = errorCode
	data.PlayerInfo = playerInfo
	a.WriteMsg(data)
}

func SendGS2CChooseRoleRet(errorCode *pb.ErrorCode, playerInfo *pb.PlayerInfo, a gate.Agent) {
	log.Debug("SendGS2CChooseRoleRet ========>>>>>> ")
	data := &pb.GS2CChooseRoleRet{}
	data.ErrorCode = errorCode
	data.PlayerInfo = playerInfo
	a.WriteMsg(data)
}

func SendGS2CEnterGame(player *role.Role, a gate.Agent) {
	log.Debug("GS2CEnterGame ========>>>>>> ")
	data := &pb.GS2CEnterGame{}
	data.RoomID = proto.String(player.RoomId)
	data.PlayerID = proto.Int32(player.PlayerId)
	data.Nickname = proto.String(player.Name)
	data.HeadIcon = proto.String(player.HeadIcon)
	data.Hp = proto.Uint32(player.Attr.Hp)
	data.PosX = proto.Int32(0)
	data.PosY = proto.Int32(0)
	a.WriteMsg(data)
}
