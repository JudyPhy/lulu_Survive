using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class Item
{
    public GComponent mObj;
    private GButton mBtnTitle;
    private GTextField mBtnTitleText;
    private GTextField mCount;
    private GTextField mDesc;

    private ConfigItem mData;

    public Item(GComponent obj)
    {
        mObj = obj;

        mBtnTitle = mObj.GetChild("btnItemName").asButton;
        mBtnTitle.onClick.Clear();  //会重复添加事件，导致点击一次响应多次
        mBtnTitle.onClick.Add(OnClickUseItem);
        mBtnTitleText = mBtnTitle.GetChild("title").asTextField;
        mCount = mObj.GetChild("curNum").asTextField;
        mDesc = mObj.GetChild("infoEffect").asTextField;
    }

    public void UpdateUI(ConfigItem data)
    {
        //MyLog.Log("Update item ui, item=" + data._id);
        mData = data;
        ItemCountData countData = Process.Instance.GetSelfItem(mData._id);
        mBtnTitleText.text = mData._name;
        mBtnTitleText.color = countData.count > 0 ? Color.green : Color.white;
        mBtnTitle.enabled = countData.count > 0;
        mCount.text = "拥有：" + countData.count + "个";
        mDesc.text = "效果：" + mData._desc;
    }

    private void OnClickUseItem(EventContext context)
    {       
        if (Process.Instance.CanUseItem(mData._id))
        {
            Process.Instance.Player.AddItem(mData._id, -1);            
            UpdateItemAttr();
            ItemCountData countData = Process.Instance.GetSelfItem(mData._id);
            mBtnTitleText.color = countData.count - 1 > 0 ? Color.green : Color.white;
            mCount.text = "拥有：" + countData.count + "个";
        }
    }

    private void UpdateItemAttr()
    {
        Process.Instance.Player.BuffID = mData._id;
        Process.Instance.Player.BuffDuration = mData._duration;
        Process.Instance.Player.AddHealthy(mData._healthy);
        Process.Instance.Player.AddEnergy(mData._energy);
        Process.Instance.Player.AddHungry(mData._hungry);
        Process.Instance.Player.AddHp(mData._hp);
        Process.Instance.Player.Power = Mathf.Max(0, Process.Instance.Player.Power + mData._power);
        Process.Instance.Player.Agile = Mathf.Max(0, Process.Instance.Player.Agile + mData._agile);
        Process.Instance.Player.Physic = Mathf.Max(0, Process.Instance.Player.Physic + mData._physic);
        Process.Instance.Player.Charm = Mathf.Max(0, Process.Instance.Player.Charm + mData._charm);
        Process.Instance.Player.Perception = Mathf.Max(0, Process.Instance.Player.Perception + mData._perception);
        UIManager.Instance.mBagWindow.UpdateTopAttr();
        Process.Instance.Saved();
    }
}
