using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using System.IO;

public class SavedData
{
    public int curScene;
    public int destination;
    public int distance;
    public int curStage;
    public int lastStoryId;

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
