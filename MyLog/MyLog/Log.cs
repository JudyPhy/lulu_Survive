using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;

public enum LogLevel
    {
        Info,
        Log,
        Warning,
        Error,
    }

public class MyLog
{
    static private LogLevel mLogLevel = LogLevel.Log;

    static string mLogDirPath = Application.persistentDataPath + "/log";

    static private Thread mLogThread = null;

    static private List<object> mLogList = new List<object>();

    static private readonly object mLock = new object();

    static private int mExportTimeInterval = 1000;

    public static void Init(LogLevel logLevel, int exportTimeInterval)
    {
        mLogLevel = logLevel;
        mExportTimeInterval = exportTimeInterval;

        mLogList.Add("Start ==================>>>>>>>>>>>>");
        mLogThread = new Thread(new ThreadStart(ProcessSaved));
        mLogThread.IsBackground = true;
        if (!mLogThread.IsAlive)
        {
            mLogThread.Start();
        }
    }

    private static void ProcessSaved()
    {
        while (true)
        {
            ExportLog();
            Thread.Sleep(mExportTimeInterval);
        }
    }

    static public void Log(object message)
    {
        if (mLogLevel > LogLevel.Log)
            return;
        Debug.Log(message);
        mLogList.Add("Log:" + message);
    }

    static public void LogWarning(object message)
    {
        if (mLogLevel > LogLevel.Warning)
            return;
        Debug.LogWarning(message);
        mLogList.Add("Warning:" + message);
    }

    static public void LogError(object message)
    {
        if (mLogLevel > LogLevel.Error)
            return;
        Debug.LogError(message);
        mLogList.Add("Error:" + message);
    }

    static private void ExportLog()
    {
        try
        {
            lock (mLock)
            {
                if (mLogList.Count > 0)
                {
                    string str = "";
                    for (int i = 0; i < mLogList.Count; i++)
                    {
                        str += i == mLogList.Count - 1 ? mLogList[i] : mLogList[i] + "\n";
                    }
                    mLogList.Clear();
                    //write to file
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
                    sw.WriteLine(str);
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
        }
        catch (Exception ex)
        {
        }
    }

    public static void StopThread()
    {
        if (mLogThread != null)
        {
            mLogThread.Abort();
        }
    }

}
