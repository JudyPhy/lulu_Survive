package db

import (
	_ "github.com/go-sql-driver/mysql"
	"github.com/go-xorm/cmd/xorm/model"
	"github.com/go-xorm/core"
	"github.com/go-xorm/xorm"
	"github.com/name5566/leaf/log"
)

var DBEngine *xorm.Engine

func init() {
	log.Debug("db init...")
	var err error
	DBEngine, err = xorm.NewEngine("mysql", "root:root@/offline?charset=utf8")
	if err != nil {
		log.Error("create db_engine Failed :", err)
		return
	}
	DBEngine.SetTableMapper(core.SnakeMapper{})

	err = DBEngine.Sync2(new(model.User), new(model.Playerinfo))
	if err != nil {
		log.Debug("sync to db Failed :", err)
	}
}
