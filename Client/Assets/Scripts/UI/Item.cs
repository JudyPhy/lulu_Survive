using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class Item : GComponent
{
    private GTextField mTitle;
    private GTextField mCount;
    private GTextField mDesc;

    private ConfigItem mData;

    private Item()
    {
        mTitle = this.GetChild("btnItemName/title").asTextField;
        mCount = this.GetChild("curNum").asTextField;
        mDesc = this.GetChild("infoEffect").asTextField;        
    }

    public void UpdateUI(ConfigItem data)
    {
        MyLog.Log("Update item ui, item=" + data._id);
        mData = data;
        ItemCountData countData = Process.Instance.GetHasItem(mData._id);
        mTitle.text = mData._name;
        mTitle.color = countData.count > 0 ? Color.green : Color.white;
        mCount.text = "拥有：" + countData.count + "个";
        mDesc.text = "效果：" + mData._desc;
    }
}
