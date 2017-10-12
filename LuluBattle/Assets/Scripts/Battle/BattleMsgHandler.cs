using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class BattleMsgHandler
{

    private static BattleMsgHandler instance;
    public static BattleMsgHandler Instance {
        get {
            if (instance == null) {
                instance = new BattleMsgHandler();
            }
            return instance;
        }
    }

    #region C -> GS

    public void SendMsgC2GSStartGame(pb.GameMode mode, int mapId, int roleId)
    {
        pb.C2GSStartGame msg = new pb.C2GSStartGame();
        msg.Mode = mode;
        msg.MapID = mapId;
        msg.RoleID = roleId;
        Debug.Log("C2GSLogin ============>>>>>>>>>> mode:" + msg.Mode + ", map:" + msg.MapID + ", role:" + msg.RoleID);
        NetworkManager.Instance.SendToGS((UInt16)MsgDef.C2GSStartGame, msg);
    }

    #endregion


    #region GS -> C

    public void RevMsgGS2CStartGameRet(int pid, byte[] msgBuf, int msgSize)
    {
        Stream stream = new MemoryStream(msgBuf);
        pb.GS2CStartGameRet msg = ProtoBuf.Serializer.Deserialize<pb.GS2CStartGameRet>(stream);
        Debug.Log("GS2CStartGameRet ============>>>>>>>>>> mode:" + msg.Mode + ", mapId:" + msg.MapID +
            ", bornInfo:" + msg.Players.Count + ", expInfo:" + msg.Exp.Count + ", buffInfo:" + msg.Buff.Count);
        BattleManager.Instance.GameStart(msg);
    }

    #endregion
}
