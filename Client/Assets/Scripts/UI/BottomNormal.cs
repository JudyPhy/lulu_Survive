using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BottomNormal : BottomUI
{
    GComponent mObj;
    GButton[] mBtnList = new GButton[9];
    GTextField mTextTips;

    public BottomNormal()
    {
        mObj = UIPackage.CreateObject("wuxia", "fn_nomal").asCom;

        GComponent btnCom = mObj.GetChild("btnList").asCom;
        for (int i = 0; i < mBtnList.Length; i++)
        {
            mBtnList[i] = btnCom.GetChildAt(i).asButton;
            mBtnList[i].onClick.Add(OnClickBtn);
            mBtnList[i].visible = false;
        }
        mTextTips = mObj.GetChild("textNormal").asTextField;
        mTextTips.text = "";
    }

    private void HideAllBtns()
    {
        for (int i = 0; i < mBtnList.Length; i++)
        {
            mBtnList[i].visible = false;
        }
    }

    public override void Show(bool show)
    {
        base.Show(show);
        UpdateBtns();
        mTextTips.text = "";
    }

    private void UpdateBtns()
    {
        HideAllBtns();
        ConfigScene scene = ConfigManager.Instance.ReqSceneData(Process.Instance.CurScene);
        if (scene != null)
        {
            bool inScene = Process.Instance.InCurScene(scene, Process.Instance.CurPos);
            mBtnList[0].visible = inScene && scene._shop != 0;
            mBtnList[3].visible = inScene;
            mBtnList[4].visible = true;
            mBtnList[5].visible = true;
        }
    }

    private void OnClickBtn(EventContext context)
    {
        GButton btn = (GButton)context.sender;
        if (btn == mBtnList[0])
        {
            MyLog.Log("Click shop.");
        }
        else if (btn == mBtnList[3])
        {
            MyLog.Log("Click explore.");
        }
        else if (btn == mBtnList[4])
        {
            Process.Instance.MoveTowards();
        }
        else if (btn == mBtnList[5])
        {
            MyLog.Log("Click bag.");
        }
    }

    public void Tips(string tips)
    {
        mTextTips.text = tips;
    }
}
