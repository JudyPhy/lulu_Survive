using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class EquipmentData : ItemData
{
    public ConfigEquipment ConfigData { get { return mConfigData; } }
    private ConfigEquipment mConfigData;

    public int Lev { set { mLev = value; } get { return mLev; } }
    private int mLev;

    public EquipmentData(int id) : base(0)
    {
        mId = id;
        mType = ItemType.Equipment;
        mCount = 0;
        mLev = 0;
        mConfigData = ConfigManager.Instance.ReqEquipment(id);
    }

    public bool CanUpgrade()
    {
        if (mConfigData != null)
        {
            for (int i = 0; i < mConfigData._materialList.Count; i++)
            {
                int needCount = mConfigData._materialList[i]._baseCount + mLev * mConfigData._materialList[i]._increaseCount;
                int curCount = Process.Instance.Player.ReqItem(mConfigData._materialList[i]._materialId).Count;
                if (curCount < needCount)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public string GetMaterialDesc()
    {
        List<string> descList = new List<string>();
        for (int i = 0; i < mConfigData._materialList.Count; i++)
        {
            ItemData material = Process.Instance.Player.ReqItem(mConfigData._materialList[i]._materialId);
            if (material.ConfigItemData != null)
            {
                string name = material.ConfigItemData._name;
                int needCount = mConfigData._materialList[i]._baseCount + mLev * mConfigData._materialList[i]._increaseCount;
                int curCount = Process.Instance.Player.ReqItem(mConfigData._materialList[i]._materialId).Count;
                string count = curCount.ToString() + "/" + needCount.ToString();
                string color = curCount >= needCount ? "[color=#00FF00]" : "[color=#FFFFFF]";
                string desc = color + name + "[" + count + "]" + "[/color]/";
                descList.Add(desc);
            }
        }
        string result = "";
        for (int i = 0; i < descList.Count; i++)
        {
            result += i == descList.Count - 1 ? descList[i] : descList[i] + ", ";
        }
        return result;
    }

    public void Upgrade()
    {
        for (int i = 0; i < mConfigData._materialList.Count; i++)
        {
            int costCount = mConfigData._materialList[i]._baseCount + mLev * mConfigData._materialList[i]._increaseCount;
            Process.Instance.Player.AddItem(mConfigData._materialList[i]._materialId, -costCount);
        }
        mLev++;
    }
}
