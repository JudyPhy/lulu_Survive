/*package mgrDB

import (
	"time"

	_ "github.com/go-sql-driver/mysql"
	"github.com/go-xorm/core"
	"github.com/go-xorm/xorm"

	"github.com/name5566/leaf/log"
)

type AccInfo struct {
	Acc       string `xorm:"pk"`
	Psw       string
	Pid       int32
	Version   int       `xorm:"version"`
	UpdatedAt time.Time `xorm:"updated"`
}

type PlayerInfo struct {
	Pid       int32 `xorm:"pk autoincr"`
	NickName  string
	HeadIcon  string
	Gold      int32
	Fangka    int32
	Version   int       `xorm:"version"`
	UpdatedAt time.Time `xorm:"updated"`
}

var db *xorm.Engine

func init() {
	return
	log.Debug("dbMgr init...")
	var err error
	db, err = xorm.NewEngine("mysql", "root:zFm826423@/mahjon_db?charset=utf8")
	db.SetTableMapper(core.SameMapper{})
	if err != nil {
		log.Debug("create db_engine Failed :", err)
	}
	err = db.Sync2(new(AccInfo), new(PlayerInfo))
	if err != nil {
		log.Debug("sync to db Failed :", err)
	}
}

func regAcc(acc string, psw string) *AccInfo {
	newPlayer := insertRole(acc)
	if newPlayer == nil {
		return nil
	}
	newUser := &AccInfo{
		Acc: acc,
		Psw: psw,
		Pid: newPlayer.Pid,
	}
	_, err := db.Insert(&newUser)
	if err != nil {
		log.Debug("Insert User Failed:", err)
		return nil
	}
	return newUser
}

func insertRole(name string) *PlayerInfo {
	newPlayer := &PlayerInfo{
		NickName: "nm" + name,
		HeadIcon: "",
		Gold:     int32(0),
		Fangka:   int32(0),
	}
	_, err := db.Insert(newPlayer)
	if err != nil {
		log.Debug("Insert Role Failed:", err)
		return nil
	}
	return newPlayer
}*/
