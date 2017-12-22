using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BottomNormal : BottomUI
{
    private GButton[] mBtnList = new GButton[9];
    private GTextField mTextTips;

    public BottomNormal()
    {

        mObj = UIPackage.CreateObject("wuxia", "fn_nomal").asCom;
        mObj.visible = false;
        GComponent btnCom = mObj.GetChild("btnList").asCom;
        for (int i = 0; i < mBtnList.Length; i++)
        {
            mBtnList[i] = btnCom.GetChildAt(i).asButton;
            mBtnList[i].onClick.Add(OnClickBtn);
            mBtnList[i].visible = false;
        }
        mTextTips = mObj.GetChild("textNormal").asTextField;
        mTextTips.text = "";
    }

    private void HideAllBtns()
    {
        for (int i = 0; i < mBtnList.Length; i++)
        {
            mBtnList[i].visible = false;
        }
    }

    public override void Show(bool show)
    {
        MyLog.Log("Show normal bottom.");
        base.Show(show);
        UpdateBtns();
        mTextTips.text = "";
    }

    public void UpdateIdleUI(string text)
    {
        mTextTips.text = text;
    }

    public void UpdateDropUI(int dropId)
    {
        MyLog.Log("Drop shown, id=" + dropId);
        mTextTips.text = "";
        ConfigDrop drop = ConfigManager.Instance.ReqDrop(dropId);
        if (drop != null)
        {
            List<string> content_get = new List<string>();
            List<string> content_loss = new List<string>();
            //gold
            if (drop._gold > 0)
            {
                int addGold = Random.Range(1, drop._gold);
                Process.Instance.Player.UpdateGold(Process.Instance.Player.Gold + addGold);
                content_get.Add(addGold + "文钱");                
            }
            else if (drop._gold < 0)
            {
                int lossGoldCount = Random.Range(1, Mathf.Abs(drop._gold));
                lossGoldCount = Mathf.Min(Process.Instance.Player.Gold, lossGoldCount);
                Process.Instance.Player.UpdateGold(Process.Instance.Player.Gold - lossGoldCount);
                if (lossGoldCount > 0)
                {
                    content_loss.Add(lossGoldCount + "文钱");
                }
            }
            //items
            for (int i = 0; i < drop._itemList.Count; i++)
            {
                ConfigItem item = ConfigManager.Instance.ReqItem(drop._itemList[i]._itemId);
                if (item != null)
                {
                    if (drop._itemList[i]._count > 0)
                    {
                        Process.Instance.Player.UpdateItem(drop._itemList[i]._itemId, drop._itemList[i]._count);
                        content_get.Add(item._name + "x" + drop._itemList[i]._count);
                    }
                    else if (drop._itemList[i]._count < 0)
                    {
                        int lossCount = Mathf.Min(Mathf.Abs(drop._itemList[i]._count), Process.Instance.GetHasItem(drop._itemList[i]._itemId).count);
                        Process.Instance.Player.UpdateItem(drop._itemList[i]._itemId, -lossCount);
                        if (lossCount > 0)
                        {
                            content_loss.Add(item._name + "x" + Mathf.Abs(drop._itemList[i]._count));
                        }
                    }
                }
            }
            if (content_get.Count > 0)
            {
                mTextTips.text = "获得：";
                for (int i = 0; i < content_get.Count; i++)
                {
                    if (i != content_get.Count - 1)
                    {
                        mTextTips.text += content_get[i] + ", ";
                    }
                    else
                    {
                        mTextTips.text += content_get[i] + ".\n";
                    }
                }
            }
            if (content_loss.Count > 0)
            {
                mTextTips.text = "失去：";
                for (int i = 0; i < content_loss.Count; i++)
                {
                    if (i != content_loss.Count - 1)
                    {
                        mTextTips.text += content_loss[i] + ", ";
                    }
                    else
                    {
                        mTextTips.text += content_loss[i] + ".";
                    }
                }
            }
            Process.Instance.Saved();
        }
        else
        {
            MyLog.Log("Drop " + dropId + " not exist.");
        }
    }

    private void UpdateBtns()
    {
        MyLog.Log("Update normal bottom btns.");
        HideAllBtns();
        ConfigScene scene = ConfigManager.Instance.ReqSceneData(Process.Instance.CurScene);
        if (scene != null)
        {
            bool inScene = Process.Instance.InCurScene(scene, Process.Instance.CurPos);
            MyLog.Log("inScene:" + inScene);
            mBtnList[0].visible = inScene && scene._shop != 0;
            mBtnList[3].visible = inScene;
            mBtnList[4].visible = true;
            mBtnList[5].visible = true;
        }
        else
        {
            MyLog.LogError("Scene[" + Process.Instance.CurScene + "] not exist.");
        }
    }

    private void OnClickBtn(EventContext context)
    {
        GButton btn = (GButton)context.sender;
        if (btn == mBtnList[0])
        {
            MyLog.Log("Click shop.");
        }
        else if (btn == mBtnList[3])
        {
            MyLog.Log("Click explore.");
        }
        else if (btn == mBtnList[4])
        {
            MyLog.LogError("Click towards");
            Process.Instance.MoveTowards();
        }
        else if (btn == mBtnList[5])
        {
            MyLog.Log("Click bag.");
            if (Process.Instance.Player.Items.Count > 0)
            {
                UIManager.Instance.SwitchToUI(UIType.Bag);
            }
            else
            {
                UIManager.Instance.mMainWindow.Tips("没有任何物品");
            }
        }
    }

    public void Tips(string tips)
    {
        mTextTips.text = tips;
    }
}
