using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTransmit;

public class Panel_Map : WindowsBasePanel
{

    public override void OnAwake()
    {
        base.OnAwake();        
    }

    public override void OnStart()
    {
        base.OnStart();

        BattleMsgHandler.Instance.SendMsgC2GSStartGame(pb.GameMode.Single, 1, 1);
        UIManager.Instance.ShowMainWindow<Panel_Loading>(eWindowsID.LoadingUI);
    }

    public override void OnRegisterEvent()
    {
        base.OnRegisterEvent();
        
    }

    public override void OnRemoveEvent()
    {
        base.OnRemoveEvent();
        
    }    

    public override void OnEnableWindow()
    {
        base.OnEnableWindow();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

}
