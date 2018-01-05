using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using DG.Tweening;

public class SleepWindow : BaseWindow
{
    private GButton mBtnBack;
    private GList mList;

    public override void OnAwake()
    {
        mBtnBack = mWindowObj.GetChild("n2").asButton;
        mBtnBack.onClick.Add(OnClickBack);
        mBtnBack.visible = false;
        mList = mWindowObj.GetChild("n1").asList;       
        mList.itemRenderer = RenderListItem;
    }

    public override void OnShownAni()
    {
        mWindowObj.SetScale(0.1f, 0.1f);
        mWindowObj.SetPivot(0.5f, 0.5f);
        mWindowObj.TweenScale(new Vector2(1, 1), 0.3f).SetEase(Ease.OutQuad);
    }

    private void OnClickBack(EventContext context)
    {        
        Process.Instance.CurEventData = null;
        UIManager.Instance.ShowWindow<MainWindow>(WindowType.WINDOW_MAIN);
        mBtnBack.visible = false;
    }

    public override void OnEnable()
    {        
        mList.numItems = 1;
        Process.Instance.Saved();
        mBtnBack.visible = true;
    }

    private void RenderListItem(int index, GObject obj)
    {
        //MyLog.Log("RenderListItem: index=" + index);
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
