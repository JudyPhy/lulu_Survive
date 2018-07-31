package user

import (
	"math/rand"
	"server/db"
	"server/pb"
	"server/player"
	"time"

	"github.com/go-xorm/cmd/xorm/model"
	"github.com/name5566/leaf/gate"
	"github.com/name5566/leaf/log"
)

var UserMap map[gate.Agent]*model.User

func UserLogin(a gate.Agent, deviceId string) *pb.GS2CLoginRet {
	ret := &pb.GS2CLoginRet{}
	if UserMap == nil {
		UserMap = make(map[gate.Agent]*model.User)
	}
	exist, usr := userExist(deviceId)
	if exist {
		ret.PlayerInfo = player.Req(usr.Id)
		log.Debug("player info:%v", ret.PlayerInfo)
		if ret.PlayerInfo == nil {
			ret.ErrorCode = pb.ErrorCode_FAIL.Enum()
		} else {
			ret.ErrorCode = pb.ErrorCode_SUCCESS.Enum()
		}
	} else {
		//Add user to db
		usr := new(model.User)
		usr.Id = getOID()
		usr.Name = ""
		usr.Deviceid = deviceId
		_, err := db.DBEngine.Insert(usr)
		if err == nil {
			UserMap[a] = usr
			ret.ErrorCode = pb.ErrorCode_SUCCESS.Enum()
			ret.PlayerInfo = player.Create(usr.Id)
		} else {
			ret.ErrorCode = pb.ErrorCode_FAIL.Enum()
		}
	}
	return ret
}

func userExist(deviceId string) (bool, *model.User) {
	usr := new(model.User)
	result, err := db.DBEngine.Where("Deviceid=?", deviceId).Get(usr)
	if err != nil {
		log.Error("err: %v", err)
	}
	return result, usr
}

func getOID() int {
	r := rand.New(rand.NewSource(time.Now().UnixNano()))
	oid := 0
	prefix := 1
	for i := 0; i < 6; i++ {
		oid += r.Intn(10) * prefix
		prefix = prefix * 10
	}
	log.Debug("create new user oid:%v", oid)

	usr := new(model.User)
	result, err := db.DBEngine.Where("Id=?", oid).Get(usr)
	if err != nil {
		log.Error("err: %v", err)
		return getOID()
	}
	if result {
		return getOID()
	} else {
		return oid
	}
}
