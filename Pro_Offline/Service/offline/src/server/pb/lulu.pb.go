// Code generated by protoc-gen-go.
// source: lulu.proto
// DO NOT EDIT!

/*
Package pb is a generated protocol buffer package.

It is generated from these files:
	lulu.proto

It has these top-level messages:
	PlayerInfo
	Reward
	ItemData
	C2GSLogin
	GS2CLoginRet
	C2GSSwitchMap
	GS2CSwitchMapRet
	GS2CBattleInfo
*/
package pb

import proto "code.google.com/p/goprotobuf/proto"
import math "math"

// Reference imports to suppress errors if they are not otherwise used.
var _ = proto.Marshal
var _ = math.Inf

type ErrorCode int32

const (
	ErrorCode_SUCCESS ErrorCode = 1
	ErrorCode_FAIL    ErrorCode = 2
)

var ErrorCode_name = map[int32]string{
	1: "SUCCESS",
	2: "FAIL",
}
var ErrorCode_value = map[string]int32{
	"SUCCESS": 1,
	"FAIL":    2,
}

func (x ErrorCode) Enum() *ErrorCode {
	p := new(ErrorCode)
	*p = x
	return p
}
func (x ErrorCode) String() string {
	return proto.EnumName(ErrorCode_name, int32(x))
}
func (x *ErrorCode) UnmarshalJSON(data []byte) error {
	value, err := proto.UnmarshalJSONEnum(ErrorCode_value, data, "ErrorCode")
	if err != nil {
		return err
	}
	*x = ErrorCode(value)
	return nil
}

type PlayerInfo struct {
	OID              *int32    `protobuf:"varint,1,req" json:"OID,omitempty"`
	NickName         *string   `protobuf:"bytes,2,req" json:"NickName,omitempty"`
	HeadIcon         *string   `protobuf:"bytes,3,req" json:"HeadIcon,omitempty"`
	Lev              *int32    `protobuf:"varint,4,req" json:"Lev,omitempty"`
	Exp              *int32    `protobuf:"varint,5,req" json:"Exp,omitempty"`
	Gold             *int32    `protobuf:"varint,6,req" json:"Gold,omitempty"`
	Diamond          *int32    `protobuf:"varint,8,req" json:"Diamond,omitempty"`
	Items            *ItemData `protobuf:"bytes,7,opt" json:"Items,omitempty"`
	XXX_unrecognized []byte    `json:"-"`
}

func (m *PlayerInfo) Reset()         { *m = PlayerInfo{} }
func (m *PlayerInfo) String() string { return proto.CompactTextString(m) }
func (*PlayerInfo) ProtoMessage()    {}

func (m *PlayerInfo) GetOID() int32 {
	if m != nil && m.OID != nil {
		return *m.OID
	}
	return 0
}

func (m *PlayerInfo) GetNickName() string {
	if m != nil && m.NickName != nil {
		return *m.NickName
	}
	return ""
}

func (m *PlayerInfo) GetHeadIcon() string {
	if m != nil && m.HeadIcon != nil {
		return *m.HeadIcon
	}
	return ""
}

func (m *PlayerInfo) GetLev() int32 {
	if m != nil && m.Lev != nil {
		return *m.Lev
	}
	return 0
}

func (m *PlayerInfo) GetExp() int32 {
	if m != nil && m.Exp != nil {
		return *m.Exp
	}
	return 0
}

func (m *PlayerInfo) GetGold() int32 {
	if m != nil && m.Gold != nil {
		return *m.Gold
	}
	return 0
}

func (m *PlayerInfo) GetDiamond() int32 {
	if m != nil && m.Diamond != nil {
		return *m.Diamond
	}
	return 0
}

func (m *PlayerInfo) GetItems() *ItemData {
	if m != nil {
		return m.Items
	}
	return nil
}

type Reward struct {
	Exp              *int32      `protobuf:"varint,1,req" json:"Exp,omitempty"`
	Gold             *int32      `protobuf:"varint,2,req" json:"Gold,omitempty"`
	Items            []*ItemData `protobuf:"bytes,3,rep" json:"Items,omitempty"`
	XXX_unrecognized []byte      `json:"-"`
}

func (m *Reward) Reset()         { *m = Reward{} }
func (m *Reward) String() string { return proto.CompactTextString(m) }
func (*Reward) ProtoMessage()    {}

func (m *Reward) GetExp() int32 {
	if m != nil && m.Exp != nil {
		return *m.Exp
	}
	return 0
}

func (m *Reward) GetGold() int32 {
	if m != nil && m.Gold != nil {
		return *m.Gold
	}
	return 0
}

func (m *Reward) GetItems() []*ItemData {
	if m != nil {
		return m.Items
	}
	return nil
}

type ItemData struct {
	ID               *int32 `protobuf:"varint,1,req" json:"ID,omitempty"`
	Count            *int32 `protobuf:"varint,2,req" json:"Count,omitempty"`
	XXX_unrecognized []byte `json:"-"`
}

