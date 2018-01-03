using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class ItemData
{
    public int ID { get { return mId; } }
    protected int mId;

    public int Count { set { mCount = value; } get { return mCount; } }
    protected int mCount;

    public ItemType Type { get { return mType; } }
    protected ItemType mType;

    public ConfigItem ConfigItemData { get { return mItemConfigData; } }
    private ConfigItem mItemConfigData;

    public ItemData(int id)
    {
        mId = id;
        mCount = 0;
        mItemConfigData = ConfigManager.Instance.ReqItem(mId);
        mType = (ItemType)mItemConfigData._type;
    }

    public bool CanUse()
    {
        return mType == ItemType.Cost && mCount > 0;
    }

    public void Used()
    {
        Process.Instance.Player.BuffID = mId;
        Process.Instance.Player.BuffDuration = mItemConfigData._duration;
        Process.Instance.Player.AddHealthy(mItemConfigData._healthy);
        Process.Instance.Player.AddEnergy(mItemConfigData._energy);
        Process.Instance.Player.AddHungry(mItemConfigData._hungry);
        Process.Instance.Player.AddHp(mItemConfigData._hp);
        Process.Instance.Player.Power = Mathf.Max(0, Process.Instance.Player.Power + mItemConfigData._power);
        Process.Instance.Player.Agile = Mathf.Max(0, Process.Instance.Player.Agile + mItemConfigData._agile);
        Process.Instance.Player.Physic = Mathf.Max(0, Process.Instance.Player.Physic + mItemConfigData._physic);
        Process.Instance.Player.Charm = Mathf.Max(0, Process.Instance.Player.Charm + mItemConfigData._charm);
        Process.Instance.Player.Perception = Mathf.Max(0, Process.Instance.Player.Perception + mItemConfigData._perception);
    }

}
