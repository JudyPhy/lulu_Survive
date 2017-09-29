using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TimeSyncMsgHandler
{
    private static TimeSyncMsgHandler instance;
    public static TimeSyncMsgHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TimeSyncMsgHandler();
            }
            return instance;
        }
    }

    #region C -> GS
    public void SendMsgC2GSReqSyncTime(long time)
    {
        Debug.Log("==>> SendMsgC2GSReqSyncTime, time=" + time);
        pb.C2GSReqSyncTime msg = new pb.C2GSReqSyncTime();
        msg.time = time;
        NetworkManager.Instance.SendToGS((UInt16)MsgDef.C2GSReqSyncTime, msg);
    }

    public void SendMsgC2GSMove()
    {
        Debug.Log("==>> SendMsgC2GSMove, time=");
        pb.C2GSMove msg = new pb.C2GSMove();
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        msg.time = Convert.ToInt64(ts.TotalSeconds);
        NetworkManager.Instance.SendToGS((UInt16)MsgDef.C2GSMove, msg);
    }
    #endregion

    #region GS -> C
    public void RevMsgGS2CRevSyncTime(int pid, byte[] msgBuf, int msgSize)
    {
        Debug.Log("==>> RevMsgGS2CRevSyncTime");
        Stream stream = new MemoryStream(msgBuf);
        pb.C2GSReqSyncTime msg = ProtoBuf.Serializer.Deserialize<pb.C2GSReqSyncTime>(stream);
        TimeSync.RevSyncTime(msg.time);
    }

    public void RevMsgGS2CSyncTimeAgain(int pid, byte[] msgBuf, int msgSize)
    {
        Debug.Log("==>> RevMsgGS2CSyncTimeAgain");
        if (TimeSync.syncOver)
        {
            TimeSync.ReqSyncTime();
        }
    }
    #endregion
}
