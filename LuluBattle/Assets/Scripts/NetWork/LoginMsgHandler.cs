using UnityEngine;
using System.Collections;
using System.IO;

public class LoginMsgHandler  {

    private static LoginMsgHandler instance;
    public static LoginMsgHandler Instance {
        get {
            if (instance == null) {
                instance = new LoginMsgHandler();
            }
            return instance;
        }
    }

    private string CurAccountName;
    private string CurPassword;

    #region 客户端->服务器
    

    #endregion


    #region 服务器->客户端
    

    //登录网关成功
    public void LoginGateServerSuccess() {
        NetworkManager.Instance.GateServerTcpConnect_.UpdateRecvHeartTickTime();
        NetworkManager.Instance.Test_GateConnectSuccess_ = true;
    }

    //接收心跳
    public void RevMsgGS2CHeartTickRet(int pid, byte[] msgBuf, int msgSize) {
        //Debug.LogError("RevMsgGS2CHeartTickRet");
        NetworkManager.Instance.GateServerTcpConnect_.UpdateRecvHeartTickTime();
    }

    #endregion
}
