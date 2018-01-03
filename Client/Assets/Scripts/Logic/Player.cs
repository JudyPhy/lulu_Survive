using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Role
{
    public int Healthy { set { _healthy = value; } get { return _healthy; } }
    private int _healthy;

    public int Energy { set { _energy = value; } get { return _energy; } }
    private int _energy;

    public int EnergyMax
    {
        get
        {
            return GameConfig.PLAYER_BASE_ENERGY_MAX + GetEquipAddAttrValue(BattleAttr.HpRecover);
        }
    }

    public int Hungry { set { _hungry = value; } get { return _hungry; } }
    protected int _hungry;

    public int HungryMax
    {
        get
        {
            return GameConfig.PLAYER_BASE_HUNGRY_MAX;
        }
    }

    public int HpMax
    {
        get
        {
            return _baseHp + GetEquipAddAttrValue(BattleAttr.Hp);
        }
    }
    private int _baseHp;

    public PlayerBattleStatus Status { get { return _status; } }
    private PlayerBattleStatus _status;

    public int Gold { set { _gold = value; } get { return _gold; } }
    private int _gold;

    public List<ItemData> Items { get { return _items; } }
    private List<ItemData> _items;

    public List<EquipmentData> EquipmentList { get { return _equipmentList; } }
    private List<EquipmentData> _equipmentList;

    public bool InBattle = false;

    public void Create()
    {
        ConfigMonster player = ConfigManager.Instance.ReqMonster(GameConfig.PLAYER_CONFIG_ID);
        if (player != null)
        {
            _healthy = GameConfig.PLAYER_BASE_HEALTHY;
            _energy = GameConfig.PLAYER_BASE_ENERGY;
            _hungry = GameConfig.PLAYER_BASE_HUNGRY;

            _hp = player._hp;
            _baseHp = player._hp;
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
            _items = new List<ItemData>();
            EquipmentData equip = new EquipmentData(1);
            _equipmentList = new List<EquipmentData>();
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
        _hungry = savedData.role.hungry;

        _hp = savedData.role.hp;
        ConfigMonster player = ConfigManager.Instance.ReqMonster(GameConfig.PLAYER_CONFIG_ID);
        _baseHp = player != null ? player._hp : 0;
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

        _items = new List<ItemData>();
        for (int i = 0; i < savedData.itemList.Count; i++)
        {
            ItemData data = new ItemData(savedData.itemList[i].id);
            data.Count = savedData.itemList[i].num;
            _items.Add(data);
        }

        _equipmentList = new List<EquipmentData>();
        for (int i = 0; i < savedData.equipmentList.Count; i++)
        {
            EquipmentData data = new EquipmentData(savedData.equipmentList[i].id);
            data.Lev = savedData.equipmentList[i].num;
            _equipmentList.Add(data);
        }
    }

    public int GetEquipAddAttrValue(BattleAttr attr)
    {
        int addValue = 0;
        for (int i = 0; i < _equipmentList.Count; i++)
        {
            if (_equipmentList[i].ConfigData._attr._type == (int)attr)
            {
                addValue += _equipmentList[i].ConfigData._attr._baseValue + _equipmentList[i].ConfigData._attr._increaseValue * _equipmentList[i].Lev;
            }
        }
        return addValue;
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
            if (_items[i].ID == itemId)
            {
                _items[i].Count = _items[i].Count + count;
                if (_items[i].Count <= 0)
                {
                    _items.RemoveAt(i);
                }
                isFind = true;
                Process.Instance.Saved();
                MyLog.Log("item[" + itemId + "] new count " + _items[i].Count);
                break;
            }
        }
        if (!isFind && count > 0)
        {
            ItemData data = new ItemData(itemId);
            data.Count = count;
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
        _energy = Mathf.Min(newValue, EnergyMax);
    }

    public void AddEnergy(int addValue)
    {
        _energy += addValue;
        _energy = Mathf.Max(_energy, 0);
        _energy = Mathf.Min(_energy, EnergyMax);
    }

    public void AddHungry(int addValue)
    {
        _hungry += addValue;
        _hungry = Mathf.Max(_hungry, 0);
        _hungry = Mathf.Min(_hungry, HungryMax);
    }

    public void AddHp(int addValue)
    {
        _hp += addValue;
        _hp = Mathf.Max(_hp, 0);
        _hp = Mathf.Min(_hp, HpMax);
    }

    public void PlayAtk()
    {
        InBattle = true;
        BattleManager.Instance.Monster.BeHurt(_atk + GetEquipAddAttrValue(BattleAttr.Atk));
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

    public ItemData ReqItem(int id)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].ID == id)
            {
                return _items[i];
            }
        }
        return new ItemData(id);
    }

    public EquipmentData ReqEquipment(int id)
    {
        for (int i = 0; i < _equipmentList.Count; i++)
        {
            if (_equipmentList[i].ID == id)
            {
                return _equipmentList[i];
            }
        }
        return new EquipmentData(id);
    }

}

public enum PlayerBattleStatus
{
    Idle,
    Balance = 1,
    Risk = 2,
    Filthy = 3,
}