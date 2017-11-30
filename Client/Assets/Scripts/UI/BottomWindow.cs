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
        }
        mTextTips = this.contentPane.GetChild("textNormal").asTextField;
        mTextTips.text = "";
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
