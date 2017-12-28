using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BagWindow : Window
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
            mTextTop[index] = com.GetChild("title").asTextField;
            index++;
            mTextTop[index] = com.GetChild("value").asTextField;
            index++;
        }
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
        Process.Instance.CurEventData = null;
        UIManager.Instance.SwitchToUI(UIType.Main);
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
        mItemList.Clear();
        mList.numItems = mDataList.Count;        
        UpdateTopAttr();
    }

    public void UpdateTopAttr()
    {
        mTextTop[0].text = "健康：";
        mTextTop[1].text = Process.Instance.Player.Healthy.ToString();
        mTextTop[2].text = "精力：";
        mTextTop[3].text = Process.Instance.Player.Energy.ToString();
        mTextTop[4].text = "饥饿：";
        mTextTop[5].text = Process.Instance.Player.Hungry.ToString();
    }

    private void RenderListItem(int index, GObject obj)
    {
        //MyLog.Log("RenderListItem: index=" + index);
        if (index < mDataList.Count)
        {
            GComponent itemObj = obj.asCom;            
            Item item = new Item(itemObj);
            item.UpdateUI(mDataList[index]);            
        }
    }
}
