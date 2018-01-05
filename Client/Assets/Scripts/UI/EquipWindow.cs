using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class EquipWindow : BaseWindow
{
    private GList mList;
    private GButton mBtnBack;
    private GTextField[] mTextTop = new GTextField[6];

    private List<ConfigEquipment> mDataList = new List<ConfigEquipment>();
    private List<EquipItem> mItemList = new List<EquipItem>();

    public override void OnAwake()
    {
        mList = mWindowObj.GetChild("itemList").asList;
        mList.itemRenderer = RenderListItem;
        mBtnBack = mWindowObj.GetChild("n1").asButton;
        mBtnBack.onClick.Add(OnClickBack);
        int index = 0;
        for (int i = 4; i < 7; i++)
        {
            GComponent com = mWindowObj.GetChild("n" + i.ToString()).asCom;
            mTextTop[index++] = com.GetChild("title").asTextField;
            mTextTop[index++] = com.GetChild("value").asTextField;
        }
    }

    private void OnClickBack(EventContext context)
    {
        Process.Instance.CurEventData = null;
        UIManager.Instance.ShowWindow<MainWindow>(WindowType.WINDOW_MAIN);
    }

    protected override void OnRegisterEvent()
    {
        UIManager.mEventDispatch.AddEventListener(EventDefine.UPDATE_EQUIPMENT_UI, OnEnable);
    }

    protected override void OnRemoveEvent()
    {
        UIManager.mEventDispatch.RemoveEventListener(EventDefine.UPDATE_EQUIPMENT_UI, OnEnable);
    }

    public override void OnEnable()
    {
        mDataList = ConfigManager.Instance.ReqEquipList();
        //MyLog.Log("Equipment count:" + mDataList.Count);
        mDataList.Sort((data1, data2) => { return data1._id.CompareTo(data2._id); });
        mItemList.Clear();
        mList.numItems = mDataList.Count;
    }

    private void RenderListItem(int index, GObject obj)
    {
        //MyLog.Log("RenderListItem: index=" + index);
        if (index < mDataList.Count)
        {
            GComponent itemObj = obj.asCom;
            EquipItem item = new EquipItem(itemObj);
            item.UpdateUI(mDataList[index]._id);
        }
    }
}
