using UnityEngine;
using System.Collections;
using System.IO;
using System;

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

    public void SendMsgC2GSLogin(string account, string password)
    {
        pb.C2GSLogin msg = new pb.C2GSLogin();
        msg.account = account;
        msg.password = password;
        Debug.Log("C2GSLogin ============>>>>>>>>>> account:" + msg.account + ", password:" + msg.password);
        NetworkManager.Instance.SendToGS((UInt16)MsgDef.C2GSLogin, msg);
    }

    public void SendMsgC2GSChooseRole(string nickName, string headIcon)
    {
        pb.C2GSChooseRole msg = new pb.C2GSChooseRole();
        msg.NickName = nickName;
        msg.HeadIcon = headIcon;
        Debug.Log("C2GSChooseRole ============>>>>>>>>>> nickName:" + msg.NickName + ", headIcon:" + msg.HeadIcon);
        NetworkManager.Instance.SendToGS((UInt16)MsgDef.C2GSChooseRole, msg);
    }

    #endregion


    #region 服务器->客户端

    public void RevMsgGS2CLoginRet(int pid, byte[] msgBuf, int msgSize)
    {
        Stream stream = new MemoryStream(msgBuf);
        pb.GS2CLoginRet msg = ProtoBuf.Serializer.Deserialize<pb.GS2CLoginRet>(stream);
        Debug.Log("GS2CLoginRet <<<<<<<<<<============ errorCode:" + msg.errorCode.ToString());
        switch (msg.errorCode)
        {
            case pb.ErrorCode.SUCCESS:
                if (msg.playerInfo == null)
                {
                    Debug.Log("To create role.");
                    UIManager.Instance.ShowMainWindow<Panel_ChooseRole>(eWindowsID.ChooseRole);
                }
                else
                {
                    Debug.Log("To battle.");
                    Player.Instance.UpdateInfo(msg.playerInfo);
                    UIManager.Instance.ShowMainWindow<Panel_Loading>(eWindowsID.LoadingUI);
                }
                break;
            case pb.ErrorCode.ACCOUNT_ERROR:
                break;
            case pb.ErrorCode.PASSWORD_ERROR:
                break;
            case pb.ErrorCode.FAIL:
                break;
            default:
                break;
        }
    }

    public void RevMsgGS2CChooseRoleRet(int pid, byte[] msgBuf, int msgSize)
    {
        Stream stream = new MemoryStream(msgBuf);
        pb.GS2CChooseRoleRet msg = ProtoBuf.Serializer.Deserialize<pb.GS2CChooseRoleRet>(stream);
        Debug.Log("GS2CChooseRoleRet <<<<<<<<<<============ errorCode:" + msg.errorCode.ToString());
        switch (msg.errorCode)
        {
            case pb.ErrorCode.SUCCESS:
                Player.Instance.UpdateInfo(msg.playerInfo);
                UIManager.Instance.ShowMainWindow<Panel_Map>(eWindowsID.BattleMap);
                break;
            case pb.ErrorCode.NICKNAME_EXIST:
                break;
            case pb.ErrorCode.FAIL:
                break;
            default:
                break;
        }
    }

    #endregion
}
