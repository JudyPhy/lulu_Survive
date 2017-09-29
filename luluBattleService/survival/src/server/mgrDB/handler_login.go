/*package mgrDB

import (
	"github.com/golang/protobuf/proto"
	"github.com/name5566/leaf/gate"

	"server/mgrPlayer"
	"server/pb"
)

func LoginHandler(acc string, psw string, a gate.Agent) (*pb.GS2CLoginRet_ErrorCode, *pb.PlayerInfo) {
	pid, ok := getDBUserInfo(acc, psw)
	if !ok {
		return pb.GS2CLoginRet_PASSWORD_ERROR.Enum(), nil
	} else {
		role := getDBPlayerInfo(pid)
		if role == nil {
			return pb.GS2CLoginRet_PASSWORD_ERROR.Enum(), nil
		} else {
			player := &pb.PlayerInfo{
				OID:      proto.Int32(role.Pid),
				NickName: proto.String(role.NickName),
				HeadIcon: proto.String(role.HeadIcon),
				Gold:     proto.Int32(role.Gold),
				Fangka:   proto.Int32(role.Fangka),
			}
			mgrPlayer.Binda2pMap(a, player)
			return pb.GS2CLoginRet_SUCCESS.Enum(), player
		}
	}
}*/
