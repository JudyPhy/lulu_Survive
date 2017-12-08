using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using Log;

public enum EventType
{
    Idle = 0,
    Event,
    Battle,
    Drop,
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

    public int NextStoryID { get { return _nextStoryId; } }
    private int _nextStoryId;

    public Role Player { get { return _player; } }
    private Role _player;

    List<ConfigEventPackage> _curSceneEvents;

    public List<string> CurDialog = new List<string>();

    public void StartGame()
    {
        Debug.Log("StartGame=>");
        _player = new Role(69999);
        UpdateScene(1001);
        _lastStoryId = 0;
        _nextStoryId = 100101;

        Saved();
    }

    public void Saved()
    {
        SavedData data = new SavedData();
        data.curScene = _curScene;
        data.destination = _destination;
        data.distance = _distance;
        data.curStage = _curStage;        
        data.gold = _player.Gold;

        data.lastStoryId = _lastStoryId;
        data.nextStoryId = _nextStoryId;

        data.role = new RoleAttr();
        data.role.healthy = _player.Healthy;
        data.role.energy = _player.Energy;
        data.role.hungry = _player.Hungry;
        data.role.hp = _player.Hp;
        data.role.atk = _player.Atk;
        data.role.def = _player.Def;

        data.itemList = _player.Items;

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
        _nextStoryId = data.nextStoryId;
        _player = new Role(data.role, data.itemList, data.gold);
        Debug.LogError("ReqHistoryData=> _curScene:" + _curScene + ", _destination:" + _destination + ", _distance:" + _distance + ", _curStage:" + _curStage +
            ", _lastStoryId:" + _lastStoryId + ", _nextStoryId:" + _nextStoryId);
        Debug.LogError("player attr: healthy:" + _player.Healthy + ", energy:" + _player.Energy + ", hungry:" + _player.Hungry + ", hp:" + _player.Hp + ", atk:" + _player.Atk + ", def:" + _player.Def);
        Debug.LogError("player item: gold:" + _player.Gold + ", itemlist:" + _player.Items.Count);

        ConfigMap sceneCfg = ConfigManager.Instance.ReqMapData(_curScene);
        _curSceneEvents = ConfigManager.Instance.ReqEventList(sceneCfg._eventPack);
    }

    public bool NeedShowDialog()
    {
        if (_nextStoryId != 0)
        {
            return true;
        }
        else
        {
            MyLog.Log("current scene switch dialog has played over.");
        }
        ConfigStory lastStory = ConfigManager.Instance.ReqStory(_lastStoryId);
        if (lastStory == null)
        {
            return false;
        }
        else
        {
            if (lastStory._type == 2)
            {
                _nextStoryId = _lastStoryId;
                return true;
            }
            else
            {
                int last_nextId = lastStory._nextId;
                if (last_nextId == 0)
                {
                    int toSceneId = lastStory._sceneId;
                    if (_curScene == toSceneId)
                    {
                        return false;
                    }
                    else
                    {
                        
                        return true;
                    }
                }
                else
                {
                    _nextStoryId = last_nextId;
                    return true;
                }
            }            
        }
    }

    public void MoveTowards()
    {
        Debug.Log("MoveTowards");
        //energy
        int energy = _player.Energy - 10;
        if (energy >= 0)
        {
            _player.Energy = energy;
            UIManager.Instance.mMainWindow.UpdateEnergy();

            //hungry
            int hungry = _player.Hungry - 20;
            if (hungry >= 0)
            {
                _player.Hungry = hungry;                
            }
            else
            {
                _player.Hungry = 0;
                _player.Healthy--;
                UIManager.Instance.mMainWindow.UpdateHealthy();
            }
            UIManager.Instance.mMainWindow.UpdateHungry();

            //scene
            if (_player.Healthy <= 0)
            {
                GameOver();
            }
            else
            {
                _distance--;
                if (_distance <= 0)
                {
                    Debug.Log("Switch to new scene.");
                    UpdateScene(_destination);
                }
                UIManager.Instance.mMainWindow.UpdateScene();
            }

            //event
            ConfigEventPackage curEvent = GetRandomEvent(_curSceneEvents);
            if (curEvent != null)
            {
                UIManager.Instance.UpdateEvent(curEvent);
            }
            Saved();
        }
        else
        {
            UIManager.Instance.mBottomWindow.Tips("精力不足");
        }
    }

