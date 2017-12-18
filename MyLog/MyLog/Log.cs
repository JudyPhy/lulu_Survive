using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

    public enum LogLevel
    {
        Info,
        Log,
        Warning,
        Error,
    }

public class MyLog
{
    static public LogLevel mLogLevel = LogLevel.Log;

    static string mLogDirPath = Application.persistentDataPath + "/log";

    static public void Log(object message)
    {
        if (mLogLevel > LogLevel.Log)
            return;
        Debug.Log(message);
        ExportLog("Log:" + message);
    }

    static public void LogWarning(object message)
    {
        if (mLogLevel > LogLevel.Warning)
            return;
        Debug.LogWarning(message);
        ExportLog("Warning:" + message);
    }

    static public void LogError(object message)
    {
        if (mLogLevel > LogLevel.Error)
            return;
        Debug.LogError(message);
        ExportLog("Error:" + message);
    }

    static private void ExportLog(object message)
    {
        if (!Directory.Exists(mLogDirPath))
        {
            Directory.CreateDirectory(mLogDirPath);
        }
        DirectoryInfo folder = new DirectoryInfo(mLogDirPath);
        List<string> logFiles = new List<string>();
        foreach (FileInfo file in folder.GetFiles())
        {
            logFiles.Add(file.Name);
        }
        logFiles.Sort();

        string today = DateTime.Now.ToString("yyyyMMdd");
        string filePath = mLogDirPath + "/" + today;
        bool isExist = File.Exists(filePath);
        StreamWriter sw = new StreamWriter(filePath, true);
        sw.WriteLine(message);
        sw.Close();
        if (!isExist && logFiles.Count >= 10)
        {
            while (logFiles.Count == 10)
            {
                File.Delete(mLogDirPath + "/" + logFiles[0]);
            }
        }
    }


}
