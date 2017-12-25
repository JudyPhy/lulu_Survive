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
}

public class ItemCountData
{
    public int id;
    public int count;
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

public class GameConfig
{
    public static readonly int DIALOG_START_ID = 111101;
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
