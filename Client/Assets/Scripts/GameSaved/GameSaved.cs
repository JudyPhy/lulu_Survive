using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class GameSaved {

    private static string _fileName = Path.Combine(Application.persistentDataPath, "saved.json");

    public static bool HasHistory()
    {
        return File.Exists(_fileName);
    }
    
    public static void SetData(SavedData data)
    {
        string toSave = JsonMapper.ToJson(data);
        Debug.Log("toSave: " + toSave);
        //加密
        StreamWriter streamWriter = File.CreateText(_fileName);
        streamWriter.Write(toSave);
        streamWriter.Close();
    }

    public static SavedData GetData()
    {
        JsonData jsonData = GetJsonData();
        SavedData data = new SavedData();
        data.curScene = int.Parse(jsonData["curScene"].ToString());
        data.curPos = new int[2];
        data.curPos[0] = int.Parse(jsonData["curPos"][0].ToString());
        data.curPos[1] = int.Parse(jsonData["curPos"][1].ToString());
        data.curOutId = int.Parse(jsonData["curOutId"].ToString());
        data.lastStoryId = int.Parse(jsonData["nextStoryId"].ToString());
        data.gold = int.Parse(jsonData["gold"].ToString());
        
        data.role = new RoleAttr();
        data.role.healthy = int.Parse(jsonData["role"]["healthy"].ToString());
        data.role.energy = int.Parse(jsonData["role"]["energy"].ToString());
        data.role.hungry = int.Parse(jsonData["role"]["hungry"].ToString());
        data.role.hp = int.Parse(jsonData["role"]["hp"].ToString());
        data.role.atk = int.Parse(jsonData["role"]["atk"].ToString());
        data.role.def = int.Parse(jsonData["role"]["def"].ToString());

        data.itemList = new List<ItemCountData>();
        for (int i = 0; i < jsonData["itemList"].Count; i++)
        {
            ItemCountData item = new ItemCountData();
            item.id = int.Parse(jsonData["itemList"][i]["id"].ToString());
            item.count = int.Parse(jsonData["itemList"][i]["count"].ToString());
            data.itemList.Add(item);
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
        //解密
        if (data.Length > 0)
        {
            return JsonMapper.ToObject(data);
        }
        return null;
    }

}
