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
            case (int)EventType.Normal:
                {
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
            default:
                break;
        }
    }

    private void Update()
    {
    }

}
