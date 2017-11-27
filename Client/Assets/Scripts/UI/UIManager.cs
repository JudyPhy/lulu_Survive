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

    public void MoveTowards()
    {
        mMainWindow.UpdateScene();
        mMainWindow.UpdateEnergy();
        mMainWindow.UpdateHungry();
    }

    private void Update()
    {
    }

}
