package role

import (
	"server/pb"

	"code.google.com/p/goprotobuf/proto"
	"github.com/name5566/leaf/gate"
	//	"github.com/name5566/leaf/log"
)

type PlayerInfo struct {
	OID      uint32
	NickName string
	HeadIcon string
	RoomID   string
}

var PlayerMap map[gate.Agent]*PlayerInfo

func PlayerLogin(a gate.Agent, account string, password string) (pb.ErrorCode, *pb.PlayerInfo) {
	if PlayerMap == nil {
		PlayerMap = make(map[gate.Agent]*PlayerInfo)
	}
	if playerExist(account) {
		if checkPassword(password) {
			player := &PlayerInfo{}
			player.OID = 1000
			player.NickName = "lulu"
			player.HeadIcon = "icon_lulu"
			PlayerMap[a] = player
			return pb.ErrorCode_SUCCESS, player.ToPbData()
		} else {
			return pb.ErrorCode_FAIL, nil
		}
	} else {
		//Add account to db
		player := &PlayerInfo{}
		player.OID = 1000
		player.NickName = ""
		player.HeadIcon = ""
		player.RoomID = ""
		PlayerMap[a] = player
		return pb.ErrorCode_SUCCESS, nil
	}
}

func ChooseRole(a gate.Agent, nickName string, headIcon string) (pb.ErrorCode, *pb.PlayerInfo) {
	_, ok := PlayerMap[a]
	if !ok {
		return pb.ErrorCode_FAIL, nil
	}
	//Update db info
	PlayerMap[a].NickName = nickName
	PlayerMap[a].HeadIcon = headIcon
	return pb.ErrorCode_SUCCESS, PlayerMap[a].ToPbData()
}

func checkPassword(password string) bool {
	return true
}

func playerExist(account string) bool {
	return false
}

func (player *PlayerInfo) ToPbData() *pb.PlayerInfo {
	pbData := &pb.PlayerInfo{}
	pbData.OID = proto.Uint32(player.OID)
	pbData.NickName = proto.String(player.NickName)
	pbData.HeadIcon = proto.String(player.HeadIcon)
	return pbData
}
