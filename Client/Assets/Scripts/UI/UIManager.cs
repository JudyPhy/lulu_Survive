﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    private GComponent _mainView;

 
    public LoginWindow mLoginWindow;
    public DialogWindow mDialogWindow;
    public MainWindow mMainWindow;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        UIConfig.defaultFont = "Microsoft YaHei";

        UIPackage.AddPackage("wuxia");
        ConfigManager.Instance.InitConfigs();

        //UIConfig.verticalScrollBar = "ui://Basics/ScrollBar_VT";
        //UIConfig.horizontalScrollBar = "ui://Basics/ScrollBar_HZ";
        //UIConfig.popupMenu = "ui://Basics/PopupMenu";
        //UIConfig.buttonSound = (AudioClip)UIPackage.GetItemAsset("Basics", "click");

        mDialogWindow = new DialogWindow();
        mLoginWindow = new LoginWindow();
        mMainWindow = new MainWindow();
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        _mainView = this.GetComponent<UIPanel>().ui;
        mLoginWindow.Show();
    }

    public void EnterGame()
    {
        MyLog.Log("EnterGame");
        if (Process.Instance.NeedShowDialog())
        {
            MyLog.Log("Play dialog[" + Process.Instance.NextStoryID + "]");
            SwitchToUI(UIType.Dialog);
        }
        else
        {
            SwitchToUI(UIType.Main);
        }
    }

    public void SwitchToUI(UIType type)
    {
        switch (type)
        {
            case UIType.Dialog:
                mLoginWindow.Hide();
                mMainWindow.Hide();
                mDialogWindow.Show();
                break;
            case UIType.Login:
                mLoginWindow.Show();
                mMainWindow.Hide();
                mDialogWindow.Hide();
                break;
            case UIType.Main:
                mLoginWindow.Hide();
                mMainWindow.Show();
                mDialogWindow.Hide();
                break;
        }
    }

    public void UpdateEvent(ConfigEventPackage curEvent)
    {
        switch (curEvent._type)
        {
            case (int)EventType.Event:
                {
                    mBottomWindow.Hide();
                    ConfigEvent eventInfo = ConfigManager.Instance.ReqEvent(curEvent._event);
                    if (eventInfo != null)
                    {
                        mEventWindow.EventInfo = eventInfo;
                        mEventWindow.Show();
                    }
                    else
                    {
                        MyLog.LogError("Has no this event[" + curEvent._event + "].");
                    }
                }
                break;
            case (int)EventType.Battle:
                {
                    mBottomWindow.Hide();
                    ConfigMonster monster = ConfigManager.Instance.ReqMonster(curEvent._event);
                    if (monster != null)
                    {
                        mBattleWindow.mMonsterInfo = monster;
                        mBattleWindow.Show();
                    }
                    else
                    {
                        MyLog.LogError("Has no this monster[" + curEvent._event + "].");
                    }
                }
                break;
            case (int)EventType.Drop:
                DropEvent(curEvent);
                break;
            default:
                break;
        }
    }

    private void DropEvent(ConfigEventPackage curEvent)
    {
        mBottomWindow.Show();
        ConfigDrop drop = ConfigManager.Instance.ReqDrop(curEvent._event);
        if (drop != null)
        {
            Dictionary<ConfigItem, int> get = new Dictionary<ConfigItem, int>();
            Dictionary<ConfigItem, int> loss = new Dictionary<ConfigItem, int>();
            for (int i = 0; i < drop._itemList.Count; i++)
            {
                int itemId = drop._itemList[i]._item;
                ConfigItem item = ConfigManager.Instance.ReqItem(itemId);
                if (item != null)
                {                    
                    if (drop._itemList[i]._count >= 0)
                    {
                        get.Add(item, drop._itemList[i]._count);
                    }
                    else
                    {
                        int count = Process.Instance.GetItemCount(drop._itemList[i]._item);
                        loss.Add(item, Mathf.Min(count, Mathf.Abs(drop._itemList[i]._count)));
                    }
                }
            }
            Process.Instance.UpdateItems(get, loss);
            ShowDropDesc(get, loss);            
        }
        else
        {
            MyLog.LogError("Has no this drop[" + curEvent._event + "].");
        }
    }

    private void ShowDropDesc(Dictionary<ConfigItem, int> get, Dictionary<ConfigItem, int> loss)
    {
        string text = "";
        if (get.Count > 0)
        {
            text += "获得：";
            foreach (ConfigItem item in get.Keys)
            {
                text += GetItemText(item, get[item]) + ", ";
            }
            text = text.Substring(0, text.Length - 2) + ". ";
            if (loss.Count > 0)
            {
                text += " 失去：";
                foreach (ConfigItem item in loss.Keys)
                {
                    text += GetItemText(item, loss[item]) + ", ";
                }
                text = text.Substring(0, text.Length - 2) + ". ";
            }
        }
        else
        {
            if (loss.Count > 0)
            {
                text += " 失去：";
                foreach (ConfigItem item in loss.Keys)
                {
                    text += GetItemText(item, loss[item]) + ", ";
                }
                text = text.Substring(0, text.Length - 2) + ". ";
            }
        }
        mBottomWindow.Tips(text);
    }

    private string GetItemText(ConfigItem item, int count)
    {
        switch (item._id)
        {
            case 1001:
                return Mathf.Abs(count) + "文钱";
            default:
                break;
        }
        return item._name + "×" + count;
    }

    private void Update()
    {
    }

}
