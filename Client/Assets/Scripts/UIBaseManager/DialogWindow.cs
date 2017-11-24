using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using DG.Tweening;

public class DialogWindow : Window
{
    private int mCurContextIndex;
    private int mCurContextWordIndex;
    private float mYStart = 0;
    private float mYSpace = 30;

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("survival", "Dialog").asCom;
        this.Center();
        this.modal = true;

        mCurContextIndex = 0;
        mCurContextWordIndex = 0;
    }

    private void PlayDialog()
    {
        if (mCurContextIndex < Process.Instance.CurDialog.Count)
        {
            string context = Process.Instance.CurDialog[mCurContextIndex];            
            GLabel label = UIPackage.CreateObject("survival", "DialogLabel").asLabel;
            PlayLineAni(label, context);
            this.contentPane.AddChild(label);
            label.position = new Vector3(0, mYStart - mCurContextIndex * mYSpace, 0);
            mCurContextIndex++;
        }
    }

    private void PlayLineAni(GLabel label, string context)
    {
        //Timers.inst.Add(0.001f, 0, () => { label.text = context; });
        label.text = context;
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
}
