using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class EquipmentData : ItemData
{
    private ConfigEquipment mData;

    public EquipmentData(int id,int count)
    {
        mData = ConfigManager.Instance.ReqItem(id);
    }

    public void UpdateUI(ConfigEquipment data)
    {
        mData = data;
        //title
        ItemCountData countData = Process.Instance.GetSelfItem(mData._id);
        mBtnTitleText.text = mData._name;
        mBtnTitleText.color = countData.count > 0 ? Color.green : Color.white;
        mBtnTitle.enabled = countData.count > 0;
        //material
        List<string> descList = new List<string>();
        for (int i = 0; i < data._materialList.Count; i++)
        {
            ConfigItem item = ConfigManager.Instance.ReqItem(data._materialList[i]._materialId);
            if (item != null)
            {
                ItemCountData materialCount = Process.Instance.GetSelfItem(data._materialList[i]._materialId);
                string colorStr = materialCount.count >= data._materialList[i]._baseCount ? "[color =#00FF00]" : "[color =#00FF00]";
                string desc = item._name + "[" + colorStr + materialCount.count + "[/color]/" + data._materialList[i]._baseCount + "]";
                descList.Add(desc);
            }
        }
        mCount.text = "所需材料：";
        for (int i = 0; i < descList.Count; i++)
        {
            mCount.text += i == descList.Count - 1 ? descList[i] : descList[i] + ", ";
        }
        mDesc.text = "效果：" + mData._desc;
    }

    private void OnClickUpgradeItem(EventContext context)
    {

        if (Process.Instance.Player.GetEquipment(mData._id))
        {

        }
    }

}
