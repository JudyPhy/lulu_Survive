package player

import (
	"server/db"
	"server/pb"

	"code.google.com/p/goprotobuf/proto"
	"github.com/go-xorm/cmd/xorm/model"
	"github.com/name5566/leaf/log"
)

func Create(oid int) *pb.PlayerInfo {
	log.Debug("create new player info")
	p := new(model.Playerinfo)
	p.Id = oid
	p.Nickname = "Name"
	p.Headicon = "default"
	p.Lev = 1
	p.Exp = 0
	p.Gold = 0
	p.Diamond = 0
	_, err := db.DBEngine.Insert(p)
	if err != nil {
		log.Debug("insert new player failed")
	}

	ret := &pb.PlayerInfo{}
	ret.OID = proto.Int(p.Id)
	ret.NickName = proto.String(p.Nickname)
	ret.HeadIcon = proto.String(p.Headicon)
	ret.Lev = proto.Int(p.Lev)
	ret.Exp = proto.Int(p.Exp)
	ret.Gold = proto.Int(p.Gold)
	ret.Diamond = proto.Int(p.Diamond)
	return ret
}

func Req(oid int) *pb.PlayerInfo {
	log.Debug("request existed player[%v] info", oid)
	p := new(model.Playerinfo)
	result, err := db.DBEngine.Where("Id=?", oid).Get(p)
	if err != nil {
		log.Error("err: %v", err)
	}
	if result {
		ret := &pb.PlayerInfo{}
		ret.OID = proto.Int(p.Id)
		ret.NickName = proto.String(p.Nickname)
		ret.HeadIcon = proto.String(p.Headicon)
		ret.Lev = proto.Int(p.Lev)
		ret.Exp = proto.Int(p.Exp)
		ret.Gold = proto.Int(p.Gold)
		ret.Diamond = proto.Int(p.Diamond)
		return ret
	}
	return nil
}
