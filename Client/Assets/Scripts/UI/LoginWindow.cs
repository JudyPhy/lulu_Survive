using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class LoginWindow : Window
{
    private GButton mBtnNew;
    private GButton mBtnContinue;

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("wuxia", "UI_login").asCom;
        this.Center();
        this.modal = true;

        mBtnNew = this.contentPane.GetChild("btnNewGame").asButton;
        mBtnNew.onClick.Add(OnClickNew);
        mBtnContinue = this.contentPane.GetChild("btnLoadGame").asButton;
        mBtnContinue.visible = GameSaved.HasHistory();
        mBtnContinue.onClick.Add(OnClickContinue);
    }

    private void OnClickNew(EventContext context)
    {
        Process.Instance.StartNewGame();
        EnterGame();
    }

    private void OnClickContinue(EventContext context)
    {
        Process.Instance.StartHistoryGame();
        EnterGame();
    }

    private void EnterGame()
    {
        if (Process.Instance.NeedShowDialog())
        {
            MyLog.Log("Play dialog[" + Process.Instance.NextStoryID + "]");
            UIManager.Instance.mDialogWindow.mSwitchScene = UIType.Idle;
            UIManager.Instance.SwitchToUI(UIType.Dialog);
        }
        else
        {
            UIManager.Instance.SwitchToUI(UIType.Main);
        }
    }

    protected override void OnShown()
    {
    }
}
