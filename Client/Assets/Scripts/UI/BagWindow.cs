using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BagWindow : Window
{
    private GList mList;
    private GButton mBtnBack;

    List<ConfigItem> mDataList = new List<ConfigItem>();

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("wuxia", "UI_items").asCom;
        this.Center();
        this.modal = true;

        mList = this.contentPane.GetChild("itemList").asList;
        mList.itemRenderer = RenderListItem;
        mList.onClickItem.Add(OnClickItem);
        mBtnBack = this.contentPane.GetChild("n1").asButton;
        mBtnBack.onClick.Add(OnClickBack);
    }

    override protected void DoShowAnimation()
    {
        this.SetScale(0.1f, 0.1f);
        this.SetPivot(0.5f, 0.5f);
        this.TweenScale(new Vector2(1, 1), 0.3f).SetEase(Ease.OutQuad).OnComplete(this.OnShown);
    }

    override protected void DoHideAnimation()
    {
        this.TweenScale(new Vector2(0.1f, 0.1f), 0.3f).SetEase(Ease.OutQuad).OnComplete(this.HideImmediately);
    }

    private void OnClickBack(EventContext context)
    {
        UIManager.Instance.SwitchToUI(UIType.Main);
    }

    private void OnClickItem(EventContext context)
    {
        GComponent item = (GComponent)context.data;

    }

    protected override void OnShown()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {        
        List<ConfigItem> costList = Process.Instance.GetItemList(ItemType.Cost);
        List<ConfigItem> materialList = Process.Instance.GetItemList(ItemType.Material);
        mDataList = new List<ConfigItem>(costList);
        mDataList.AddRange(materialList);
        mDataList.Sort((data1, data2) => { return data1._id.CompareTo(data2._id); });
        Debug.LogError("22222:" + mDataList.Count);
        mList.numItems = mDataList.Count;
        MyLog.Log("333333mDataList count=" + mDataList.Count);
    }

    private void RenderListItem(int index, GObject obj)
    {
        MyLog.Log("RenderListItem: index=" + index);
        if (index < mDataList.Count)
        {            
            Item itemObj = (Item)obj;
            itemObj.UpdateUI(mDataList[index]);
        }
    }
}