func (m *ItemData) Reset()         { *m = ItemData{} }
func (m *ItemData) String() string { return proto.CompactTextString(m) }
func (*ItemData) ProtoMessage()    {}

func (m *ItemData) GetID() int32 {
	if m != nil && m.ID != nil {
		return *m.ID
	}
	return 0
}

func (m *ItemData) GetCount() int32 {
	if m != nil && m.Count != nil {
		return *m.Count
	}
	return 0
}

// ///////////////////////////////////////////////////////////////////
type C2GSLogin struct {
	OID              *string `protobuf:"bytes,1,req" json:"OID,omitempty"`
	XXX_unrecognized []byte  `json:"-"`
}

func (m *C2GSLogin) Reset()         { *m = C2GSLogin{} }
func (m *C2GSLogin) String() string { return proto.CompactTextString(m) }
func (*C2GSLogin) ProtoMessage()    {}

func (m *C2GSLogin) GetOID() string {
	if m != nil && m.OID != nil {
		return *m.OID
	}
	return ""
}

type GS2CLoginRet struct {
	ErrorCode        *ErrorCode  `protobuf:"varint,1,req,name=errorCode,enum=pb.ErrorCode" json:"errorCode,omitempty"`
	PlayerInfo       *PlayerInfo `protobuf:"bytes,2,opt,name=playerInfo" json:"playerInfo,omitempty"`
	Reward           *Reward     `protobuf:"bytes,3,opt,name=reward" json:"reward,omitempty"`
	XXX_unrecognized []byte      `json:"-"`
}

func (m *GS2CLoginRet) Reset()         { *m = GS2CLoginRet{} }
func (m *GS2CLoginRet) String() string { return proto.CompactTextString(m) }
func (*GS2CLoginRet) ProtoMessage()    {}

func (m *GS2CLoginRet) GetErrorCode() ErrorCode {
	if m != nil && m.ErrorCode != nil {
		return *m.ErrorCode
	}
	return ErrorCode_SUCCESS
}

func (m *GS2CLoginRet) GetPlayerInfo() *PlayerInfo {
	if m != nil {
		return m.PlayerInfo
	}
	return nil
}

func (m *GS2CLoginRet) GetReward() *Reward {
	if m != nil {
		return m.Reward
	}
	return nil
}

type C2GSSwitchMap struct {
	MapID            *int32 `protobuf:"varint,1,req,name=mapID" json:"mapID,omitempty"`
	XXX_unrecognized []byte `json:"-"`
}

func (m *C2GSSwitchMap) Reset()         { *m = C2GSSwitchMap{} }
func (m *C2GSSwitchMap) String() string { return proto.CompactTextString(m) }
func (*C2GSSwitchMap) ProtoMessage()    {}

func (m *C2GSSwitchMap) GetMapID() int32 {
	if m != nil && m.MapID != nil {
		return *m.MapID
	}
	return 0
}

type GS2CSwitchMapRet struct {
	ErrorCode        *ErrorCode `protobuf:"varint,1,req,name=errorCode,enum=pb.ErrorCode" json:"errorCode,omitempty"`
	MapID            *int32     `protobuf:"varint,2,req,name=mapID" json:"mapID,omitempty"`
	XXX_unrecognized []byte     `json:"-"`
}

func (m *GS2CSwitchMapRet) Reset()         { *m = GS2CSwitchMapRet{} }
func (m *GS2CSwitchMapRet) String() string { return proto.CompactTextString(m) }
func (*GS2CSwitchMapRet) ProtoMessage()    {}

func (m *GS2CSwitchMapRet) GetErrorCode() ErrorCode {
	if m != nil && m.ErrorCode != nil {
		return *m.ErrorCode
	}
	return ErrorCode_SUCCESS
}

func (m *GS2CSwitchMapRet) GetMapID() int32 {
	if m != nil && m.MapID != nil {
		return *m.MapID
	}
	return 0
}

type GS2CBattleInfo struct {
	MosterID         *int32  `protobuf:"varint,1,req,name=mosterID" json:"mosterID,omitempty"`
	Result           *bool   `protobuf:"varint,2,req,name=result" json:"result,omitempty"`
	Reward           *Reward `protobuf:"bytes,3,opt,name=reward" json:"reward,omitempty"`
	XXX_unrecognized []byte  `json:"-"`
}

func (m *GS2CBattleInfo) Reset()         { *m = GS2CBattleInfo{} }
func (m *GS2CBattleInfo) String() string { return proto.CompactTextString(m) }
func (*GS2CBattleInfo) ProtoMessage()    {}

func (m *GS2CBattleInfo) GetMosterID() int32 {
	if m != nil && m.MosterID != nil {
		return *m.MosterID
	}
	return 0
}

func (m *GS2CBattleInfo) GetResult() bool {
	if m != nil && m.Result != nil {
		return *m.Result
	}
	return false
}

func (m *GS2CBattleInfo) GetReward() *Reward {
	if m != nil {
		return m.Reward
	}
	return nil
}

func init() {
	proto.RegisterEnum("pb.ErrorCode", ErrorCode_name, ErrorCode_value)
}
