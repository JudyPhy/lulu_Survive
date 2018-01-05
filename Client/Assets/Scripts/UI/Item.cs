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

    private ItemData mData;

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
        mData = Process.Instance.Player.ReqItem(data._id);
        mBtnTitleText.text = mData.ConfigItemData._name;
        mBtnTitleText.color = mData.Count > 0 ? Color.green : Color.white;
        mBtnTitle.enabled = mData.Count > 0;
        mCount.text = "拥有：" + mData.Count + "个";
        mDesc.text = "效果：" + mData.ConfigItemData._desc;
    }

    private void OnClickUseItem(EventContext context)
    {       
        if (mData.CanUse())
        {
            Process.Instance.Player.AddItem(mData.ID, -1);
            mData.Used();
            UIManager.mEventDispatch.DispatchEvent(EventDefine.UPDATE_BAG_UI);
            Process.Instance.Saved();
        }
    }
}
