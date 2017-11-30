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
        mBtn1.onClick.Add(OnClickBtn);
        mBtn2 = this.contentPane.GetChild("btnChooseB").asButton;
        mBtn2.onClick.Add(OnClickBtn);
        mText = this.contentPane.GetChild("textEvent").asTextField;
    }

    private void OnClickBtn(EventContext context)
    {
        int eventPackId = 0;
        eventPackId = context.sender == mBtn1 ? EventInfo._resultList[0].reward : EventInfo._resultList[1].reward;
        Hide();
        if (eventPackId == 0)
        {            
            UIManager.Instance.mBottomWindow.Show();
            UIManager.Instance.mBottomWindow.Tips("");
        }
        else
        {
            List<ConfigEventPackage> packList = ConfigManager.Instance.ReqEventList(eventPackId);
            ConfigEventPackage eventInfo = Process.Instance.GetRandomEvent(packList);
            if (eventInfo != null)
            {
                UIManager.Instance.UpdateEvent(eventInfo);
            }
        }
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
