using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using System.IO;

public class SavedData
{
    public int curScene;
    public int[] curPos;
    public int curOutId;

    public int lastStoryId;
    public int nextStoryId;

    public RoleAttr role;

    public int gold;

    public List<ItemCountData> itemList;
}

public class RoleAttr
{
    public int healthy;
    public int energy;
    public int hungry;
    public int hp;
    public int def;
    public int atk;
}

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
}

public class GameConfig
{
    public static int COST_ENERGY_ONCE = 10;
    public static int COST_HUNGRY_ONCE = 10;

    public static int MEDICINE_ID = 1003;
}
