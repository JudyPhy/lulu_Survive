using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class GameSaved
{
    private static string _fileName = Path.Combine(Application.persistentDataPath, "saved.json");

    public static bool HasHistory()
    {
        return File.Exists(_fileName);
    }

    public static void SaveData(SavedData data)
    {
        string toSave = JsonMapper.ToJson(data);
        //Debug.Log("toSave: " + toSave);
        StreamWriter streamWriter = File.CreateText(_fileName);
        streamWriter.Write(toSave);
        streamWriter.Close();
    }

    public static SavedData GetData()
    {
        JsonData jsonData = GetJsonData();

        SavedData data = new SavedData();

        data.curScene = int.Parse(jsonData["curScene"].ToString());

        data.lastStoryId = int.Parse(jsonData["lastStoryId"].ToString());
        data.nextStoryId = int.Parse(jsonData["nextStoryId"].ToString());

        data.role = new RoleAttr();
        data.role.healthy = int.Parse(jsonData["role"]["healthy"].ToString());
        data.role.energy = int.Parse(jsonData["role"]["energy"].ToString());
        data.role.hungry = int.Parse(jsonData["role"]["hungry"].ToString());
        data.role.hp = int.Parse(jsonData["role"]["hp"].ToString());
        data.role.atk = int.Parse(jsonData["role"]["atk"].ToString());
        data.role.def = int.Parse(jsonData["role"]["def"].ToString());
        data.role.power = int.Parse(jsonData["role"]["power"].ToString());
        data.role.agile = int.Parse(jsonData["role"]["agile"].ToString());
        data.role.physic = int.Parse(jsonData["role"]["physic"].ToString());
        data.role.charm = int.Parse(jsonData["role"]["charm"].ToString());
        data.role.perception = int.Parse(jsonData["role"]["perception"].ToString());
        data.role.buffId = int.Parse(jsonData["role"]["buffId"].ToString());
        data.role.buffDuration = int.Parse(jsonData["role"]["buffDuration"].ToString());

        data.gold = int.Parse(jsonData["gold"].ToString());

        data.itemList = new List<SavedData.ItemCountData>();
        for (int i = 0; i < jsonData["itemList"].Count; i++)
        {
            SavedData.ItemCountData item = new SavedData.ItemCountData();
            item.id = int.Parse(jsonData["itemList"][i]["id"].ToString());
            item.num = int.Parse(jsonData["itemList"][i]["num"].ToString());
            data.itemList.Add(item);
        }

        data.equipmentList = new List<SavedData.ItemCountData>();
        for (int i = 0; i < jsonData["equipmentList"].Count; i++)
        {
            SavedData.ItemCountData item = new SavedData.ItemCountData();
            item.id = int.Parse(jsonData["equipmentList"][i]["id"].ToString());
            item.num = int.Parse(jsonData["equipmentList"][i]["num"].ToString());
            data.equipmentList.Add(item);
        }

        return data;
    }

    private static JsonData GetJsonData()
    {
        StreamReader streamReader = new StreamReader(_fileName);
        if (streamReader == null)
        {
            return null;
        }
        string data = streamReader.ReadToEnd();
        streamReader.Close();
        if (data.Length > 0)
        {
            return JsonMapper.ToObject(data);
        }
        return null;
    }
}

public class SavedData
{
    public struct ItemCountData
    {
        public int id;
        public int num;
    }

    public int day;

    public int curScene;

    public int lastStoryId;
    public int nextStoryId;

    public RoleAttr role;

    public int gold;

    public List<ItemCountData> itemList;

    public List<ItemCountData> equipmentList;
}

public class RoleAttr
{
    public int healthy;
    public int energy;
    public int hungry;

    public int hp;
    public int def;
    public int atk;
    public int power;
    public int agile;
    public int physic;
    public int charm;
    public int perception;

    public int buffId;
    public int buffDuration;
}
