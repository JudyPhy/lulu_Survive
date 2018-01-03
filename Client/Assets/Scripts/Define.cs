using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using System.IO;

public enum ItemType
{
    Idle,
    Material = 1,
    Cost = 2,
    Equipment = 3,
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

public enum UIType
{
    Idle,
    Login,
    Main,
    Dialog,
    Bag,
    Shop,
    Sleep,
    Equip,
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


public enum EventResultType
{
    Idle,
    Battle = 1,
    Drop = 2,
    Event = 3,
}

public class GameConfig
{
    public static readonly int SCENE_START_ID = 111;
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

public enum BattleAttr
{
    Idle,
    Atk = 1,
    Hp = 2,
    Def = 3,
    HpRecover = 4,
}



