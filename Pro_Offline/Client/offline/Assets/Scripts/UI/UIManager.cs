﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    Dictionary<DefineWindow.WindowID, BaseWindow> mShownWindows = new Dictionary<DefineWindow.WindowID, BaseWindow>();

    DefineWindow.WindowID mCurWindow = DefineWindow.WindowID.Idle;

    private NarvigationBar mNavBar;

    const string mFirstUIName = "UIPanel";

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        GameObject defaultObj = GameObject.Find("UIPanel");
        defaultObj.name = "NarvigationBar";
        UIPanel panel = defaultObj.GetComponent<UIPanel>();
        panel.packageName = DefineWindow.OFFLINE;
        panel.componentName = "bottomMenu";
        mNavBar = defaultObj.AddComponent<NarvigationBar>();

        ShowWindow<BattleUI>(DefineWindow.WindowID.Battle);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowWindow<T>(DefineWindow.WindowID id) where T : BaseWindow
    {
        if (mCurWindow == id)
        {
            MyLog.Log("window has shown.");
            return;
        }
        if (mShownWindows.ContainsKey(mCurWindow))
            mShownWindows[mCurWindow].gameObject.SetActive(false);
        if (mShownWindows.ContainsKey(id))
        {
            mShownWindows[id].gameObject.SetActive(true);
        }
        else
        {
            string comName = DefineWindow.WindowCom(id);
            T script = AddObjTo<T>(GRoot.inst, DefineWindow.OFFLINE, comName);          
        }
        mCurWindow = id;
    }

    public void ShowTips(DefineWindow.TipsType type, string content)
    {
        MyLog.Log("type:" + type + " content:" + content);
        switch (type)
        {
            case DefineWindow.TipsType.Idle:
                //AddObjTo<TextTips>(GRoot.inst, DefineWindow.OFFLINE, "tips");
                break;
            default:
                break;
        }
    }

    public T AddObjTo<T>(Transform parent, string packName, string comName) where T : BaseWindow
    {
        GameObject obj = new GameObject(comName);
        obj.layer = LayerMask.NameToLayer("UI");
        if (parent != null)
            obj.transform.parent = parent;
        obj.transform.localScale = Vector3.one;
        obj.transform.localEulerAngles = Vector3.zero;

        UIPanel panel = obj.AddComponent<UIPanel>();
        panel.packageName = packName;
        panel.componentName = comName;
        T script = obj.AddComponent<T>();
        return script;
    }

    public T AddObjTo<T>(GComponent parent, string packName, string comName) where T : BaseWindow
    {
        GameObject obj = new GameObject(comName);
        obj.layer = LayerMask.NameToLayer("UI");
        obj.transform.localScale = Vector3.one;
        obj.transform.localEulerAngles = Vector3.zero;

        UIPanel panel = obj.AddComponent<UIPanel>();
        panel.packageName = packName;
        panel.componentName = comName;
        if (parent != null)
            parent.AddChild(panel.ui);       
        T script = obj.AddComponent<T>();        
        return script;
    }

}
