using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class GameSaved {

    private static string _fileName = Path.Combine(Application.persistentDataPath, "saved.json");
    
    public static void SetData(object pObject)
    {
        string toSave = JsonMapper.ToJson(pObject);
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
        data.destination = int.Parse(jsonData["destination"].ToString());
        data.distance = int.Parse(jsonData["distance"].ToString());
        data.curStage = int.Parse(jsonData["curStage"].ToString());
        data.lastStoryId = int.Parse(jsonData["lastStoryId"].ToString());
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
