using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTransmit;

public class Player : Role
{

    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Player();
            }
            return instance;
        }
    }

    public void UpdateInfo(pb.PlayerInfo info)
    {
        OID = info.OID;
        Nickname = info.NickName;
        HeadIcon = info.HeadIcon;
    }


}
