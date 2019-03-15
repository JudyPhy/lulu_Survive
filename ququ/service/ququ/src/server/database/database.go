package database

import (
	_ "github.com/go-sql-driver/mysql"
	"github.com/go-xorm/xorm"
	"github.com/name5566/leaf/log"
)

var engine *xorm.Engine

func init() {
	log.Debug("database init")
	var err error
	engine, err = xorm.NewEngine("mysql", "root:123456@(127.0.0.1:3306)/ququ?charset=utf8")
	if err != nil {
		log.Error("xorm engine error:%v", err)
		return
	}
	//	engine.ShowSQL(true) // 显示SQL的执行, 便于调试分析

	syncAccount()
	syncPlayerInfo()
}
