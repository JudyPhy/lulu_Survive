using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Role
{
    public int Healthy { set { _healthy = value; } get { return _healthy; } }
    private int _healthy;

    public int Energy { set { _energy = value; } get { return _energy; } }
    private int _energy;

    public int EnergyMax { set { _energyMax = value; } get { return _energyMax; } }
    private int _energyMax;

    public int Hungry { set { _hungry = value; } get { return _hungry; } }
    protected int _hungry;

    public int HungryMax { set { _hungryMax = value; } get { return _hungryMax; } }
    protected int _hungryMax;

    public int HpMax { set { _hpMax = value; } get { return _hpMax; } }
    protected int _hpMax;

    public PlayerBattleStatus Status { get { return _status; } }
    private PlayerBattleStatus _status;

    public int Gold { set { _gold = value; } get { return _gold; } }
    private int _gold;

    public List<ItemCountData> Items { get { return _items; } }
    private List<ItemCountData> _items;

    public List<EquipmentData> EquipmentList { get { return _equipmentList; } }
    private List<EquipmentData> _equipmentList;

    public void Create()
    {
        ConfigMonster player = ConfigManager.Instance.ReqMonster(GameConfig.PLAYER_CONFIG_ID);
        if (player != null)
        {
            _healthy = GameConfig.PLAYER_BASE_HEALTHY;
            _energy = GameConfig.PLAYER_BASE_ENERGY;
            _energyMax = GameConfig.PLAYER_BASE_ENERGY_MAX;
            _hungry = GameConfig.PLAYER_BASE_HUNGRY;
            _hungryMax = GameConfig.PLAYER_BASE_HUNGRY_MAX;

            _hp = player._hp;
            _hpMax = player._hp;
            _def = player._def;
            _atk = player._atk;
            _power = player._power;
            _agile = player._agile;
            _physic = player._physic;
            _charm = player._charm;
            _perception = player._perception;

            _buffID = 0;
            _buffDuration = 0;

            _status = PlayerBattleStatus.Balance;

            _gold = 0;
            _items = new List<ItemCountData>();
        }
        else
        {
            MyLog.LogError("Player config data is null.");
        }        
    }

    public void CreateHistory(SavedData savedData)
    {
        _healthy = savedData.role.healthy;
        _energy = savedData.role.energy;
        _energyMax = savedData.role.energyMax;
        _hungry = savedData.role.hungry;
        _hungryMax = savedData.role.hungryMax;

        _hp = savedData.role.hp;
        _hpMax = savedData.role.hpMax;
        _def = savedData.role.def;
        _atk = savedData.role.atk;
        _power = savedData.role.power;
        _agile = savedData.role.agile;
        _physic = savedData.role.physic;
        _charm = savedData.role.charm;
        _perception = savedData.role.perception;
        _buffID = savedData.role.buffId;
        _buffDuration = savedData.role.buffDuration;

        _status = PlayerBattleStatus.Balance;

        _gold = savedData.gold;
        _items = savedData.itemList;       
    }

    public void GoToNextStatus()
    {
        int curIndex = (int)_status;
        curIndex++;
        _status = curIndex > 3 ? PlayerBattleStatus.Balance : (PlayerBattleStatus)curIndex;
    }

    public void UpdateGold(int gold)
    {
        _gold = gold <= 0 ? 0 : gold;
    }

    public void AddItem(int itemId, int count)
    {
        MyLog.Log("update item[" + itemId + "] count " + count);
        bool isFind = false;
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].id == itemId)
            {
                _items[i].count = _items[i].count + count;
                if (_items[i].count <= 0)
                {
                    _items.RemoveAt(i);
                }
                isFind = true;
                Process.Instance.Saved();
                MyLog.Log("item[" + itemId + "] new count " + _items[i].count);
                break;
            }
        }
        if (!isFind && count > 0)
        {
            ItemCountData data = new ItemCountData();
            data.id = itemId;
            data.count = count;
            _items.Add(data);
            Process.Instance.Saved();
        }        
    }

    public void AddHealthy(int addValue)
    {
        _healthy += addValue;
        if (_healthy <= 0)
        {
            Process.Instance.GameOver();
        }
        else if (_healthy > GameConfig.PLAYER_BASE_HEALTHY)
        {
            _healthy = GameConfig.PLAYER_BASE_HEALTHY;
        }
    }

    public void UpdateEnergy(int newValue)
    {
        _energy = Mathf.Min(newValue, _energyMax);
    }

    public void AddEnergy(int addValue)
    {
        _energy += addValue;
        _energy = Mathf.Max(_energy, 0);
        _energy = Mathf.Min(_energy, _energyMax);
    }

    public void AddHungry(int addValue)
    {
        _hungry += addValue;
        _hungry = Mathf.Max(_hungry, 0);
        _hungry = Mathf.Min(_hungry, _hungryMax);
    }

    public void AddHp(int addValue)
    {
        _hp += addValue;
        _hp = Mathf.Max(_hp, 0);
        _hp = Mathf.Min(_hp, _hpMax);
    }

    public void UpdateAttrDaily()
    {
        MyLog.Log("update role attr everyday.");
        if (_buffDuration > 0)
        {
            _buffDuration--;
            if (_buffDuration <= 0)
            {
                MyLog.Log("Need reset attr to orig.");
                ConfigItem item = ConfigManager.Instance.ReqItem(_buffID);
                if (item != null)
                {
                    if (item._healthy != 0)
                    {
                        _healthy -= item._healthy;
                    }
                    if (item._energy != 0)
                    {
                        _energy -= item._energy;
                    }
                    if (item._hungry != 0)
                    {
                        _hungry -= item._hungry;
                    }
                    if (item._hp != 0)
                    {
                        _hp -= item._hp;
                    }
                    if (item._power != 0)
                    {
                        _power -= item._power;
                    }
                    if (item._agile != 0)
                    {
                        _agile -= item._agile;
                    }
                    if (item._physic != 0)
                    {
                        _physic -= item._physic;
                    }
                    if (item._charm != 0)
                    {
                        _charm -= item._charm;
                    }
                    if (item._perception != 0)
                    {
                        _perception -= item._perception;
                    }
                }
            }
            Process.Instance.UpdateAttr();
            Process.Instance.Saved();
        }
    }

    public void PlayAtk()
    {
        BattleManager.Instance.Monster.BeHurt(_atk);
        UIManager.Instance.mMainWindow.PlayBattleAtkAni();
    }

    public void BeHurt(int atk)
    {
        _hp -= Mathf.Max(atk - _def, 0);
        if (_hp <= 0)
        { 
            _healthy--;
            if (_healthy <= 0)
            {
                Process.Instance.GameOver();
            }
            else
            {
                BattleManager.Instance.BattleOver(false);
            }
        }
        else
        {
            UIManager.Instance.mMainWindow.UpdateBattleAttr();
            string curDesc = "你失去" + atk + "点生命";
            UIManager.Instance.mMainWindow.BattleUpdate(curDesc);
        }
    }

}

public enum PlayerBattleStatus
{
    Idle,
    Balance = 1,
    Risk = 2,
    Filthy = 3,
}