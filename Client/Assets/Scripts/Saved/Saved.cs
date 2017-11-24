using UnityEngine;
using System.Collections;
using System.IO;
using SimpleJSON;

public class GameSaved {

    private static string _fileName = Path.Combine(Application.persistentDataPath, "saved.json");

    public static bool IsFileExists()
    {
        return File.Exists(_fileName);
    }

    public static void SetData(object pObject)
    {
        string toSave = JsonMapper.ToJson(pObject);
        Debug.LogError("toSave: " + toSave);
        //加密
        StreamWriter streamWriter = File.CreateText(_fileName);
        streamWriter.Write(toSave);
        streamWriter.Close();
    }

    public static SavedData GetData()
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
            return JsonMapper.ToObject<SavedData>(data);
        }
        return null;
    }

}
