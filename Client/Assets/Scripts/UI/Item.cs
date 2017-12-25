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
        ItemCountData countData = Process.Instance.GetHasItem(mData._id);
        mBtnTitleText.text = mData._name;
        mBtnTitleText.color = countData.count > 0 ? Color.green : Color.white;
        mBtnTitle.enabled = countData.count > 0;
        mCount.text = "拥有：" + countData.count + "个";
        mDesc.text = "效果：" + mData._desc;
    }

    private void OnClickUseItem(EventContext context)
    {
        Debug.LogError("click item");
        ItemCountData countData = Process.Instance.GetHasItem(mData._id);
        if (countData.count > 0)
        {
            Process.Instance.Player.AddItem(mData._id, -1);
            mBtnTitleText.color = countData.count - 1 > 0 ? Color.green : Color.white;
            mCount.text = "拥有：" + (countData.count - 1) + "个";
            UpdateItemAttr();
        }
    }

    private void UpdateItemAttr()
    {
        Process.Instance.Player.BuffID = mData._id;
        Process.Instance.Player.BuffDuration = mData._duration;
        if (mData._healthy != 0)
        {
            Process.Instance.Player.Healthy += mData._healthy;
        }
        if (mData._energy != 0)
        {
            Process.Instance.Player.Energy += mData._energy;
        }
        if (mData._hungry != 0)
        {
            Process.Instance.Player.Hungry += mData._hungry;
        }
        if (mData._hp != 0)
        {
            Process.Instance.Player.Hp += mData._hp;
        }
        if (mData._power != 0)
        {
            Process.Instance.Player.Power += mData._power;
        }
        if (mData._agile != 0)
        {
            Process.Instance.Player.Agile += mData._agile;
        }
        if (mData._physic != 0)
        {
            Process.Instance.Player.Physic += mData._physic;
        }
        if (mData._charm != 0)
        {
            Process.Instance.Player.Charm += mData._charm;
        }
        if (mData._perception != 0)
        {
            Process.Instance.Player.Perception += mData._perception;
        }
        Process.Instance.UpdateAttr();
        Process.Instance.Saved();
    }
}
