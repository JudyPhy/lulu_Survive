using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BottomWindow : Window
{
    GButton[] mBtnList = new GButton[9];

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("wuxia", "fn_nomal").asCom;
        this.Center();
        this.modal = true;

        GComponent btnCom = this.contentPane.GetChild("btnList").asCom;
        for (int i = 0; i < mBtnList.Length; i++)
        {
            mBtnList[i] = btnCom.GetChildAt(i).asButton;
            mBtnList[i].onClick.Add(OnClickBtn);
        }
    }

    override protected void DoShowAnimation()
    {
        this.SetPosition(0, 350, 0);
        this.SetPivot(0.5f, 0.5f);
        Tweener tween = this.TweenMoveY(0, 0.3f);
        tween.SetEase(Ease.OutQuad).OnComplete(this.OnShown);
        tween.SetDelay(0.3f);
    }

    override protected void DoHideAnimation()
    {
        this.TweenMoveY(-350f, 0.3f).SetEase(Ease.OutQuad).OnComplete(this.HideImmediately);
    }

    private void OnClickBtn(EventContext context)
    {
        GButton btn = (GButton)context.sender;
        if (btn == mBtnList[0])
        {
            Debug.Log("Click shop.");
        }
        else if (btn == mBtnList[1])
        {
            Debug.Log("Click challenge.");
        }
        else if (btn == mBtnList[4])
        {
            Debug.Log("Move towards.");
            Process.Instance.MoveTowards();
        }
    }

    protected override void OnShown()
    {
    }
}
