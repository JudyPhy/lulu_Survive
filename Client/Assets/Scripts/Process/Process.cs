﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

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

    private Vector2 _bornPos;

    public Vector2 CurPos { get { return _curPos; } }
    private Vector2 _curPos;

    public List<ConfigEvent> CurSceneEvents { get { return _curSceneEvents; } }
    private List<ConfigEvent> _curSceneEvents = new List<ConfigEvent>();

    public List<ConfigMonster> CurSceneMonsters { get { return _curSceneMonsters; } }
    private List<ConfigMonster> _curSceneMonsters = new List<ConfigMonster>();

    public List<ConfigDrop> CurSceneDrops { get { return _curSceneDrops; } }
    private List<ConfigDrop> _curSceneDrops = new List<ConfigDrop>();

    public int LastStoryID { get { return _lastStoryId; } }
    private int _lastStoryId;

    public int NextStoryID { get { return _nextStoryId; } }
    private int _nextStoryId;

    public Role Player { get { return _player; } }
    private Role _player;   

    public List<string> CurDialog = new List<string>();

    public void StartNewGame()
    {        
        MyLog.Log("StartNewGame=>");
        _player = new Role(69999);
        SwitchScene(1001);
        _lastStoryId = 0;
        _nextStoryId = 100101;
        Saved();
    }    

    public void StartHistoryGame()
    {
        SavedData data = GameSaved.GetData();
        _curScene = data.curScene;
        _curPos = new Vector2(data.curPos[0], data.curPos[1]);
        _lastStoryId = data.lastStoryId;
        _nextStoryId = data.nextStoryId;
        _player = new Role(data.role, data.itemList, data.gold);
        MyLog.LogError("ReqHistoryData=> _curScene:" + _curScene + ", _curPos:" + _curPos + ", _lastStoryId:" + _lastStoryId + ", _nextStoryId:" + _nextStoryId);
        MyLog.LogError("player attr: healthy:" + _player.Healthy + ", energy:" + _player.Energy + ", hungry:" + _player.Hungry + ", hp:" + _player.Hp + ", atk:" + _player.Atk + ", def:" + _player.Def);
        MyLog.LogError("player item: gold:" + _player.Gold + ", itemlist:" + _player.Items.Count);

        ConfigScene scene = ConfigManager.Instance.ReqSceneData(_curScene);
        if (scene != null)
        {
            _curSceneEvents = ConfigManager.Instance.ReqEventList(_curScene);
            _curSceneMonsters = ConfigManager.Instance.ReqMonsterList(_curScene);
            _curSceneDrops = ConfigManager.Instance.ReqDropList(_curScene);
        }
        else
        {
            MyLog.LogError("Scene[" + _curScene + "] not exist.");
            GameOver();
        }
    }

    public void Saved()
    {
        SavedData data = new SavedData();
        data.curScene = _curScene;
        data.curPos = new int[2];
        data.curPos[0] = (int)_curPos.x;
        data.curPos[1] = (int)_curPos.y;
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

    private void SwitchScene(int toSceneId)
    {
        _curScene = toSceneId;
        ConfigScene scene = ConfigManager.Instance.ReqSceneData(_curScene);
        if (scene != null)
        {
            _bornPos = scene._pos;
            _curPos = scene._pos;
            _curSceneEvents = ConfigManager.Instance.ReqEventList(_curScene);
            _curSceneMonsters = ConfigManager.Instance.ReqMonsterList(_curScene);
            _curSceneDrops = ConfigManager.Instance.ReqDropList(_curScene);
        }
        else
        {
            MyLog.LogError("Scene[" + _curScene + "] not exist.");
            GameOver();
        }
    }

    public bool NeedShowDialog()
    {
        if (_nextStoryId != 0)
        {
            return true;
        }
        else
        {
            MyLog.Log("Switched dialog of current scene  has played over.");
        }
        ConfigStory lastStory = ConfigManager.Instance.ReqStory(_lastStoryId);
        if (lastStory == null)
        {
            MyLog.LogError("lastStory[" + lastStory + "] is null.");
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
                    if (_curScene == lastStory._sceneId)
                    {
                        return false;
                    }
                    else
                    {
                        _nextStoryId = GetSwitchDialog(_curScene);
                        return _nextStoryId != 0;
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

    private int GetSwitchDialog(int newSceneId)
    {
        MyLog.Log("GetSwitchDialog: newSceneId=" + newSceneId);
        List<ConfigStory> list = ConfigManager.Instance.ReqSceneStory(newSceneId);
        list.Sort((data1, data2) => { return data1._id.CompareTo(data2._id); });
        if (list.Count > 0)
        {
            return list[0]._id;
        }
        return 0;
    }

    public bool InCurScene(ConfigScene curSceneData, Vector2 curPos)
    {
        Vector2 deltaX = curPos - _bornPos;
        if ((curPos.x <= _bornPos.x && Mathf.Abs(deltaX.x) <= curSceneData._range.x) && (curPos.x >= _bornPos.x && Mathf.Abs(deltaX.x) <= curSceneData._range.y)
            && (curPos.y >= _bornPos.y && Mathf.Abs(deltaX.y) <= curSceneData._range.z) && (curPos.y <= _bornPos.y && Mathf.Abs(deltaX.y) <= curSceneData._range.w))
        {
            return true;
        }
        return false;
    }

    public void MoveTowards()
    {
        MyLog.Log("MoveTowards");
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
                    MyLog.Log("Switch to new scene.");
                    SwitchScene(_destination);
                }
                UIManager.Instance.mMainWindow.UpdateSceneInfo();
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

    private EventType GetCurEventType()
    {
        return (EventType)Random.Range(1, 4);
    }

    public EventData GetRandomEvent()
    {
        EventType type = GetCurEventType();
        List<int> sampleList = new List<int>();
        switch (type)
        {
            case EventType.Event:
                for (int i = 0; i < _curSceneEvents.Count; i++)
                {
                    for (int n = 0; n < _curSceneEvents[i]._rate; n++)
                    {
                        sampleList.Add(_curSceneEvents[i]._id);
                    }
                }
                break;
            case EventType.Battle:
                for (int i = 0; i < _curSceneMonsters.Count; i++)
                {
                    for (int n = 0; n < _curSceneMonsters[i]._rate; n++)
                    {
                        sampleList.Add(_curSceneMonsters[i]._id);
                    }
                }
                break;
            case EventType.Drop:
                for (int i = 0; i < _curSceneDrops.Count; i++)
                {
                    for (int n = 0; n < _curSceneDrops[i]._rate; n++)
                    {
                        sampleList.Add(_curSceneDrops[i]._id);
                    }
                }
                break;
            default:
                return null;
        }
        int m = Random.Range(0, sampleList.Count);
        EventData result = new EventData(type, sampleList[m]);
        return result;
    }

    public void UpdateItems(Dictionary<ConfigItem, int> get, Dictionary<ConfigItem, int> loss)
    {
        foreach (ConfigItem item in get.Keys)
        {
            if (item._id == 1001)
            {
                MyLog.Log("add gold:" + get[item]);
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
                MyLog.Log("lost gold:" + loss[item]);
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
        MyLog.LogError("Game Over!");
        UIManager.Instance.mDialogWindow.Hide();
        UIManager.Instance.mMainWindow.Hide();
        UIManager.Instance.mBottomWindow.Hide();
        UIManager.Instance.mEventWindow.Hide();
        UIManager.Instance.mBattleWindow.Hide();
        UIManager.Instance.mLoginWindow.Show();
    }



}