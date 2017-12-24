using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using DG.Tweening;

public class SleepWindow : Window
{
    private GButton mBtnBack;
    private GList mList;

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("wuxia", "UI_sleep").asCom;
        this.Center();
        this.modal = true;

        mBtnBack = this.contentPane.GetChild("n2").asButton;
        mBtnBack.onClick.Add(OnClickBack);
        mList = this.contentPane.GetChild("n1").asList;       
        mList.itemRenderer = RenderListItem;
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

    private void OnClickBack(EventContext context)
    {
        Process.Instance.CurEventData = null;
        UIManager.Instance.SwitchToUI(UIType.Main);
    }

    protected override void OnShown()
    {
        mList.numItems = 1;
    }

    private void RenderListItem(int index, GObject obj)
    {
        MyLog.Log("RenderListItem: index=" + index);
        GComponent itemObj = obj.asCom;        
        GTextField title = itemObj.GetChild("n3").asTextField;
        title.text = "精力";
        GTextField addValue = itemObj.GetChild("n4").asTextField;
        addValue.text = "+" + GameConfig.RECOVER_ENERGY_ONCE;
        Process.Instance.Player.UpdateEnergy(Process.Instance.Player.Energy + GameConfig.RECOVER_ENERGY_ONCE);
        GTextField curValue = itemObj.GetChild("n5").asTextField;
        curValue.text = Process.Instance.Player.Energy + "/" + Process.Instance.Player.EnergyMax;
    }

}
