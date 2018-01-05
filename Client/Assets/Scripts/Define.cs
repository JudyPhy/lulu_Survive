using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using System.IO;

public class WindowType
{
    public static readonly string WINDOW_LOGIN = "UI_login";
    public static readonly string WINDOW_DIALOG = "UI_storyA";
    public static readonly string WINDOW_MAIN = "UI_main";
    public static readonly string WINDOW_SLEEP = "UI_sleep";
    public static readonly string WINDOW_ITEMS = "UI_items";

    public static readonly string SUBWINDOW_MAIN_NORMAL = "fn_nomal";
    public static readonly string SUBWINDOW_MAIN_EVENT = "fn_event";
    public static readonly string SUBWINDOW_MAIN_BATTLE = "fn_moster";
}

public class GameConfig
{
    public static readonly int SCENE_START_ID = 1;
    public static readonly int DIALOG_START_ID = 11101;
    public static readonly int PLAYER_CONFIG_ID = 69999;

    public static readonly int PLAYER_BASE_HEALTHY = 30;
    public static readonly int PLAYER_BASE_ENERGY = 100;
    public static readonly int PLAYER_BASE_ENERGY_MAX = 100;
    public static readonly int PLAYER_BASE_HUNGRY = 100;
    public static readonly int PLAYER_BASE_HUNGRY_MAX = 100;

    public static readonly int COST_ENERGY_ONCE = 10;
    public static readonly int COST_HUNGRY_ONCE = 10;
    public static readonly int RECOVER_ENERGY_ONCE = 100;

    public static readonly int MEDICINE_ID = 1003;
}

public enum ItemType
{
    Idle = 0,
    Material = 1,
    Cost = 2,
    Equipment = 3,
}

public enum DialogType
{
    Idle,
    ToNextDialog = 1,
    ChooseDialog = 2,
    ToNextScene = 3,
}

public enum DialogChooseResultType
{
    Idle,
    ToNextDialog = 1,
    ToScene = 2,
}

public enum EventType
{
    Idle,
    Battle = 1,
    Drop = 2,
    Event = 3,
}

public enum BattleAttr
{
    Idle,
    Atk = 1,
    Hp = 2,
    Def = 3,
    HpRecover = 4,
}

public class EventData
{
    public EventType _type;
    public int _id;
    public string _desc;

    public EventData(EventType type, int id, string desc = "")
    {
        _type = type;
        _id = id;
        _desc = desc;
    }
}


