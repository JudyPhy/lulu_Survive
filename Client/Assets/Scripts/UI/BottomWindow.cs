using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BottomWindow : Window
{
    GButton[] mBtnList = new GButton[9];
    GTextField mTextTips;

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
            mBtnList[i].visible = false;
        }
        mTextTips = this.contentPane.GetChild("textNormal").asTextField;
        mTextTips.text = "";

        UpdateBtns();
    }

    private void UpdateBtns()
    {
        ConfigScene scene = ConfigManager.Instance.ReqSceneData(Process.Instance.CurScene);
        if (scene != null)
        {
            bool inScene = Process.Instance.InCurScene(scene, Process.Instance.CurPos);
            mBtnList[0].visible = inScene && scene._shop != 0;
            mBtnList[0].text = "商店";

            mBtnList[3].visible = inScene && scene._shop != 0;
            mBtnList[3].text = "探索";
        }
    }

    private void OnClickBtn(EventContext context)
    {
        GButton btn = (GButton)context.sender;
        if (btn == mBtnList[0])
        {
            MyLog.Log("Click shop.");
        }
        else if (btn == mBtnList[1])
        {
            MyLog.Log("Click challenge.");
        }
        else if (btn == mBtnList[4])
        {
            Process.Instance.MoveTowards();
        }
    }

    public void Tips(string tips)
    {
        mTextTips.text = tips;
    }

    protected override void OnShown()
    {
    }
}
