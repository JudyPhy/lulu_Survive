/*package mgrDB

import (
	_ "github.com/go-sql-driver/mysql"

	"github.com/name5566/leaf/log"
)

func getDBPlayerInfo(pid int32) *PlayerInfo {
	var player *PlayerInfo
	_, err := db.Id(pid).Get(&player)
	if err != nil {
		log.Debug("Role", pid, "non-existent:", err)
		player = nil
	}
	return player
}

func getDBUserInfo(acc string, psw string) (int32, bool) {
	var user *AccInfo
	has, err := db.Id(acc).Get(&user)
	if !has {
		log.Debug("Account %v ;non-existent: %v", acc, err)
		user = regAcc(acc, psw)
		if user == nil {
			return -1, false
		}
	} else {
		if psw != user.Psw {
			return -1, false
		}
	}
	log.Debug("getUserInfo %v", user.Pid)
	return user.Pid, true
}

//-------------------无数据库test-------------------------

func getTestUserInfo(acc string, psw string) (int32, bool) {
	log.Debug("无数据库test==>[acc=%v][psw=%v]", acc, psw)
	return int32(911), true
}

func getTestPlayerInfo(pid int32) PlayerInfo {
	player := PlayerInfo{
		Pid:      pid,
		NickName: "test911",
		HeadIcon: "",
		Gold:     int32(0),
		Fangka:   int32(0),
	}

	return player
}*/
