using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using System.IO;

public class SavedData
{
    public int curScene;
    public int[] curPos;

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

public class ItemCountData
{
    public int id;
    public int count;
}

public class EventData
{
    public EventType _type;
    public int _id;

    public EventData(EventType type, int id)
    {
        _type = type;
        _id = id;
    }
}

public enum UIType
{
    Idle,
    Login,
    Main,
    Dialog,
}

public class GameConfig
{
    public static int COST_ENERGY_ONCE = 10;
    public static int COST_HUNGRY_ONCE = 10;
}
