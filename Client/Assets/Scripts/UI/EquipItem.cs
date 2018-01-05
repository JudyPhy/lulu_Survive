using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class EquipItem
{
    public GComponent mObj;
    private GButton mBtnTitle;
    private GTextField mBtnTitleText;
    private GTextField mCount;
    private GTextField mDesc;

    private EquipmentData mData;

    public EquipItem(GComponent obj)
    {
        mObj = obj;

        mBtnTitle = mObj.GetChild("btnItemName").asButton;
        mBtnTitle.onClick.Clear();  //会重复添加事件，导致点击一次响应多次
        mBtnTitle.onClick.Add(OnClickUpgradeItem);
        mBtnTitleText = mBtnTitle.GetChild("title").asTextField;
        mCount = mObj.GetChild("curNum").asTextField;
        mDesc = mObj.GetChild("infoEffect").asTextField;
    }

    public void UpdateUI(int equipmentId)
    {
        mData = Process.Instance.Player.ReqEquipment(equipmentId);
        //title
        mBtnTitleText.text = mData.ConfigData._name;
        bool canCompose = mData.CanUpgrade();
        if (canCompose)
        {
            mBtnTitleText.color = Color.green;            
        }
        else
        {
            mBtnTitleText.color = mData.Count > 0 ? Color.white : Color.gray;
        }
        mBtnTitle.enabled = canCompose ? true : false;

        //material        
        mCount.text = "所需材料：" + mData.GetMaterialDesc();
        mDesc.text = "效果：" + mData.ConfigData._desc;
    }

    private void OnClickUpgradeItem(EventContext context)
    {
        if (mData.CanUpgrade())
        {
            mData.Upgrade();
            UIManager.mEventDispatch.DispatchEvent(EventDefine.UPDATE_EQUIPMENT_UI);
            Process.Instance.Saved();
        }
    }

}
