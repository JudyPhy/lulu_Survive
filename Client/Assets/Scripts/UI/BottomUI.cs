using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;

public class BottomUI
{
    public GComponent mObj;

    public virtual void Show(bool show)
    {
        mObj.visible = show;
        if (show)
        {
            RegisterEvent();
        }
        else
        {
            RemoveEvent();
        }
    }

    public virtual void RegisterEvent() { }
    public virtual void RemoveEvent() { }

}
