using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class MainWindow : BaseWindow
{
    private GTextField[] mTextTop = new GTextField[3];

    private GTextField[] mTextValueInRect = new GTextField[9];

    private GTextField mTextMoney;
    private GButton mBtnNav;

    private BottomNormal mBottomNormal;
    private BottomBattle mBottomBattle;
    private BottomEvent mBottomEvent;

    public override void OnAwake()
    {
        GComponent top = mWindowObj.GetChild("titleList").asCom;
        if (top != null)
        {
            for (int i = 0; i < mTextTop.Length; i++)
            {
                GComponent item = top.GetChild("title" + i.ToString()).asCom;
                mTextTop[i] = item.GetChild("title").asTextField;
            }
        }

        GComponent attr = mWindowObj.GetChild("attbList").asCom;
        if (attr != null)
        {
            for (int i = 0; i < mTextValueInRect.Length; i++)
            {
                GComponent item = attr.GetChild("attb" + i.ToString()).asCom;
                mTextValueInRect[i] = item.GetChild("value").asTextField;
            }
        }
    }

    public override void OnShownAni()
    {
        mWindowObj.SetScale(0.1f, 0.1f);
        mWindowObj.SetPivot(0.5f, 0.5f);
        mWindowObj.TweenScale(new Vector2(1, 1), 0.3f).SetEase(Ease.OutQuad);
    }

    protected override void OnRegisterEvent()
    {
        UIManager.mEventDispatch.AddEventListener(EventDefine.PLAY_ATK_ANI, PlayBattleAtkAni);
        UIManager.mEventDispatch.AddEventListener(EventDefine.UPDATE_TIPS, Tips);
        UIManager.mEventDispatch.AddEventListener(EventDefine.UPDATE_MAIN_UI, OnEnable);
    }

    protected override void OnRemoveEvent()
    {
        UIManager.mEventDispatch.RemoveEventListener(EventDefine.PLAY_ATK_ANI, PlayBattleAtkAni);
        UIManager.mEventDispatch.RemoveEventListener(EventDefine.UPDATE_TIPS, Tips);
        UIManager.mEventDispatch.RemoveEventListener(EventDefine.UPDATE_MAIN_UI, OnEnable);
    }

    public override void OnEnable()
    {
        UpdateTop();
        ShowBottom();
    }

    private void UpdateTop()
    {
        UpdateSceneInfo();
        UpdateHealthy();
        UpdateEnergy();
        UpdateHungry();
        UpdateBattleAttr();
        UpdateGold();
        UpdateMedicine();
    }

    private void UpdateSceneInfo()
    {
        ConfigScene curSceneData = ConfigManager.Instance.ReqSceneData(Process.Instance.CurScene);
        if (curSceneData != null)
        {
            mTextTop[0].text = curSceneData._name;
            ConfigScene destinationData = ConfigManager.Instance.ReqSceneData(curSceneData._destination);
            mTextValueInRect[6].text = destinationData == null ? "" : destinationData._name;
        }
        mTextValueInRect[2].text = "";
        mTextValueInRect[7].text = "";
    }

    private void UpdateHealthy()
    {
        mTextValueInRect[0].text = Process.Instance.Player.Healthy < 0 ? "0" : Process.Instance.Player.Healthy.ToString();
    }

    private void UpdateEnergy()
    {
        mTextTop[1].text = Process.Instance.Player.Energy.ToString() + "/" + Process.Instance.Player.EnergyMax.ToString();
    }

    private void UpdateHungry()
    {
        mTextTop[2].text = Process.Instance.Player.Hungry < 0 ? "0/" + Process.Instance.Player.EnergyMax.ToString()
            : Process.Instance.Player.Hungry.ToString() + "/" + Process.Instance.Player.EnergyMax.ToString();
        mTextTop[2].color = Process.Instance.Player.Hungry < 0 ? Color.red : Color.white;
    }

    private void UpdateBattleAttr()
    {
        mTextValueInRect[3].text = Process.Instance.Player.Atk.ToString();
        mTextValueInRect[4].text = Process.Instance.Player.Def.ToString();
        mTextValueInRect[5].text = Process.Instance.Player.Hp.ToString();
    }

    private void UpdateGold()
    {
        mTextValueInRect[1].text = Process.Instance.Player.Gold < 0 ? "0" : Process.Instance.Player.Gold.ToString();
    }

    private void UpdateMedicine()
    {
        ItemData item = Process.Instance.Player.ReqItem(GameConfig.MEDICINE_ID);
        mTextValueInRect[8].text = item.Count.ToString() + "个";
    }

    private void ShowBottom()
    {
        if (Process.Instance.CurEventData == null)
        {
            Process.Instance.CurEventData = new EventData(EventType.Idle, 0);
        }
        switch (Process.Instance.CurEventData._type)
        {
            case EventType.Idle:
            case EventType.Drop:
                UIManager.Instance.ShowSubWindow<BottomNormal>(this, WindowType.SUBWINDOW_MAIN_NORMAL);
                break;
            case EventType.Event:
                UIManager.Instance.ShowSubWindow<BottomEvent>(this, WindowType.SUBWINDOW_MAIN_EVENT);
                break;
            case EventType.Battle:
                UIManager.Instance.ShowSubWindow<BottomBattle>(this, WindowType.SUBWINDOW_MAIN_BATTLE);
                break;
            default:
                break;
        }
    }

    private void Tips(EventContext context)
    {
        string content = (string)context.data;
        Process.Instance.CurEventData = new EventData(EventType.Idle, 0, content);
        ShowBottom();
    }

    private void PlayBattleAtkAni()
    {
        mWindowObj.TweenMove(new Vector2(10, 10), 0.05f).SetEase(Ease.OutExpo).OnComplete(() => { mWindowObj.TweenMove(new Vector2(0, 0), 0.05f).SetEase(Ease.OutExpo); });
    }

}
