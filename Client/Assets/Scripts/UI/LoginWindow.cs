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
        mBtnContinue.onClick.Add(OnClickContinue);
    }

    override protected void DoShowAnimation()
    {
        this.SetScale(0.1f, 0.1f);
        this.SetPivot(0.5f, 0.5f);
        this.TweenScale(new Vector2(1, 1), 0.3f).SetEase(Ease.OutQuad).OnComplete(this.OnShown);
    }

    override protected void DoHideAnimation()
    {
        this.TweenScale(new Vector2(0.1f, 0.1f), 0.3f).SetEase(Ease.OutQuad).OnComplete(this.HideImmediately);
    }

    private void OnClickNew(EventContext context)
    {
        Process.Instance.StartGame();
        this.Hide();
        UIManager.Instance.UpdateUI();
    }

    private void OnClickContinue(EventContext context)
    {
        Process.Instance.ReqHistoryData();
        this.Hide();
        UIManager.Instance.UpdateUI();
    }

    protected override void OnShown()
    {
    }
}
