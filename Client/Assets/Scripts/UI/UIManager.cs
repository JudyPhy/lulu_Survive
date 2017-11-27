using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private GComponent _mainView;
    private DialogWindow _mDialogWindow;
    private MainWindow _mMainWindow;

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

        _mDialogWindow = new DialogWindow();
        _mMainWindow = new MainWindow();
    }

    void Start()
    {
        Application.targetFrameRate = 60;

        _mainView = this.GetComponent<UIPanel>().ui;

        if (GameSaved.IsFileExists())
        {
            Process.Instance.ReqHistoryData();
        }
        else
        {
            Process.Instance.StartGame();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        PlayDialog();
        ShowMainUI();        
    }

    private void PlayDialog()
    {
        Debug.Log("PlayDialog");
        Process.Instance.LoadDialog();
        Debug.Log("current dialog lines count:" + Process.Instance.CurDialog.Count);
        if (Process.Instance.CurDialog.Count > 0)
        {
            
        }
    }

    private void ShowMainUI()
    {
        Debug.Log("ShowMainUI");
        _mMainWindow.Show();
    }

    private void Update()
    {
    }

}
