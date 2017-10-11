using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTransmit;

public enum BattleProcess
{
    Default,
    ExchangCard,
    ExchangCardOver,

    Lack,
    LackOver,

    Discard,
    DiscardOver,

    SelfGangChoose,
    ProcEnsureOver,
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

    public Dictionary<uint, Role> RoomPlayersInfo = new Dictionary<uint, Role>();

    public void AddNewPlayer(pb.GS2CEnterGame data)
    {
        Role role = new Role();
        role.InitRole(data);
        RoomPlayersInfo.Add(data.PlayerID, role);
    }

}
