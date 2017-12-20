﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class MainWindow : Window
{
    GTextField[] mTextTop = new GTextField[3];
    
    GTextField[] mTextValueInRect = new GTextField[9];

    GTextField mTextMoney;
    GButton mBtnNav;

    BottomNormal mBottomNormal;
    BottomBattle mBottomBattle;
    BottomEvent mBottomEvent;

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
        mBottomBattle = new BottomBattle();
        mBottomEvent = new BottomEvent();
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
        MyLog.Log("MainWindow shown");
        UpdateTop();
        BottomShown(Process.Instance.CurEventType);
    }

    private void UpdateTop()
    {
        UpdateSceneInfo();
        UpdateHealthy();
        UpdateEnergy();
        UpdateHungry();
        UpdateBattleAttr();
        UpdateGold();
    }

    public void UpdateSceneInfo()
    {
        MyLog.Log("UpdateSceneInfo");
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
    }

    public void UpdateHealthy()
    {
        MyLog.Log("UpdateHealthy:" + Process.Instance.Player.Healthy);
        mTextValueInRect[0].text = Process.Instance.Player.Healthy < 0 ? "0" : Process.Instance.Player.Healthy.ToString();
    }

    private void TweenBackText()
    {
        
    }

    public void UpdateEnergy()
    {
        MyLog.Log("UpdateEnergy:" + Process.Instance.Player.Energy);
        mTextTop[1].text = Process.Instance.Player.Energy.ToString();
    }

    public void UpdateHungry()
    {
        MyLog.Log("UpdateHungry:" + Process.Instance.Player.Hungry);
        mTextTop[2].text = Process.Instance.Player.Hungry < 0 ? "0" : Process.Instance.Player.Hungry.ToString();
        mTextTop[2].color = Process.Instance.Player.Hungry < 0 ? Color.red : Color.black;
    }

    public void UpdateBattleAttr()
    {
        MyLog.Log("UpdateBattleAttr: Hp=" + Process.Instance.Player.Hp + ", Atk=" + Process.Instance.Player.Atk + ", Def=" + Process.Instance.Player.Def);
        mTextValueInRect[5].text = Process.Instance.Player.Hp.ToString();
        mTextValueInRect[3].text = Process.Instance.Player.Atk.ToString();
        mTextValueInRect[4].text = Process.Instance.Player.Def.ToString();
    }

    public void UpdateGold()
    {
        MyLog.Log("UpdateGold:" + Process.Instance.Player.Gold);
        mTextValueInRect[1].text = Process.Instance.Player.Gold < 0 ? "0" : Process.Instance.Player.Gold.ToString();
    }

    private void BottomShown(EventType type)
    {
        mBottomNormal.Show(type == EventType.Idle);
        mBottomEvent.Show(type == EventType.Event);
        mBottomBattle.Show(type == EventType.Battle);
    }

    public void Tips(string content)
    {
        BottomShown(EventType.Idle);
        mBottomNormal.Tips(content);
    }

}
