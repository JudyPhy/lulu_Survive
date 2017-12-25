using UnityEngine;
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

    public Vector2 CurOutPos { get { return _curOutPos; } }
    private Vector2 _curOutPos;
    private int _curOutId;

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

    public Player Player { get { return _player; } }
    private Player _player;

    public EventData CurEventData { set { _curEventData = value; } get { return _curEventData; } }
    private EventData _curEventData;

    public int CurDay { get { return _curDay; } }
    private int _curDay;

    public void StartNewGame()
    {        
        SwitchScene(1001);
        _player = new Player();
        _player.Create();
        _lastStoryId = 0;
        _nextStoryId = GameConfig.DIALOG_START_ID;
        _curDay = 1;
        Saved();
    }

    public void StartHistoryGame()
    {
        SavedData data = GameSaved.GetData();

        _curScene = data.curScene;
        _curPos = new Vector2(data.curPos[0], data.curPos[1]);
        _curOutId = data.curOutId;

        _player = new Player();
        _player.CreateHistory(data);

        _lastStoryId = data.lastStoryId;
        _nextStoryId = data.nextStoryId;

        _curDay = data.day;
        MyLog.LogError("ReqHistoryData=> _curDay:" + _curDay + ", _curScene:" + _curScene + ", _curPos:" + _curPos
            + ", _curOutId:" + _curOutId + ", _lastStoryId:" + _lastStoryId + ", _nextStoryId:" + _nextStoryId);
        MyLog.LogError("player attr: healthy:" + _player.Healthy + ", energy:" + _player.Energy + ", energyMax:"
            + _player.EnergyMax + ", hungry:" + _player.Hungry + ", hungryMax:" + _player.HungryMax
            + ", Hp:" + _player.Hp + ", Atk:" + _player.Atk + ", Def:" + _player.Def
            + ", Power:" + _player.Power + ", Agile:" + _player.Agile + ", Physic:" + _player.Physic
            + ", Charm:" + _player.Charm + ", Perception:" + _player.Perception);
        MyLog.LogError("player item: gold:" + _player.Gold + ", itemCount:" + _player.Items.Count);
        MyLog.LogError("player buff: id:" + _player.BuffID + ", duration:" + _player.BuffDuration);

        ConfigScene scene = ConfigManager.Instance.ReqSceneData(_curScene);
        if (scene != null)
        {
            _bornPos = scene._pos;
            _curOutPos = scene != null ? scene._outList[_curOutId] : new Vector2(0, 0);
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
        data.day = _curDay;

        data.curScene = _curScene;
        data.curPos = new int[2];
        data.curPos[0] = (int)_curPos.x;
        data.curPos[1] = (int)_curPos.y;
        data.curOutId = _curOutId;       

        data.lastStoryId = _lastStoryId;
        data.nextStoryId = _nextStoryId;

        data.role = new RoleAttr();
        data.role.healthy = _player.Healthy;
        data.role.energy = _player.Energy;
        data.role.energyMax = _player.EnergyMax;
        data.role.hungry = _player.Hungry;
        data.role.hungryMax = _player.HungryMax;

        data.role.hp = _player.Hp;
        data.role.atk = _player.Atk;
        data.role.def = _player.Def;
        data.role.power = _player.Power;
        data.role.agile = _player.Agile;
        data.role.physic = _player.Physic;
        data.role.charm = _player.Charm;
        data.role.perception = _player.Perception;

        data.role.buffId = _player.BuffID;
        data.role.buffDuration = _player.BuffDuration;

        data.gold = _player.Gold;

        data.itemList = _player.Items;        

        GameSaved.SaveData(data);
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
            //out
            if (scene._outList.Count > 0)
            {
                foreach (int id in scene._outList.Keys)
                {
                    _curOutId = id;
                    _curOutPos = scene._outList[id];
                }
            }
            else
            {
                _curOutId = 0;
                _curOutPos = Vector2.zero;
            }
            //event
            _curEventData = null;
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
            switch (lastStory._type)
            {
                case 1:
                    //跳转下一个剧情
                    _nextStoryId = lastStory._nextId; ;
                    return true;
                case 2:
                    //未选择就退出，重新选择
                    _nextStoryId = _lastStoryId;
                    return true;
                case 3:
                    //跳转场景
                    if (lastStory._nextId != _curScene)
                    {
                        SwitchScene(lastStory._nextId);
                    }
                    _nextStoryId = 0;
                    return false;
                default:
                    return false;
            }
        }
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

    public void Explore()
    {
        MyLog.Log("Explore");
        //energy
        int energy = _player.Energy - GameConfig.COST_ENERGY_ONCE;
        if (energy >= 0)
        {
            _player.Energy = energy;

            //hungry
            int hungry = _player.Hungry - GameConfig.COST_HUNGRY_ONCE;
            if (_player.Hungry > 0 && hungry < 0)
            {
                _player.Healthy--;
            }
            _player.Hungry = hungry;

            //random event
            if (_player.Healthy <= 0)
            {
                GameOver();
            }
            else
            {
                _curEventData = GetRandomEvent();
                UIManager.Instance.mMainWindow.UpdateUI();
                Saved();
            }
        }
        else
        {
            UIManager.Instance.mMainWindow.Tips("精力不足");
        }
    }

    public void MoveTowards()
    {
        MyLog.Log("MoveTowards");
        //energy
        int energy = _player.Energy - GameConfig.COST_ENERGY_ONCE;
        if (energy >= 0)
        {
            _player.Energy = energy;

            //hungry
            int hungry = _player.Hungry - GameConfig.COST_HUNGRY_ONCE;
            if (_player.Hungry > 0 && hungry < 0)
            {
                _player.Healthy--;
            }
            _player.Hungry = hungry;

            //scene
            if (_player.Healthy <= 0)
            {
                GameOver();
            }
            else
            {
                _curPos = GetNewPos();
                MyLog.Log("Move to pos[" + _curPos + "]");
                if (_curPos == _curOutPos)
                {
                    SwitchScene(_curOutId);
                }
                else
                {
                    //event
                    _curEventData = GetRandomEvent();                    
                }
                UIManager.Instance.mMainWindow.UpdateUI();
                Saved();
            }
        }
        else
        {
            UIManager.Instance.mMainWindow.Tips("精力不足");
        }
    }

    private Vector2 GetNewPos()
    {
        Vector2 pos = _curPos;
        ConfigScene curSceneData = ConfigManager.Instance.ReqSceneData(_curScene);
        if (curSceneData != null)
        {
            Vector2 delta = _curOutPos - _curPos;
            if (delta.x != 0)
            {
                pos.x = delta.x > 0 ? _curPos.x++ : _curPos.x--;
            }
            else
            {
                if (delta.y != 0)
                {
                    pos.y = delta.y > 0 ? _curPos.y++ : _curPos.y--;
                }
                else
                {
                    MyLog.Log("Current pos == out pos");
                }
            }
        }
        return pos;
    }

    private EventType GetCurEventType()
    {
        return EventType.Battle;
        //return (EventType)Random.Range(1, 4);
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

    public void UpdateAttr()
    {
        UIManager.Instance.mMainWindow.UpdateHealthy();
        UIManager.Instance.mMainWindow.UpdateEnergy();
        UIManager.Instance.mMainWindow.UpdateHungry();
        UIManager.Instance.mMainWindow.UpdateBattleAttr();
        UIManager.Instance.mBagWindow.UpdateTopAttr();
        if (_player.Healthy <= 0)
        {
            GameOver();
        }
    }

    public void TurnToNextDialog(int nextStoryId)
    {
        _lastStoryId = _nextStoryId;
        _nextStoryId = nextStoryId;
        Saved();
    }

    public ItemCountData GetHasItem(int itemId)
    {
        ItemCountData data = new ItemCountData();
        data.id = itemId;
        data.count = 0;
        for (int i = 0; i < _player.Items.Count; i++)
        {
            if (_player.Items[i].id == itemId)
            {
                data.count = _player.Items[i].count;
            }
        }
        return data;
    }

    public List<ConfigItem> GetItemList(ItemType type)
    {
        List<ConfigItem> list = ConfigManager.Instance.ReqItemList();
        List<ConfigItem> result = new List<ConfigItem>();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i]._type == (int)type)
            {
                result.Add(list[i]);
            }
        }
        return result;
    }

    public void GameOver()
    {
        MyLog.LogError("Game Over!");
        UIManager.Instance.SwitchToUI(UIType.Login);
    }



}