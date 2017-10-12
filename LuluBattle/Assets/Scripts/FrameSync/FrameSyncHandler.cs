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
        Stream stream = new MemoryStream(msgBuf);
        pb.GSSyncPkgSend msg = ProtoBuf.Serializer.Deserialize<pb.GSSyncPkgSend>(stream);
        Debug.Log("GSSyncPkgSend ============>>>>>>>>>> act:" + msg.Act+", time:"+ (System.DateTime.Now - new System.DateTime(1970, 1, 1, 8, 0, 0)).TotalMilliseconds);
        FrameSync.Instance.RecvSyncPkg(msg);
    }

    #endregion

}
