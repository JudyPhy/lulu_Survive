using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ResourcesManager
{
    public static Dictionary<string, ReadCsv> CsvDict = new Dictionary<string, ReadCsv>();

    public static List<string> GetCsvFileList()
    {
        List<string> list = new List<string>();
        string dirPath = GetCsvDirPath();
        DirectoryInfo dir = new DirectoryInfo(dirPath);
        FileInfo[] fileInfo = dir.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            if (file.Extension == ".csv")
            {
                list.Add(file.FullName);
            }
        }
        return list;
    }

    private static string GetCsvDirPath()
    {
#if UNITY_EDITOR
        string filepath = Application.dataPath + "/StreamingAssets/csv/";

#elif UNITY_IPHONE
	  string filepath = Application.dataPath +"/Raw/csv/";
 
#elif UNITY_ANDROID
	  string filepath = "jar:file://" + Application.dataPath + "!/assets/csv/";
 
#endif
        return filepath;
    }

}
