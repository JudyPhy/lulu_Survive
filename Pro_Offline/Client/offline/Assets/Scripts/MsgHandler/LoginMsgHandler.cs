using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pb;
using System.IO;

public class LoginMsgHandler
{
    private static LoginMsgHandler instance;
    public static LoginMsgHandler Instance
    {
        get
        {
            if (instance == null)
                instance = new LoginMsgHandler();
            return instance;
        }
    }

    public void SendLogin(string deviceId)
    {
        MyLog.Log("deviceId=" + deviceId);
        C2GSLogin msg = new C2GSLogin();
        msg.OID = deviceId;
        NetworkManager.Instance.SendToGS((ushort)MsgDef.C2GSLogin, msg);
    }

    public void RevGS2CLoginRet(int pid, byte[] msgBuf, int msgSize)
    {
        Stream stream = new MemoryStream(msgBuf);
        pb.GS2CLoginRet msg = ProtoBuf.Serializer.Deserialize<pb.GS2CLoginRet>(stream);
        if (msg.errorCode == ErrorCode.SUCCESS)
        {
            UIManager.Instance.EnterGame();
        }
    }

}
