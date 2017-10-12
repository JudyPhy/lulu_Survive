using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTransmit;

public enum BattleProcess
{
    Default,
    Start,
}


public class BattleManager
{
    private static BattleManager _instance;
    public static BattleManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new BattleManager();
            return _instance;
        }
    }

    private pb.GameMode _mode;
    public pb.GameMode GameMode { get { return _mode; } }

    private int _mapId;
    public int MapID { get { return _mapId; } }

    private BattleProcess _process;
    public BattleProcess Process { get { return _process; } }

    private List<Vector2> _expPos = new List<Vector2>();
    public List<Vector2> ExpPos { get { return _expPos; } }

    private List<Vector2> _buffPos = new List<Vector2>();
    public List<Vector2> BuffPos { get { return _buffPos; } }

    private Dictionary<uint, Role> _players = new Dictionary<uint, Role>();
    public Dictionary<uint, Role> Players { get { return _players; } }

    public void GameStart(pb.GS2CStartGameRet msg)
    {
        _mode = msg.Mode;
        _mapId = msg.MapID;

        //exp
        for (int i = 0; i < msg.Exp.Count; i++)
        {
            Vector2 pos = new Vector2(msg.Exp[i].pos_x, msg.Exp[i].pos_y);
            _expPos.Add(pos);
        }

        //buff
        for (int i = 0; i < msg.Buff.Count; i++)
        {
            Vector2 pos = new Vector2(msg.Buff[i].pos_x, msg.Buff[i].pos_y);
            _buffPos.Add(pos);
        }

        //players born
        for (int i = 0; i < msg.Players.Count; i++)
        {
            pb.BornInfo info = msg.Players[i];
            Role role = new Role();
            if (info.OID == Player.Instance.OID)
            {
                role = Player.Instance;
            }
            else
            {
                role.OID = info.OID;
                role.Nickname = info.NickName;
            }
            role.RoleType = info.RoleID;
            role.Attr.InitAttr(info.Attr);
            role.BornPos = new Vector2(info.PosX, info.PosY);
            Players.Add(role.OID, role);
        }

        _process = BattleProcess.Start;
    }

}
