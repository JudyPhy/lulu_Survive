using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class MainWindow : Window
{
    GTextField[] mTextTop = new GTextField[3];
    
    GTextField[] mTextValueInRect = new GTextField[6];



    GTextField mTextMoney;
    GButton mBtnNav;

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
        Debug.Log("MainWindow shown");
        UpdateScene();
        UpdateHealthy();
        UpdateEnergy();
        UpdateHungry();
        UpdateBattleAttr();
    }

    public void UpdateScene()
    {
        Debug.Log("UpdateScene");
        ConfigMap curSceneData = ConfigManager.Instance.ReqMapData(Process.Instance.CurScene);
        if (curSceneData != null)
        {
            mTextTop[0].text = curSceneData._name;
            ConfigMap destination = ConfigManager.Instance.ReqMapData(curSceneData._destination);
            if (destination != null)
            {
                mTextTop[1].text = destination._name;
            }         
        }
        mTextTop[2].text = Process.Instance.Distance.ToString() + "km";
    }

    public void UpdateHealthy()
    {
        Debug.Log("UpdateHealthy:" + Process.Instance.Player.Healthy);
        mTextValueInRect[0].text = Process.Instance.Player.Healthy < 0 ? "0" : Process.Instance.Player.Healthy.ToString();
    }

    public void UpdateEnergy()
    {
        Debug.Log("UpdateEnergy:" + Process.Instance.Player.Energy);
        mTextValueInRect[1].text = Process.Instance.Player.Energy.ToString();
    }

    public void UpdateHungry()
    {
        Debug.Log("UpdateHungry:" + Process.Instance.Player.Hungry);
        mTextValueInRect[2].text = Process.Instance.Player.Hungry < 0 ? "0" : Process.Instance.Player.Hungry.ToString();
    }

    public void UpdateBattleAttr()
    {
        mTextValueInRect[3].text = Process.Instance.Player.Hp.ToString();
        mTextValueInRect[4].text = Process.Instance.Player.Atk.ToString();
        mTextValueInRect[5].text = Process.Instance.Player.Def.ToString();
    }

}
