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

    public int Hungry { set { _hungry = value; } get { return _hungry; } }
    private int _hungry;

    public int Hp { set { _hp = value; } get { return _hp; } }
    private int _hp;

    public int Def { get { return _def; } }
    private int _def;

    public int Atk { get { return _atk; } }
    private int _atk;

    public int Status { get { return _status; } }
    private int _status;    

    public int Gold { set { _gold = value; } get { return _gold; } }
    private int _gold;

    public List<ItemCountData> Items { get { return _items; } }
    private List<ItemCountData> _items;

    public Role(int id)
    {
        _id = id;

        ConfigMonster player = ConfigManager.Instance.ReqMonster(_id);
        _healthy = 30;
        _energy = 100;
        _hungry = 100;
        _hp = player._hp;
        _def = player._def;
        _atk = player._atk;
        _status = 0;

        _gold = 0;
        _items = new List<ItemCountData>();
    }

    public Role(RoleAttr roleAttr, List<ItemCountData> items,int gold)
    {
        _id = 69999;

        _healthy = roleAttr.healthy;
        _energy = roleAttr.energy;
        _hungry = roleAttr.hungry;
        _hp = roleAttr.hp;
        _def = roleAttr.def;
        _atk = roleAttr.atk;
        _status = 0;

        _gold = gold;
        _items = items;
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

}