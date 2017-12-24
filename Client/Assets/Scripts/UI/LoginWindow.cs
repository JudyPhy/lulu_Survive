using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class LoginWindow : Window
{
    GButton mBtnNew;
    GButton mBtnContinue;

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
        this.Hide();
        UIManager.Instance.EnterGame();
    }

    private void OnClickContinue(EventContext context)
    {
        Process.Instance.StartHistoryGame();
        this.Hide();
        UIManager.Instance.EnterGame();
    }

    protected override void OnShown()
    {
    }
}
