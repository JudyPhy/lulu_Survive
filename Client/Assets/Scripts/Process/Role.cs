using UnityEngine;
using System.Collections;

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
    }

    public Role(int healthy, int energy, int hungry, int hp, int atk, int def)
    {
        _id = 999;
        _healthy = healthy;
        _energy = energy;
        _hungry = hungry;
        _hp = hp;
        _def = atk;
        _atk = def;
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

}