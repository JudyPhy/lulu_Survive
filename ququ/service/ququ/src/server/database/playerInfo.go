package database

import (
	"strconv"
	"time"

	_ "github.com/go-sql-driver/mysql"
	"github.com/name5566/leaf/log"
)

type PlayerInfo struct {
	Id        int64 `xorm:"unique not null pk autoincr"`
	Nickname  string
	Headicon  string
	Card      int64
	Coin      int64
	UpdatedAt time.Time `xorm:"updated"`
}

func syncPlayerInfo() {
	if err := engine.Sync2(new(PlayerInfo)); err != nil {
		log.Error("Fail to sync database: %v\n", err)
	}
}

func NewPlayerInfo(id int64) (*PlayerInfo, error) {
	log.Debug("NewPlayerInfo: %v", id)
	player := &PlayerInfo{Id: id, Nickname: "yk_" + strconv.FormatInt(id, 10), Headicon: "", Card: 0, Coin: 0}
	sess := engine.NewSession()
	defer sess.Close()
	if err := sess.Begin(); err != nil {
		return nil, err
	}
	if _, err := sess.Insert(player); err != nil {
		sess.Rollback()
		return nil, err
	}
	sess.Commit()
	return player, nil
}

func GetPlayerInfo(id int64) (*PlayerInfo, error) {
	player := &PlayerInfo{Id: id}
	has, err := engine.Get(player)
	if err == nil {
		if has {
			return player, nil
		} else {
			return nil, nil
		}
	} else {
		return nil, err
	}
}

func UpdatePlayerInfo(info *PlayerInfo) error {
	log.Debug("UpdatePlayerInfo: %v", info)
	sess := engine.NewSession()
	defer sess.Close()
	if err := sess.Begin(); err != nil {
		log.Error("sess begin error[%v]", err)
		return err
	}
	if _, err := sess.Exec("update player_info set card = ?,coin = ? where id = ?", info.Card, info.Coin, info.Id); err != nil {
		sess.Rollback()
		log.Error("update error, rollback[%v]", err)
		return err
	}
	sess.Commit()
	return nil
}
