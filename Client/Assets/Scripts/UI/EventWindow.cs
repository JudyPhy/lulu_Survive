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
        Hide();
        int eventPackId = EventInfo._resultList[0].reward;
        if (eventPackId == 0)
        {            
            UIManager.Instance.mBottomWindow.Show();
            UIManager.Instance.mBottomWindow.Tips("");
        }
        else
        {
            List<ConfigEventPackage> packList = ConfigManager.Instance.ReqEvents(eventPackId);
            ConfigEventPackage eventInfo = Process.Instance.GetRandomEvent(packList);
            if (eventInfo != null)
            {
                UIManager.Instance.UpdateEvent(eventInfo);
            }
        }
    }

    private void OnClickBtnB(EventContext context)
    {
        
    }

    protected override void OnShown()
    {
        Debug.Log("EventWindow shown=> eventId:" + EventInfo._id);
        mText.text = EventInfo._desc;
        bool showBtns = EventInfo._resultList.Count == 2;
        mBtn1.visible = showBtns;
        mBtn2.visible = showBtns;
        if (showBtns)
        {
            mBtn1.text = EventInfo._resultList[0].resultDesc;
            mBtn2.text = EventInfo._resultList[1].resultDesc;
        }       
    }
}
