using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BottomEvent : BottomUI
{
    GButton mBtn1;
    GButton mBtn2;
    GTextField mText;

    private ConfigEvent mEventInfo;

    public BottomEvent()
    {
        mObj = UIPackage.CreateObject("wuxia", "fn_event").asCom;
        mObj.visible = false;
        mBtn1 = mObj.GetChild("btnChooseA").asButton;
        mBtn1.onClick.Add(OnClickBtn);
        mBtn2 = mObj.GetChild("btnChooseB").asButton;
        mBtn2.onClick.Add(OnClickBtn);
        mText = mObj.GetChild("textEvent").asTextField;
    }

    public void UpdateUI(int eventId)
    {
        MyLog.Log("Update event ui:" + eventId);
        mEventInfo = ConfigManager.Instance.ReqEvent(eventId);
        if (mEventInfo != null)
        {
            mText.text = mEventInfo._desc;
            mBtn1.visible = mEventInfo._resultList.Count > 0;
            mBtn1.text = mEventInfo._resultList.Count > 0 ? mEventInfo._resultList[0].resultDesc : "";
            mBtn2.visible = mEventInfo._resultList.Count > 1;
            mBtn2.text = mEventInfo._resultList.Count > 1 ? mEventInfo._resultList[1].resultDesc : "";
        }
        else
        {
            MyLog.LogError("Event " + eventId + " not exist.");
        }
    }

    private void OnClickBtn(EventContext context)
    {
        EventResultType resultType = EventResultType.Idle;
        int resultId = 0;
        if (context.sender == mBtn1)
        {
            resultType = (EventResultType)mEventInfo._resultList[0].type;
            resultId = mEventInfo._resultList[0].result;
        }
        else if (context.sender == mBtn2)
        {
            resultType = (EventResultType)mEventInfo._resultList[1].type;
            resultId = mEventInfo._resultList[1].result;
        }
        switch (resultType)
        {
            case EventResultType.Battle:
                Process.Instance.CurEventData = new EventData(EventType.Battle, resultId);
                break;
            case EventResultType.Drop:
                Process.Instance.CurEventData = new EventData(EventType.Drop, resultId);
                break;
            case EventResultType.Event:
                Process.Instance.CurEventData = new EventData(EventType.Event, resultId);
                break;
            default:
                Process.Instance.CurEventData = null;
                break;
        }
        UIManager.Instance.mMainWindow.UpdateUI();
    }
}
