using UnityEngine;
using System.Collections;
using System.IO;

public class ResourcesManager {

    private static ResourcesManager instance;
    public static ResourcesManager Instance {
        get {
            if (instance == null) {
                instance = new ResourcesManager();
            }
            return instance;
        }
    }

    public static string GetCsvConfigFilePath(string csvName) {
        string path = Application.persistentDataPath + "/lulu/CSVConfig/" + csvName + ".csv";
        if (!File.Exists(path)) {
            path = Application.streamingAssetsPath + "/CSVConfig/" + csvName + ".csv";
        }
        return path;
    }

    public string GetResPath(string resName)
    {
        switch (resName)
        {
            case "SidePai":
                return "3d/model/" + resName;
            case "Item_pai_3d":
                return "3d/model/pai";
            default:
                break;
        }
        return "Prefabs/UI/" + resName;
    }

    public GameObject GetUIPrefabs(string path) {
        //Debug.LogError("path:" + path);
        Object obj = Resources.Load(path);
        GameObject go = GameObject.Instantiate(obj) as GameObject;
        return go;
    }

    public GameObject GetModelPrefab(string modelName) {
        string path = GetResPath(modelName);
        GameObject obj = GetUIPrefabs(path);
        return obj;
    }

}
