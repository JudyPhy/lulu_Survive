using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;

public class BottomUI
{
    GComponent mObj;

    public virtual void Show(bool show)
    {
        mObj.visible = show;
    }

}
