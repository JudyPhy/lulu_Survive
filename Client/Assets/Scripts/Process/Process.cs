using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class SavedData
{
    public int curScene;
    public int curStage;
    public int lastStoryId;
    public int hp;
}

public class Process
{
    private static Process _instance;
    public static Process Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Process();
            return _instance;
        }
    }

    public int CurScene { get { return _curScene; } }
    private int _curScene;

    public int CurStage { get { return _curStage; } }
    private int _curStage;

    public int LastStoryID { get { return _lastStoryId; } }
    private int _lastStoryId;

    public Role Player { get { return _player; } }
    private Role _player;

    List<ConfigEventPackage> _curSceneEvents;

    public List<string> CurDialog = new List<string>();

    public void StartGame()
    {
        Debug.Log("StartGame=>");
        _player = new Role(100);
        _curScene = 1001;
        _curStage = 0;
        _lastStoryId = 0;
        _curSceneEvents = ConfigManager.Instance.GetEvents(_curScene);

        Saved();
    }

    public void Saved()
    {
        SavedData data = new SavedData();
        data.curScene = _curScene;
        data.hp = _player.Hp;
        data.curStage = _curStage;
        data.lastStoryId = _lastStoryId;
        GameSaved.SetData(data);
    }

    public void ReqHistoryData()
    {
        SavedData data = GameSaved.GetData();
        _curScene = data.curScene;
        _player = new Role(data.hp);
        _curStage = data.curStage;
        _lastStoryId = data.lastStoryId;
        Debug.LogError("ReqHistoryData=> _curScene:" + _curScene + ", _curStage:" + _curStage + ", _lastStoryId:" + _lastStoryId);
        Debug.LogError("player: hp:" + _player.Hp);
    }

    public void LoadDialog()
    {
        List<ConfigStory> storyList = ConfigManager.Instance.ReqStory(_curScene);
        storyList.Sort((data1, data2) => { return data1._id.CompareTo(data2._id); });
        if (storyList.Count > 0)
        {
            for (int i = 0; i < storyList.Count; i++)
            {
                if (_curStage >= storyList[i]._condition && storyList[i]._id > _lastStoryId)
                {
                    Debug.Log("curStory id=" + storyList[i]._id);
                    List<string> strs = new List<string>();
                    int startIndex = 0;
                    for (int j = 0; j < storyList[i]._desc.Length; j++)
                    {
                        if (storyList[i]._desc[i] == '|')
                        {
                            string context = storyList[i]._desc.Substring(startIndex, i - startIndex);
                            Debug.Log("context:" + context);
                            strs.Add(context);
                            startIndex = i + 1;
                        }
                    }
                    CurDialog = strs;
                    break;
                }
            }
        }
    }



}