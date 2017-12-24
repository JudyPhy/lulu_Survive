using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Role
{
    public int ID { get { return _id; } }
    private int _id;

    public int Healthy { set { _healthy = value; } get { return _healthy; } }
    private int _healthy;

    public int Energy { set { _energy = value; } get { return _energy; } }
    private int _energy;

    public int EnergyMax { set { _energyMax = value; } get { return _energyMax; } }
    private int _energyMax;

    public int Hungry { set { _hungry = value; } get { return _hungry; } }
    private int _hungry;

    public int HungryMax { set { _hungryMax = value; } get { return _hungryMax; } }
    private int _hungryMax;

    public int Hp { set { _hp = value; } get { return _hp; } }
    private int _hp;

    public int Def { get { return _def; } }
    private int _def;

    public int Atk { get { return _atk; } }
    private int _atk;

    public int Power { set { _power = value; } get { return _power; } }
    private int _power;

    public int Agile { set { _agile = value; } get { return _agile; } }
    private int _agile;

    public int Physic { set { _physic = value; } get { return _physic; } }
    private int _physic;

    public int Charm { set { _charm = value; } get { return _charm; } }
    private int _charm;

    public int Perception { set { _perception = value; } get { return _perception; } }
    private int _perception;

    public int Status { get { return _status; } }
    private int _status;

    public int Gold { set { _gold = value; } get { return _gold; } }
    private int _gold;

    public List<ItemCountData> Items { get { return _items; } }
    private List<ItemCountData> _items;

    public int BuffDuration { set { _buffDuration = value; } get { return _buffDuration; } }
    private int _buffDuration;

    public int BuffID { set { _buffID = value; } get { return _buffID; } }
    private int _buffID;

    public Role(int id)
    {
        _id = id;

        ConfigMonster player = ConfigManager.Instance.ReqMonster(_id);
        _healthy = 30;
        _energy = 100;
        _energyMax = 100;
        _hungry = 100;
        _hungryMax = 100;
        _hp = player._hp;
        _def = player._def;
        _atk = player._atk;
        _status = 0;

        _gold = 0;
        _items = new List<ItemCountData>();
    }

    public Role(RoleAttr roleAttr, List<ItemCountData> items, int gold, int buffId, int buffDuration)
    {
        _id = 69999;

        _healthy = roleAttr.healthy;
        _energy = roleAttr.energy;
        _energyMax = roleAttr.energyMax;
        _hungry = roleAttr.hungry;
        _hungryMax = roleAttr.hungryMax;
        _hp = roleAttr.hp;
        _def = roleAttr.def;
        _atk = roleAttr.atk;
        _status = 0;

        _gold = gold;
        _items = items;

        _buffID = buffId;
        _buffDuration = buffDuration;
    }

    public void BeAtc(int atk)
    {
        _hp -= atk;
        _hp = _hp < 0 ? 0 : _hp;
        if (_hp == 0)
        {
            _healthy--;
        }
        Process.Instance.UpdateAttr();
    }

    public void GoToNextStatus()
    {
        _status++;
        _status = _status >= 3 ? 0 : _status;
    }

    public void UpdateGold(int gold)
    {
        _gold = gold <= 0 ? 0 : gold;
        UIManager.Instance.mMainWindow.UpdateGold();
    }

    public void UpdateItem(int itemId, int count)
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
        UIManager.Instance.mMainWindow.UpdateMedicine();
    }

    public void UpdateEnergy(int newValue)
    {
        _energy = Mathf.Min(newValue, _energyMax);
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

}