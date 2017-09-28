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

    public void RevMsgC2GSReqSyncTime(int pid, byte[] msgBuf, int msgSize)
    {
        Debug.Log("==>> RevMsgC2GSReqSyncTime");
        Stream stream = new MemoryStream(msgBuf);
        pb.C2GSReqSyncTime msg = ProtoBuf.Serializer.Deserialize<pb.C2GSReqSyncTime>(stream);
        TimeSync.RevSyncTime(msg.time);
    }

}
