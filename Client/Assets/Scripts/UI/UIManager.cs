using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using System.IO;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    public Dictionary<string, BaseWindow> mShownWindows = new Dictionary<string, BaseWindow>();
    public static EventDispatcher mEventDispatch;
    private BaseWindow mCurShownWindow;
    private int mCsvCount = 0;

    void Awake()
    {        
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        MyLog.Init(LogLevel.Log, 1000);
        UIConfig.defaultFont = "Microsoft YaHei";

        UIPackage.AddPackage("wuxia");
        LoadConfigs();
        mEventDispatch = new EventDispatcher();

        //UIConfig.verticalScrollBar = "ui://Basics/ScrollBar_VT";
        //UIConfig.horizontalScrollBar = "ui://Basics/ScrollBar_HZ";
        //UIConfig.popupMenu = "ui://Basics/PopupMenu";
        //UIConfig.buttonSound = (AudioClip)UIPackage.GetItemAsset("Basics", "click");       
    }

    private void LoadConfigs()
    {
        List<string> filePathList = ResourcesManager.GetCsvFileList();
        mCsvCount = filePathList.Count;
        MyLog.Log("Csv count:" + mCsvCount);
        ResourcesManager.CsvDict.Clear();
        for (int i = 0; i < filePathList.Count; i++)
        {
            StartCoroutine(LoadCsv(filePathList[i]));
        }
    }

    IEnumerator LoadCsv(string path)
    {
#if UNITY_EDITOR
        path = "file://" + path;
#endif
        WWW www = new WWW(path);
        yield return www;
        if (www.error == null)
        {
            MyLog.Log("Load :" + path);
            string[] array = www.text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            ReadCsv config = new ReadCsv(path, array);
            ResourcesManager.CsvDict.Add(path, config);
        }
        else
        {
            MyLog.LogError("load csv" + path + " error:" + www.error);
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void ShowWindow<T>(string componentName, object param = null, string packgeName = "wuxia") where T : BaseWindow
    {         
        string uiName = typeof(T).Name;
        MyLog.Log("ShowWindow:" + uiName);
        if (mCurShownWindow != null)
        {
            mCurShownWindow.Close();
        }
        if (!mShownWindows.ContainsKey(uiName))
        {
            T window = System.Activator.CreateInstance<T>();
            window.Awake(packgeName, componentName);
            window.Show(param);
            mShownWindows.Add(uiName, window);
            mCurShownWindow = window;
        }
        else
        {
            mShownWindows[uiName].Show(param);
            mCurShownWindow = mShownWindows[uiName];
        }
    }

    public void ShowSubWindow<T>(BaseWindow parent, string componentName, object param = null, string packgeName = "wuxia") where T : BaseWindow
    {
        string uiName = typeof(T).Name;
        MyLog.Log("ShowSubWindow:" + uiName);
        if (parent.mCurShownSubWindow != null)
        {
            parent.mCurShownSubWindow.Close();
        }
        if (!parent.mSubWindows.ContainsKey(uiName))
        {
            T window = System.Activator.CreateInstance<T>();
            window.Awake(packgeName, componentName);
            window.Show(param);
            parent.mSubWindows.Add(uiName, window);
            parent.mCurShownSubWindow = window;
        }
        else
        {
            parent.mSubWindows[uiName].Show(param);
        }
    }

    private void Update()
    {
        if (mCsvCount > 0 && ResourcesManager.CsvDict.Count == mCsvCount)
        {            
            mCsvCount = 0;
            ConfigManager.Instance.InitConfigs();
            MyLog.Log("Load csv over.");
            ShowWindow<LoginWindow>(WindowType.WINDOW_LOGIN);
        }
    }

    private void OnDestroy()
    {
        MyLog.StopThread();
    }

}
