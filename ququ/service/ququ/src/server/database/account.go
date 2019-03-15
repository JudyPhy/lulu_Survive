package database

import (
	"math/rand"
	"time"

	_ "github.com/go-sql-driver/mysql"
	"github.com/name5566/leaf/log"
)

type Account struct {
	Id        int64  `xorm:"not null pk autoincr"`
	Name      string `xorm:"unique"`
	Password  string
	UpdatedAt time.Time `xorm:"updated"`
}

func syncAccount() {
	if err := engine.Sync2(new(Account)); err != nil {
		log.Error("Fail to sync database: %v\n", err)
	}
}

func getPlayerId() (int64, error) {
	rnd := rand.New(rand.NewSource(time.Now().UnixNano()))
	id := rnd.Int63n(1000000)
	a := new(Account)
	has, err := engine.Id(id).Get(a)
	if err != nil {
		return 0, err
	} else {
		if has {
			return getPlayerId()
		} else {
			return id, nil
		}
	}
}

func NewAccount(name string, password string) (int64, error) {
	log.Debug("NewAccount: %v", name)
	id, err := getPlayerId()
	if err != nil {
		return 0, err
	} else {
		account := &Account{Id: id, Name: name, Password: password}
		sess := engine.NewSession()
		defer sess.Close()
		if err = sess.Begin(); err != nil {
			return 0, err
		}
		if _, err = sess.Insert(account); err != nil {
			sess.Rollback()
			return 0, err
		}
		sess.Commit()
		return id, nil
	}
}

func GetAccount(name string) (*Account, error) {
	account := &Account{Name: name}
	has, err := engine.Get(account)
	if err == nil {
		if has {
			return account, nil
		} else {
			return nil, nil
		}
	} else {
		return nil, err
	}
}
