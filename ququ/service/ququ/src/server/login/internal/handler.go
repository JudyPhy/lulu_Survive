package internal

import (
	"reflect"
	"server/database"
	"server/pb"
	"server/playerManager"

	"github.com/golang/protobuf/proto"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

func handleMsg(m interface{}, h interface{}) {
	skeleton.RegisterChanRPC(reflect.TypeOf(m), h)
}

func init() {
	// 向当前模块注册消息处理函数
	handleMsg(&pb.C2GSLogin{}, recvC2GSLogin)
}

func recvC2GSLogin(args []interface{}) {
	log.Debug("recvC2GSLogin=>%v", args)
	m := args[0].(*pb.C2GSLogin)
	a := args[1].(gate.Agent)

	// get data from db
	account, err := database.GetAccount(m.GetUser())
	if err != nil {
		log.Debug("account database error")
		sendGS2CLoginRet(a, nil, pb.GS2CLoginRet_Fail.Enum()) //account database error
	} else {
		if account == nil {
			id, err := database.NewAccount(m.GetUser(), m.GetPassword())
			if err != nil {
				log.Debug("create new account error")
				sendGS2CLoginRet(a, nil, pb.GS2CLoginRet_Fail.Enum()) //create new account error
			} else {
				player, err := database.NewPlayerInfo(id)
				if err == nil {
					playerManager.UpdatePlayerAgent(a, player.Id)
					sendGS2CLoginRet(a, toPbPlayerInfo(player), pb.GS2CLoginRet_Success.Enum())
				} else {
					log.Debug("create new player info error")
					sendGS2CLoginRet(a, nil, pb.GS2CLoginRet_Fail.Enum()) //create new player info error
				}
			}
		} else {
			if account.Password != m.GetPassword() {
				log.Debug("password error")
				sendGS2CLoginRet(a, nil, pb.GS2CLoginRet_Fail.Enum()) //password error
			} else {
				player, err := database.GetPlayerInfo(account.Id)
				if err != nil {
					log.Debug("playerinfo database error")
					sendGS2CLoginRet(a, nil, pb.GS2CLoginRet_Fail.Enum()) //playerinfo database error
				} else {
					if player == nil {
						log.Debug("account[%v] has no role, create new role", account.Name)
						player, err := database.NewPlayerInfo(account.Id)
						if err == nil {
							playerManager.UpdatePlayerAgent(a, player.Id)
							sendGS2CLoginRet(a, toPbPlayerInfo(player), pb.GS2CLoginRet_Success.Enum())
						} else {
							log.Debug("create new player info error")
							sendGS2CLoginRet(a, nil, pb.GS2CLoginRet_Fail.Enum()) //create new player info error
						}
					} else {
						playerManager.UpdatePlayerAgent(a, player.Id)
						sendGS2CLoginRet(a, toPbPlayerInfo(player), pb.GS2CLoginRet_Success.Enum())
					}
				}
			}
		}
	}

	//	chanPlayer := roomMgr.NewPlayer(player)
	//	roomMgr.AddChanPlayerInfo(a, chanPlayer)
}

func toPbPlayerInfo(info *database.PlayerInfo) *pb.PlayerInfo {
	player := &pb.PlayerInfo{}
	player.PlayerId = proto.Int64(info.Id)
	player.Nickname = proto.String(info.Nickname)
	player.Headicon = proto.String(info.Headicon)
	player.Card = proto.Int64(info.Card)
	player.Coin = proto.Int64(info.Coin)
	return player
}

func sendGS2CLoginRet(a gate.Agent, player *pb.PlayerInfo, errorCode *pb.GS2CLoginRet_ErrorCode) {
	log.Debug("sendGS2CLoginRet: %v", player)
	ret := &pb.GS2CLoginRet{}
	ret.ErrorCode = errorCode
	ret.User = player
	a.WriteMsg(ret)
}
