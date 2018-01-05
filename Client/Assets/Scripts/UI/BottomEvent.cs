using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BottomEvent : BaseWindow
{
    private GButton mBtn1;
    private GButton mBtn2;
    private GTextField mText;

    private ConfigEvent mEventInfo;

    public override void OnAwake()
    {
        mBtn1 = mWindowObj.GetChild("btnChooseA").asButton;
        mBtn1.onClick.Add(OnClickBtn);
        mBtn2 = mWindowObj.GetChild("btnChooseB").asButton;
        mBtn2.onClick.Add(OnClickBtn);
        mText = mWindowObj.GetChild("textEvent").asTextField;
        mWindowObj.onClick.Add(OnClickBtn);
    }

    public override void OnEnable()
    {
        int eventId = Process.Instance.CurEventData._id;
        MyLog.Log("show event ui:" + eventId);
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
        EventType resultType = EventType.Idle;
        int resultId = 0;
        if (context.sender == mBtn1)
        {
            resultType = (EventType)mEventInfo._resultList[0].type;
            resultId = mEventInfo._resultList[0].result;
        }
        else if (context.sender == mBtn2)
        {
            resultType = (EventType)mEventInfo._resultList[1].type;
            resultId = mEventInfo._resultList[1].result;
        }
        else if (context.sender == mWindowObj)
        {
            MyLog.Log("click valid");
            if (mEventInfo._resultList.Count > 0)
            {
                return;
            }
        }
        switch (resultType)
        {
            case EventType.Battle:
                Process.Instance.CurEventData = new EventData(EventType.Battle, resultId);
                break;
            case EventType.Drop:
                Process.Instance.CurEventData = new EventData(EventType.Drop, resultId);
                break;
            case EventType.Event:
                Process.Instance.CurEventData = new EventData(EventType.Event, resultId);
                break;
            default:
                Process.Instance.CurEventData = null;
                break;
        }
        UIManager.mEventDispatch.DispatchEvent(EventDefine.UPDATE_MAIN_UI);
    }
}
