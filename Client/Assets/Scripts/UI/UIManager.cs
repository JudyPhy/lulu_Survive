using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    private GComponent _mainView;

    public DialogWindow mDialogWindow;
    public LoginWindow mLoginWindow;
    public MainWindow mMainWindow;
    public BottomWindow mBottomWindow;
    public EventWindow mEventWindow;
    public BattleWindow mBattleWindow;

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

        mBottomWindow = new BottomWindow();
        mEventWindow = new EventWindow();
        mBattleWindow = new BattleWindow();
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        _mainView = this.GetComponent<UIPanel>().ui;
        mLoginWindow.Show();
    }

    public void UpdateUI()
    {
        Debug.Log("PlayDialog");
        Process.Instance.LoadDialog();
        Debug.Log("current dialog lines count:" + Process.Instance.CurDialog.Count);
        if (Process.Instance.CurDialog.Count > 0)
        {

        }
        else
        {
            mMainWindow.Show();
            mBottomWindow.Show();
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
                        Debug.LogError("Has no this event[" + curEvent._event + "].");
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
                        Debug.LogError("Has no this monster[" + curEvent._event + "].");
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
            List<string> get = new List<string>();
            List<string> loss = new List<string>();
            for (int i = 0; i < drop._itemList.Count; i++)
            {
                int itemId = drop._itemList[i]._item;
                ConfigItem item = ConfigManager.Instance.ReqItem(itemId);
                if (item != null)
                {
                    string itemDesc = "";
                    switch (drop._itemList[i]._item)
                    {
                        case 1001:
                            itemDesc = Mathf.Abs(drop._itemList[i]._count) + "文钱";
                            break;
                        default:
                            itemDesc = item._name + "×" + Mathf.Abs(drop._itemList[i]._count);
                            break;
                    }
                    if (drop._itemList[i]._count >= 0)
                    {
                        get.Add(itemDesc);
                    }
                    else
                    {
                        loss.Add(itemDesc);
                    }
                }
            }
            string text = "";
            if (get.Count > 0)
            {
                text += "获得：";
                for (int i = 0; i < get.Count; i++)
                {
                    text += i == get.Count - 1 ? get[i] + "." : get[i] + ", ";
                }
                if (loss.Count > 0)
                {
                    text += " 失去：";
                    for (int i = 0; i < loss.Count; i++)
                    {
                        text += i == loss.Count - 1 ? loss[i] + "." : loss[i] + ", ";
                    }
                }                
            }
            else
            {
                if (loss.Count > 0)
                {
                    text += "失去：";
                    for (int i = 0; i < loss.Count; i++)
                    {
                        text += i == loss.Count - 1 ? loss[i] + "." : loss[i] + ", ";
                    }
                }
            }
            mBottomWindow.Tips(text);
        }
        else
        {
            Debug.LogError("Has no this drop[" + curEvent._event + "].");
        }
    }

    private void Update()
    {
    }

}
