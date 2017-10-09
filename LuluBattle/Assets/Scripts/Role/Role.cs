using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTransmit;

public class Role
{
    public int _playerId;
    public string _headIcon;
    public string _nickName;

    public Vector2 _pos;
    public float _rot;

    public void UpdateInfo(pb.PlayerInfo info)
    {
        _playerId = info.OID;
        _headIcon = info.HeadIcon;
        _nickName = info.NickName;
    }

    public void UpdateTrs(pb.RoleTrs trs)
    {
        _pos.x = trs.pos_x / 100.0f;
        _pos.y = trs.pos_y / 100.0f;
        _rot = trs.rot / 100.0f;
    }
    

}
