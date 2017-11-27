using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class SavedData
{
    public int curScene;
    public int destination;
    public int distance;
    public int curStage;
    public int lastStoryId;

    public int healthy;
    public int energy;
    public int hungry;
    public int hp;
    public int atk;
    public int def;
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

    public int Destination { get { return _destination; } }
    private int _destination;

    public int Distance { get { return _distance; } }
    private int _distance;

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
        _player = new Role(999);
        _curScene = 1001;
        ConfigMap data = ConfigManager.Instance.ReqMapData(_curScene);
        _destination = data == null ? 0 : data._destination;
        _distance = data == null ? 0 : data._distance;
        _curStage = 0;
        _lastStoryId = 0;
        _curSceneEvents = ConfigManager.Instance.ReqEvents(_curScene);

        Saved();
    }

    public void Saved()
    {
        SavedData data = new SavedData();
        data.curScene = _curScene;
        data.destination = _destination;
        data.distance = _distance;
        data.curStage = _curStage;
        data.lastStoryId = _lastStoryId;

        data.healthy = _player.Healthy;
        data.energy = _player.Energy;
        data.hungry = _player.Hungry;
        data.hp = _player.Hp;
        data.atk = _player.Atk;
        data.def = _player.Def;
        GameSaved.SetData(data);
    }

    public void ReqHistoryData()
    {
        SavedData data = GameSaved.GetData();
        _curScene = data.curScene;
        _destination = data.destination;
        _distance = data.distance;
        _curStage = data.curStage;
        _lastStoryId = data.lastStoryId;
        _player = new Role(data.healthy, data.energy, data.hungry, data.hp, data.atk, data.def);        
        Debug.LogError("ReqHistoryData=> _curScene:" + _curScene + ", _destination:" + _destination + ", _distance:" + _distance + ", _curStage:" + _curStage + ", _lastStoryId:" + _lastStoryId);
        Debug.LogError("player: healthy:" + _player.Healthy + ", energy:" + _player.Energy + ", hungry:" + _player.Hungry + ", hp:" + _player.Hp + ", atk:" + _player.Atk + ", def:" + _player.Def);
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

    public void MoveTowards()
    {
        if (_player.Energy >= 10)
        {
            _player.Energy -= 10;
            UIManager.Instance.mMainWindow.UpdateEnergy();

            _player.Hungry -= 20;
            if (_player.Hungry < 0)
            {
                _player.Hungry = 0; 
                _player.Healthy--;
                UIManager.Instance.mMainWindow.UpdateHealthy();
                if (_player.Healthy <= 0)
                {
                    Debug.LogError("Game Over!");
                    UIManager.Instance.mMainWindow.Hide();
                    UIManager.Instance.mLoginWindow.Show();
                    return;
                }
            }
            UIManager.Instance.mMainWindow.UpdateHungry();

            _distance--;
            if (_distance <= 0)
            {
                Debug.Log("Switch to new scene.");
                _curScene = _destination;
                ConfigMap data = ConfigManager.Instance.ReqMapData(_curScene);
                _destination = data == null ? 0 : data._destination;
                _distance = data == null ? 0 : data._distance;
            }
            UIManager.Instance.mMainWindow.UpdateScene();
        }
        else
        {
        }
    }



}