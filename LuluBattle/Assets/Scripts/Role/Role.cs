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

    private Dictionary<uint, pb.FrameData> _frameList = new Dictionary<uint, pb.FrameData>();

    public void InitAttr(pb.BaseAttr attr)
    {
        _attr = new RoleAttr();
        _attr.InitAttr(attr);
    }

    public void UpdateFrameInfo(List<pb.FrameData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (!_frameList.ContainsKey(list[i].Index))
            {
                _frameList.Add(list[i].Index, list[i]);
            }
        }
    }

    public void RemoveFrame(uint index)
    {
        _frameList.Remove(index);
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

    public pb.BaseAttr ToPbAttr()
    {
        pb.BaseAttr attr = new pb.BaseAttr();
        attr.Hp = _hp;
        return attr;
    }
}