    private void UpdateScene(int sceneId)
    {
        _curScene = sceneId;
        ConfigMap data = ConfigManager.Instance.ReqMapData(_curScene);
        _destination = data == null ? 0 : data._destination;
        _distance = data == null ? 0 : data._distance;
        _curSceneEvents = ConfigManager.Instance.ReqEventList(data._eventPack);
        _curStage = 0;
    }

    public ConfigEventPackage GetRandomEvent(List<ConfigEventPackage> packList)
    {
        //Debug.LogError("GetRandomEvent packList count:" + packList.Count);
        List<int> sampleList = new List<int>();
        for (int i = 0; i < packList.Count; i++)
        {
            int eventId = packList[i]._event;
            int weight = packList[i]._weight;
            for (int n = 0; n < weight * 10; n++)
            {
                sampleList.Add(eventId);
            }
        }
        int index = Random.Range(0, sampleList.Count);
        for (int i = 0; i < packList.Count; i++)
        {
            if (packList[i]._event == sampleList[index])
            {
                return packList[i];
            }
        }
        return null;
    }

    public void UpdateItems(Dictionary<ConfigItem, int> get, Dictionary<ConfigItem, int> loss)
    {
        foreach (ConfigItem item in get.Keys)
        {
            if (item._id == 1001)
            {
                Debug.Log("add gold:" + get[item]);
                _player.UpdateGold(_player.Gold + get[item]);
            }
            else
            {
                bool find = false;
                for (int i = 0; i < _player.Items.Count; i++)
                {
                    if (item._id == _player.Items[i].id)
                    {
                        find = true;
                        _player.Items[i].count += get[item];
                        break;
                    }
                }
                if (!find)
                {
                    ItemCountData data = new ItemCountData();
                    data.id = item._id;
                    data.count = get[item];
                    _player.Items.Add(data);
                }
            }
        }
        foreach (ConfigItem item in loss.Keys)
        {
            if (item._id == 1001)
            {
                Debug.Log("lost gold:" + loss[item]);
                _player.UpdateGold(_player.Gold - loss[item]);
            }
            else
            {
                for (int i = 0; i < _player.Items.Count; i++)
                {
                    if (item._id == _player.Items[i].id)
                    {
                        _player.Items[i].count -= loss[item];
                        if (_player.Items[i].count <= 0)
                        {
                            _player.Items.RemoveAt(i);
                        }
                        break;
                    }
                }
            }
        }
        Saved();
    }

    public void UpdateAttr()
    {
        UIManager.Instance.mMainWindow.UpdateBattleAttr();
        UIManager.Instance.mMainWindow.UpdateHealthy();
        if (_player.Healthy <= 0)
        {
            GameOver();
        }
    }

    public int GetItemCount(int itemId)
    {
        for (int i = 0; i < _player.Items.Count; i++)
        {
            if (itemId == _player.Items[i].id)
            {
                return _player.Items[i].count;
            }
        }
        return 0;
    }

    public void TurnToNextDialog(int nextStoryId)
    {
        _lastStoryId = _nextStoryId;
        _nextStoryId = nextStoryId;
        Saved();
    }

    public void GameOver()
    {
        Debug.LogError("Game Over!");
        UIManager.Instance.mDialogWindow.Hide();
        UIManager.Instance.mMainWindow.Hide();
        UIManager.Instance.mBottomWindow.Hide();
        UIManager.Instance.mEventWindow.Hide();
        UIManager.Instance.mBattleWindow.Hide();
        UIManager.Instance.mLoginWindow.Show();
    }



}