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

}
