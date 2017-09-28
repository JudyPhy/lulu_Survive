using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class NetConfigData
{
    public string NetAddr = "127.0.0.1";
    public ushort Port = 3563;
    public bool UseLocalAddr = false;
}

public class NetConfig
{
    public static NetConfigData LoadConfig()
    {
        string filePath = Application.streamingAssetsPath + "/NetConfig.json";
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            if (sr == null)
            {
                return null;
            }
            string json = sr.ReadToEnd();
            if (json.Length > 0)
            {
                return JsonUtility.FromJson<NetConfigData>(json);
            }
        }
        return null;
    }
}