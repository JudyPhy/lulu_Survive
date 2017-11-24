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

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        UIConfig.defaultFont = "Microsoft YaHei";

        UIPackage.AddPackage("UI/survival");

        //UIConfig.verticalScrollBar = "ui://Basics/ScrollBar_VT";
        //UIConfig.horizontalScrollBar = "ui://Basics/ScrollBar_HZ";
        //UIConfig.popupMenu = "ui://Basics/PopupMenu";
        //UIConfig.buttonSound = (AudioClip)UIPackage.GetItemAsset("Basics", "click");
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
    }

    private void PlayDialog()
    {
        Process.Instance.LoadDialog();
        if (Process.Instance.CurDialog.Count > 0)
        {
            _mDialogWindow = new DialogWindow();
        }
    }

    private void Update()
    {
    }

}
