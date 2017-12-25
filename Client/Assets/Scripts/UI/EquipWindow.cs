using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class EquipWindow : Window
{
    private GList mList;
    private GButton mBtnBack;
    private GTextField[] mTextTop = new GTextField[6];

    private List<ConfigItem> mDataList = new List<ConfigItem>();
    private List<Item> mItemList = new List<Item>();

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("wuxia", "UI_items").asCom;
        this.Center();
        this.modal = true;

        mList = this.contentPane.GetChild("itemList").asList;
        mList.itemRenderer = RenderListItem;
        mBtnBack = this.contentPane.GetChild("n1").asButton;
        mBtnBack.onClick.Add(OnClickBack);
        int index = 0;
        for (int i = 4; i < 7; i++)
        {
            GComponent com = this.contentPane.GetChild("n" + i.ToString()).asCom;
            mTextTop[index++] = com.GetChild("title").asTextField;
            mTextTop[index++] = com.GetChild("value").asTextField;
        }
    }

    private void OnClickBack(EventContext context)
    {
        Process.Instance.CurEventData = null;
        UIManager.Instance.SwitchToUI(UIType.Main);
    }

    protected override void OnShown()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        
    }

    private void RenderListItem(int index, GObject obj)
    {
        //MyLog.Log("RenderListItem: index=" + index);
        if (index < mDataList.Count)
        {
            GComponent itemObj = obj.asCom;
            Item item = new Item(itemObj);
            //item.UpdateUI(mDataList[index]);
        }
    }
}
