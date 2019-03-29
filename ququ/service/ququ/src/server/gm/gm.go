package gm

import (
	"server/database"
	"server/pb"
	"server/playerManager"

	"github.com/golang/protobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func RecvC2GSGMAddCoin(args []interface{}) {
	log.Debug("RecvC2GSGMAddCoin=>%v", args)
	m := args[0].(*pb.C2GSGMAddCoin)
	a := args[1].(gate.Agent)
	playerId := playerManager.GetPlayerId(a)
	if playerId == 0 {
		log.Error("can't find playerid of agent[%v]", a)
	} else {
		playerInfo, _ := database.GetPlayerInfo(playerId)
		if playerInfo != nil {
			playerInfo.Coin += m.GetValue()
			database.UpdatePlayerInfo(playerInfo)

			newPlayerInfo, _ := database.GetPlayerInfo(playerId)
			SendGS2CGMAddCoinRet(a, newPlayerInfo)
		}
	}
}

func SendGS2CGMAddCoinRet(a gate.Agent, info *database.PlayerInfo) {
	log.Debug("SendGS2CGMAddCoinRet=>%v", info)
	ret := &pb.GS2CGMAddCoinRet{}
	ret.User = &pb.PlayerInfo{}
	ret.User.PlayerId = proto.Int64(info.Id)
	ret.User.Nickname = proto.String(info.Nickname)
	ret.User.Headicon = proto.String(info.Headicon)
	ret.User.Card = proto.Int64(info.Card)
	ret.User.Coin = proto.Int64(info.Coin)
	a.WriteMsg(ret)
}
