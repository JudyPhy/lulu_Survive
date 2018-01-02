using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class ItemData
{
    public int ID { get { return mId; } }
    private int mId;

    public int Count { get { return mCount; } }
    private int mCount;

    public ItemType Type { get { return mType; } }
    private ItemType mType;

    

}
