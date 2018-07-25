using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineWindow
{
    public const string OFFLINE = "Offline";

    public enum WindowID
    {
        Idle,
        Home,
        Battle,
        Loading,
    }

    public enum TipsType
    {
        Idle,
    }

    public static string WindowCom(WindowID id)
    {
        switch (id)
        {
            case WindowID.Home:
                return "mainui";
            case WindowID.Battle:
                return "battleui";
            case WindowID.Loading:
                return "loading";
            default:
                break;
        }
        return "";
    }

}