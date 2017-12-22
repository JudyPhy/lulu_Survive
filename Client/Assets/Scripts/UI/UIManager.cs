using UnityEngine;
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
    public BagWindow mBagWindow;

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

        mDialogWindow = new DialogWindow();
        mLoginWindow = new LoginWindow();
        mMainWindow = new MainWindow();
        mBagWindow = new BagWindow();
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
                mBagWindow.Hide();
                mDialogWindow.Show();
                break;
            case UIType.Login:                
                mMainWindow.Hide();
                mDialogWindow.Hide();
                mBagWindow.Hide();
                mLoginWindow.Show();
                break;
            case UIType.Main:
                mLoginWindow.Hide();
                mDialogWindow.Hide();
                mBagWindow.Hide();
                mMainWindow.Show();
                break;
            case UIType.Bag:
                mLoginWindow.Hide();
                mDialogWindow.Hide();
                mMainWindow.Hide();
                mBagWindow.Show();
                break;
        }
    }

    private void OnDestroy()
    {
        MyLog.StopThread();
    }

}
