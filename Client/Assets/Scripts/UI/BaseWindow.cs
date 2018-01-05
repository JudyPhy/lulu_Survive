using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BaseWindow
{
    public GComponent mWindowObj;
    public object mParam;
    public Dictionary<string, BaseWindow> mSubWindows = new Dictionary<string, BaseWindow>();
    public BaseWindow mCurShownSubWindow;

    public void Awake(string packgeName, string componentName)
    {
        mWindowObj = UIPackage.CreateObject(packgeName, componentName).asCom;
        OnAwake();
    }

    public virtual void OnAwake() { /*Debug.LogError("BaseWindow OnAwake");*/ }

    public virtual void Show(object param)
    {
        mParam = param;
        OnRegisterEvent();
        if (mWindowObj != null)
        {
            GRoot.inst.AddChild(mWindowObj);
        }
        OnShownAni();
        OnEnable();
    }

    public virtual void OnShownAni() { }
    public virtual void OnEnable() { }

    public virtual void Close()
    {
        OnRemoveEvent();
        if (mWindowObj != null)
        {
            GRoot.inst.RemoveChild(mWindowObj);
        }
    }

    protected virtual void OnRegisterEvent() { }
    protected virtual void OnRemoveEvent() { }

}
