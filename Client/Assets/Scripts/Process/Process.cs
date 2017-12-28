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

    public int TowardsStep { get { return _towardsStep; } }
    private int _towardsStep;

    //public List<ConfigEvent> CurSceneEvents { get { return _curSceneEvents; } }
    private List<ConfigEvent> _curSceneEvents = new List<ConfigEvent>();

    //public List<ConfigMonster> CurSceneMonsters { get { return _curSceneMonsters; } }
    private List<ConfigMonster> _curSceneMonsters = new List<ConfigMonster>();

    //public List<ConfigDrop> CurSceneDrops { get { return _curSceneDrops; } }
    private List<ConfigDrop> _curSceneDrops = new List<ConfigDrop>();

    //public int LastStoryID { get { return _lastStoryId; } }
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
        SwitchScene(GameConfig.SCENE_START_ID);
        _player = new Player();
        _player.Create();
        _lastStoryId = 0;
        //_nextStoryId = GameConfig.DIALOG_START_ID;
        _curDay = 1;
        Saved();
    }

    public void StartHistoryGame()
    {
        SavedData data = GameSaved.GetData();

        _curScene = data.curScene;

        _player = new Player();
        _player.CreateHistory(data);

        _lastStoryId = data.lastStoryId;
        //_nextStoryId = data.nextStoryId;

        _curDay = data.day;
        MyLog.LogError("ReqHistoryData=> _curDay:" + _curDay + ", _curScene:" + _curScene + ", _lastStoryId:" + _lastStoryId + ", _nextStoryId:" + _nextStoryId);
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

        data.lastStoryId = _lastStoryId;
        //data.nextStoryId = _nextStoryId;

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

    public void SwitchScene(int toSceneId)
    {
        _curScene = toSceneId;
        ConfigScene scene = ConfigManager.Instance.ReqSceneData(_curScene);
        if (scene != null)
        {
            _curSceneEvents = ConfigManager.Instance.ReqEventList(_curScene);
            _curSceneMonsters = ConfigManager.Instance.ReqMonsterList(_curScene);
            _curSceneDrops = ConfigManager.Instance.ReqDropList(_curScene);
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
        if (_lastStoryId == 0)
        {
            _nextStoryId = GameConfig.DIALOG_START_ID;
            return true;
        }
        else
        {
            ConfigStory lastStory = ConfigManager.Instance.ReqStory(_lastStoryId);
            if (lastStory._type == (int)DialogType.ChooseDialog)
            {
                MyLog.Log("Continue show last choose dialog[" + lastStory._nextId + "]");
                _nextStoryId = lastStory._nextId;
                return true;
            }
            else if (lastStory._type == (int)DialogType.ToNextDialog)
            {
                MyLog.Log("To next dialog[" + lastStory._nextId + "]");
                _nextStoryId = lastStory._nextId;
                return true;
            }
            else
            {
                List<ConfigStory> curSceneDialogs = ConfigManager.Instance.ReqDialogList(_curScene);
                curSceneDialogs.Sort((data1, data2) => { return data1._id.CompareTo(data2._id); });
                int index = -1;
                for (int i = 0; i < curSceneDialogs.Count; i++)
                {
                    if (curSceneDialogs[i]._id == lastStory._id)
                    {
                        index = i + 1;
                        break;
                    }
                }
                if (index == -1)
                {
                    MyLog.Log("Last dialog scene[" + _lastStoryId + "] is not current scene[" + _curScene + "]. Current scene has dialog count " + curSceneDialogs.Count);
                    if (curSceneDialogs.Count > 0)
                    {
                        _nextStoryId = curSceneDialogs[0]._id;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    MyLog.Log("Current scene has dialog count " + curSceneDialogs.Count);
                    if (index >= curSceneDialogs.Count)
                        return false;
                    else
                    {
                        _nextStoryId = curSceneDialogs[index]._id;
                        MyLog.Log("Will play dialog[ " + _nextStoryId + "]");
                        return true;
                    }
                }
            }
        }
    }

    public void Explore()
    {
        MyLog.Log("Explore");
        Move(false);
    }

    public void MoveTowards()
    {
        MyLog.Log("MoveTowards");
        Move(true);
    }

    private void Move(bool isTowards)
    {
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
                if (isTowards)
                {
                    _towardsStep++;
                }
                //event
                _curEventData = GetRandomEvent();
                UIManager.Instance.mMainWindow.UpdateUI();
                Saved();
            }
        }
        else
        {
            UIManager.Instance.mMainWindow.Tips("精力不足");
        }
        MyLog.Log("Move over, event type=" + _curEventData._type);
    }

    private EventType GetCurEventType()
    {
        List<EventType> typeList = new List<EventType>();
        if (_curSceneEvents.Count > 0)
        {
            typeList.Add(EventType.Event);
        }
        if (_curSceneMonsters.Count > 0)
        {
            typeList.Add(EventType.Battle);
        }
        if (_curSceneDrops.Count > 0)
        {
            typeList.Add(EventType.Drop);
        }
        int index = Random.Range(0, typeList.Count);
        return typeList[index];
    }

    public EventData GetRandomEvent()
    {
        EventType type = GetCurEventType();
        MyLog.Log("GetRandomEvent type=" + type);
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
                MyLog.Log("type is null.");
                return null;
        }
        //MyLog.Log("sampleList count=" + sampleList.Count + ", cur event type=" + type.ToString());
        if (sampleList.Count > 0)
        {            
            int m = Random.Range(0, sampleList.Count);
            EventData result = new EventData(type, sampleList[m]);
            return result;
        }
        else
            return null;
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

    public void TurnToNextDialog(int nextId)
    {
        _lastStoryId = _nextStoryId;
        _nextStoryId = nextId;
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

    public bool CanUseItem(int itemId)
    {
        ItemCountData data = GetHasItem(itemId);
        if (data.count <= 0)
            return false;
        ConfigItem configData = ConfigManager.Instance.ReqItem(itemId);
        if (configData == null)
            return false;
        if (configData._healthy > 0 && _player.Healthy == GameConfig.PLAYER_BASE_HEALTHY)
            return false;
        if ((configData._energy > 0 && _player.Energy == _player.EnergyMax) || (configData._energy < 0 && _player.Energy == 0))
            return false;
        if ((configData._hungry > 0 && _player.Hungry == _player.HungryMax) || (configData._hungry < 0 && _player.Hungry == 0))
            return false;
        if ((configData._hp > 0 && _player.Hp == _player.HpMax) || (configData._hp < 0 && _player.Hp == 0))
            return false;
        if (configData._power < 0 && _player.Power == 0)
            return false;
        if (configData._agile < 0 && _player.Agile == 0)
            return false;
        if (configData._physic < 0 && _player.Physic == 0)
            return false;
        if (configData._charm < 0 && _player.Charm == 0)
            return false;
        if (configData._perception < 0 && _player.Perception == 0)
            return false;
        return true;
    }

    public void GameOver()
    {
        MyLog.LogError("Game Over!");
        UIManager.Instance.SwitchToUI(UIType.Login);
    }



}