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

        GList topList = this.contentPane.GetChild("titleList").asList;
        if (topList != null)
        {
            for (int i = 0; i < mTextTop.Length; i++)
            {
                GComponent item = topList.GetChild("item" + (i + 1).ToString()).asCom;
                mTextTop[i] = item.GetChild("title").asTextField;
            }
        }

        GList attrList = this.contentPane.GetChild("attbList").asList;
        if (attrList != null)
        {
            for (int i = 0; i < mTextValueInRect.Length; i++)
            {
                GComponent item = attrList.GetChild("attb" + (i + 1).ToString()).asCom;
                mTextValueInRect[i] = item.GetChild("n2").asTextField;
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
    }

    public void UpdateScene()
    {
        ConfigMap curSceneData = ConfigManager.Instance.ReqMapData(Process.Instance.CurScene);
        if (curSceneData != null)
        {
            mTextTop[0].text = curSceneData._name;
            ConfigMap destination = ConfigManager.Instance.ReqMapData(curSceneData._destination);
            if (destination != null)
            {
                mTextTop[1].text = destination._name;
            }
            mTextTop[2].text = curSceneData._distance.ToString() + "km";
        }
    }

    public void UpdateHealthy()
    {
        mTextValueInRect[0].text = Process.Instance.Player.Healthy.ToString();
    }

    public void UpdateEnergy()
    {
        mTextValueInRect[1].text = Process.Instance.Player.Energy.ToString();
    }

    public void UpdateHungry()
    {
        mTextValueInRect[2].text = Process.Instance.Player.Hungry.ToString();
    }

    public void UpdateBattleAttr()
    {
        mTextValueInRect[3].text = Process.Instance.Player.Hp.ToString();
        mTextValueInRect[4].text = Process.Instance.Player.Atk.ToString();
        mTextValueInRect[5].text = Process.Instance.Player.Def.ToString();
    }

}
