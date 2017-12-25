using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class MainWindow : Window
{
    private GTextField[] mTextTop = new GTextField[3];

    private GTextField[] mTextValueInRect = new GTextField[9];

    private GTextField mTextMoney;
    private GButton mBtnNav;

    private BottomNormal mBottomNormal;
    private BottomBattle mBottomBattle;
    private BottomEvent mBottomEvent;

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("wuxia", "UI_main").asCom;
        this.Center();
        this.modal = true;

        GComponent top = this.contentPane.GetChild("titleList").asCom;
        if (top != null)
        {
            for (int i = 0; i < mTextTop.Length; i++)
            {
                GComponent item = top.GetChild("title" + i.ToString()).asCom;
                mTextTop[i] = item.GetChild("title").asTextField;
            }
        }

        GComponent attr = this.contentPane.GetChild("attbList").asCom;
        if (attr != null)
        {
            for (int i = 0; i < mTextValueInRect.Length; i++)
            {
                GComponent item = attr.GetChild("attb" + i.ToString()).asCom;
                mTextValueInRect[i] = item.GetChild("value").asTextField;
            }
        }

        mBottomNormal = new BottomNormal();       
        this.contentPane.AddChild(mBottomNormal.mObj);
        mBottomBattle = new BottomBattle();
        this.contentPane.AddChild(mBottomBattle.mObj);
        mBottomEvent = new BottomEvent();
        this.contentPane.AddChild(mBottomEvent.mObj);
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

    protected override void OnShown()
    {
        //MyLog.Log("MainWindow shown");
        UpdateUI();
    }

    public void UpdateUI()
    {
        UpdateTop();
        UpdateBottom();
    }

    public void UpdateTop()
    {
        UpdateSceneInfo();
        UpdateHealthy();
        UpdateEnergy();
        UpdateHungry();
        UpdateBattleAttr();
        UpdateGold();
        UpdateMedicine();
    }

    public void UpdateSceneInfo()
    {
        ConfigScene curSceneData = ConfigManager.Instance.ReqSceneData(Process.Instance.CurScene);
        if (curSceneData != null)
        {
            mTextTop[0].text = curSceneData._name;
            if (curSceneData._outList.Count == 1)
            {
                List<int> list = new List<int>(curSceneData._outList.Keys);
                ConfigScene outSceneData = ConfigManager.Instance.ReqSceneData(list[0]);
                mTextValueInRect[6].text = outSceneData != null ? outSceneData._name : "未知";
            }
            else
            {
                mTextValueInRect[6].text = "";
            }
        }

        mTextValueInRect[2].text = "";
        mTextValueInRect[7].text = "";
    }

    public void UpdateHealthy()
    {
        mTextValueInRect[0].text = Process.Instance.Player.Healthy < 0 ? "0" : Process.Instance.Player.Healthy.ToString();
    }

    public void UpdateEnergy()
    {
        mTextTop[1].text = Process.Instance.Player.Energy.ToString() + "/" + Process.Instance.Player.EnergyMax.ToString();
    }

    public void UpdateHungry()
    {
        mTextTop[2].text = Process.Instance.Player.Hungry < 0 ? "0/" + Process.Instance.Player.EnergyMax.ToString()
            : Process.Instance.Player.Hungry.ToString() + "/" + Process.Instance.Player.EnergyMax.ToString();
        mTextTop[2].color = Process.Instance.Player.Hungry < 0 ? Color.red : Color.black;
    }

    public void UpdateBattleAttr()
    {        
        mTextValueInRect[3].text = Process.Instance.Player.Atk.ToString();
        mTextValueInRect[4].text = Process.Instance.Player.Def.ToString();
        mTextValueInRect[5].text = Process.Instance.Player.Hp.ToString();
    }

    public void UpdateGold()
    {
        mTextValueInRect[1].text = Process.Instance.Player.Gold < 0 ? "0" : Process.Instance.Player.Gold.ToString();
    }

    public void UpdateMedicine()
    {
        ItemCountData data = Process.Instance.GetHasItem(GameConfig.MEDICINE_ID);
        mTextValueInRect[8].text = data.count.ToString() + "个";
    }

    public void UpdateBottom()
    {
        if (Process.Instance.CurEventData == null)
        {
            Process.Instance.CurEventData = new EventData(EventType.Idle, 0);
        }
        EventType type = Process.Instance.CurEventData._type;
        //MyLog.Log("Bottom shown, type=" + type.ToString());
        mBottomNormal.Show(type == EventType.Idle || type == EventType.Drop);
        mBottomEvent.Show(type == EventType.Event);
        mBottomBattle.Show(type == EventType.Battle);
        switch (type)
        {
            case EventType.Idle:
                mBottomNormal.UpdateIdleUI(Process.Instance.CurEventData._desc);
                break;
            case EventType.Drop:
                mBottomNormal.UpdateDropUI(Process.Instance.CurEventData._id);
                break;
            case EventType.Event:
                mBottomEvent.UpdateUI(Process.Instance.CurEventData._id);
                break;
            case EventType.Battle:
                mBottomBattle.UpdateUI(Process.Instance.CurEventData._id);
                break;
            default:
                break;
        }
    }

    public void Tips(string content)
    {
        Process.Instance.CurEventData = new EventData(EventType.Idle, 0, content);
        UpdateBottom();
    }

    public void PlayBattleAtkAni()
    {
        UpdateHealthy();
        UpdateBattleAttr();
        mBottomBattle.PlayAtkAni();
    }

}
