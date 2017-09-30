using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FrameSyncHandler : MonoBehaviour {

    private static FrameSyncHandler instance;
    public static FrameSyncHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FrameSyncHandler();
            }
            return instance;
        }
    }

    #region GS -> C
    public void RevMsgGSSyncPkgSend(int pid, byte[] msgBuf, int msgSize)
    {
        Debug.Log("==>> RevMsgGSSyncPkgSend");
        Stream stream = new MemoryStream(msgBuf);
        pb.GSSyncPkgSend msg = ProtoBuf.Serializer.Deserialize<pb.GSSyncPkgSend>(stream);
        FrameSync.Instance.RecvSyncPkg(msg);
    }
    #endregion

}
