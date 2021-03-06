﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BottomNormal : BaseWindow
{
    private GButton[] mBtnList = new GButton[9];
    private GTextField mTextTips;

    public override void OnAwake()
    {
        GComponent btnCom = mWindowObj.GetChild("btnList").asCom;
        for (int i = 0; i < mBtnList.Length; i++)
        {
            mBtnList[i] = btnCom.GetChildAt(i).asButton;
            mBtnList[i].onClick.Add(OnClickBtn);
            mBtnList[i].visible = false;
        }
        mTextTips = mWindowObj.GetChild("textNormal").asTextField;
        mTextTips.text = "";
    }

    public override void OnEnable()
    {
        MyLog.Log("Show normal bottom.");
        UpdateBtns();
        mTextTips.text = "";
        if (Process.Instance.CurEventData._type == EventType.Idle)
        {
            UpdateIdleUI(Process.Instance.CurEventData._desc);
        }
        else if (Process.Instance.CurEventData._type == EventType.Drop)
        {
            UpdateDropUI(Process.Instance.CurEventData._id);
        }
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
            //MyLog.Log("Drop item count:" + drop._itemList.Count);
            for (int i = 0; i < drop._itemList.Count; i++)
            {
                ConfigItem item = ConfigManager.Instance.ReqItem(drop._itemList[i]._itemId);
                if (item != null)
                {
                    //MyLog.Log("get item id:" + drop._itemList[i]._itemId + ", count:" + drop._itemList[i]._countMax);
                    if (drop._itemList[i]._countMax > 0)
                    {
                        int getCount = drop._sceneId == 0 ? Random.Range(1, drop._itemList[i]._countMax) : Random.Range(0, drop._itemList[i]._countMax);
                        Process.Instance.Player.AddItem(drop._itemList[i]._itemId, getCount);
                        content_get.Add(item._name + "x" + getCount);
                    }
                    else if (drop._itemList[i]._countMax < 0)
                    {
                        int lossOrigCount = drop._sceneId == 0 ? Random.Range(1, Mathf.Abs(drop._itemList[i]._countMax)) : Random.Range(0, Mathf.Abs(drop._itemList[i]._countMax));
                        int lossCount = Mathf.Min(lossOrigCount, Process.Instance.Player.ReqItem(drop._itemList[i]._itemId).Count);
                        Process.Instance.Player.AddItem(drop._itemList[i]._itemId, -lossCount);
                        if (lossCount > 0)
                        {
                            content_loss.Add(item._name + "x" + lossCount);
                        }
                    }
                }
                else
                {
                    MyLog.LogError("item[" + drop._itemList[i]._itemId + "] not exist.");
                }
            }
            MyLog.Log("content_get count:" + content_get.Count);
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
            MyLog.Log("content_loss count:" + content_loss.Count);
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
        for (int i = 0; i < mBtnList.Length; i++)
        {
            mBtnList[i].visible = false;
        }
        ConfigScene scene = ConfigManager.Instance.ReqSceneData(Process.Instance.CurScene);
        if (scene != null)
        {
            mBtnList[0].visible = scene._shop != 0;  //shop
            mBtnList[3].visible = true;  //explore
            mBtnList[4].visible = scene._destination != 0; //towards
            mBtnList[5].visible = true; //sleep
            mBtnList[6].visible = true; //equip
            mBtnList[8].visible = true; //bag
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
            Process.Instance.Explore();
        }
        else if (btn == mBtnList[4])
        {
            OnClickTowards();
        }
        else if (btn == mBtnList[5])
        {
            OnClickSleep();                     
        }
        else if (btn == mBtnList[6])
        {
            UIManager.Instance.ShowWindow<EquipWindow>(WindowType.WINDOW_ITEMS);
        }
        else if (btn == mBtnList[8])
        {
            UIManager.Instance.ShowWindow<BagWindow>(WindowType.WINDOW_ITEMS);
        }
    }

    private void OnClickTowards()
    {
        if (Process.Instance.CanSwitchScene())
        {
            ConfigScene scene = ConfigManager.Instance.ReqSceneData(Process.Instance.CurScene);
            Process.Instance.SwitchScene(scene._destination);
            if (Process.Instance.NeedShowDialog())
            {
                MyLog.Log("Play dialog[" + Process.Instance.NextStoryID + "]");
                UIManager.Instance.ShowWindow<DialogWindow>(WindowType.WINDOW_MAIN, WindowType.WINDOW_MAIN);
            }
            else
            {
                UIManager.mEventDispatch.DispatchEvent(EventDefine.UPDATE_MAIN_UI);
            }
        }
        else
        {
            Process.Instance.MoveTowards();
        }
    }

    private void OnClickSleep()
    {
        if (Process.Instance.NeedShowDialog())
        {
            MyLog.Log("Play dialog[" + Process.Instance.NextStoryID + "]");
            UIManager.Instance.ShowWindow<DialogWindow>(WindowType.WINDOW_DIALOG, WindowType.WINDOW_SLEEP);
        }
        else
        {
            UIManager.Instance.ShowWindow<SleepWindow>(WindowType.WINDOW_SLEEP);
        }
    }
}
