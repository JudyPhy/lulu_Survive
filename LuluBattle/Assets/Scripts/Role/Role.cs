using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTransmit;

public class Role
{
    public uint PlayerID;
    public string HeadIcon;
    public string Nickname;

    public Vector2 BornPos;

    private pb.MoveStatus _moveStatus;
    public pb.MoveStatus MoveStatus { get { return _moveStatus; } }

    private uint _rot;
    public uint Rot { get { return _rot; } }

    private uint _hp;
    public uint Hp { get { return _hp; } set { _hp = value; } }

    public void InitRole(pb.GS2CEnterGame data)
    {
        PlayerID = data.PlayerID;
        Nickname = data.Nickname;
        HeadIcon = data.HeadIcon;
        Hp = data.Hp;
        BornPos = new Vector2(data.PosX, data.PosY);
    }

    public void UpdateCurInfo(Role info)
    {
        _hp = info.Hp;
        _moveStatus = info.MoveStatus;
        _rot = info.Rot;
    }

    public void UpdateFrameInfo(pb.FrameRoleData info)
    {
        PlayerID = info.PlayerID;
        _hp = info.Hp;
        _moveStatus = info.Move.Status;
        _rot = info.Move.Rot;
    }

   


}
