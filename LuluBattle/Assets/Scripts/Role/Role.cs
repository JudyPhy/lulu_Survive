using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTransmit;

public class Role
{
    public uint OID;
    public string HeadIcon;
    public string Nickname;

    //Battle
    private int _roleType;
    public int RoleType { get { return _roleType; } set { _roleType = value; } }

    public Vector2 BornPos;

    private pb.MoveStatus _moveStatus;
    public pb.MoveStatus MoveStatus { get { return _moveStatus; } }    

    private uint _rot;
    public uint Rot { get { return _rot; } }

    private RoleAttr _attr;
    public RoleAttr Attr { get { return _attr; } }

    public void UpdateFrameInfo(pb.FrameRoleData info)
    {
        OID = info.PlayerID;
        _hp = info.Hp;
        _moveStatus = info.Move.Status;
        _rot = info.Move.Rot;
    }

}

public class RoleAttr
{
    private uint _hp;
    public uint Hp { get { return _hp; } }

    public void InitAttr(pb.BaseAttr attr)
    {
        _hp = attr.Hp;
    }
}
