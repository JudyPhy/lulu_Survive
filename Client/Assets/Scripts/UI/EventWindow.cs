using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class EventWindow : Window
{
    GButton mBtn1;
    GButton mBtn2;
    GTextField mText;

    public ConfigEvent EventInfo;

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("wuxia", "fn_event").asCom;
        this.Center();
        this.modal = true;

        mBtn1 = this.contentPane.GetChild("btnChooseA").asButton;
        mBtn1.onClick.Add(OnClickBtnA);
        mBtn2 = this.contentPane.GetChild("btnChooseB").asButton;
        mBtn1.onClick.Add(OnClickBtnB);
        mText = this.contentPane.GetChild("textEvent").asTextField;
    }

    private void OnClickBtnA(EventContext context)
    {
        
    }

    private void OnClickBtnB(EventContext context)
    {
        
    }

    protected override void OnShown()
    {
        Debug.Log("EventWindow shown");
    }
}
