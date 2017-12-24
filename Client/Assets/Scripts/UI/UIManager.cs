using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    private GComponent _mainView;
 
    public Dictionary<UIType, Window> mWindows = new Dictionary<UIType, Window>();
    public LoginWindow mLoginWindow;
    public MainWindow mMainWindow;
    public BagWindow mBagWindow;
    public DialogWindow mDialogWindow;
    public SleepWindow mSleedpWindow;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        MyLog.Init(LogLevel.Log, 1000);

        UIConfig.defaultFont = "Microsoft YaHei";

        UIPackage.AddPackage("wuxia");
        ConfigManager.Instance.InitConfigs();

        //UIConfig.verticalScrollBar = "ui://Basics/ScrollBar_VT";
        //UIConfig.horizontalScrollBar = "ui://Basics/ScrollBar_HZ";
        //UIConfig.popupMenu = "ui://Basics/PopupMenu";
        //UIConfig.buttonSound = (AudioClip)UIPackage.GetItemAsset("Basics", "click");
        LoadAllUI();
    }

    private void LoadAllUI()
    {
        mLoginWindow = new LoginWindow();
        mWindows.Add(UIType.Login, mLoginWindow);

        mMainWindow = new MainWindow();
        mWindows.Add(UIType.Main, mMainWindow);

        mDialogWindow = new DialogWindow();
        mWindows.Add(UIType.Dialog, mDialogWindow);

        mBagWindow = new BagWindow();
        mWindows.Add(UIType.Bag, mBagWindow);

        mSleedpWindow = new SleepWindow();
        mWindows.Add(UIType.Sleep, mSleedpWindow);
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        _mainView = this.GetComponent<UIPanel>().ui;
        _mainView.visible = false;
        SwitchToUI(UIType.Login);
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
        foreach (UIType windowType in mWindows.Keys)
        {
            //Debug.LogError("windowType:" + windowType.ToString() + ", type:" + type.ToString());
            if (windowType == type)
            {
                mWindows[windowType].Show();
            }
            else
            {
                mWindows[windowType].Hide();
            }
        }
    }

    private void OnDestroy()
    {
        MyLog.StopThread();
    }

}
