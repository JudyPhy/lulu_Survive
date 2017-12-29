using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Role
{
    public int ID { get { return _id; } }
    protected int _id;

    public string Name { set { _name = value; } get { return _name; } }
    protected string _name;

    public int Hp { set { _hp = value; } get { return _hp; } }
    protected int _hp;    

    public int Def { set { _def = value; } get { return _def; } }
    protected int _def;

    public int Atk { set { _atk = value; } get { return _atk; } }
    protected int _atk;

    public int Power { set { _power = value; } get { return _power; } }
    protected int _power;

    public int Agile { set { _agile = value; } get { return _agile; } }
    protected int _agile;

    public int Physic { set { _physic = value; } get { return _physic; } }
    protected int _physic;

    public int Charm { set { _charm = value; } get { return _charm; } }
    protected int _charm;

    public int Perception { set { _perception = value; } get { return _perception; } }
    protected int _perception;

    public int BuffDuration { set { _buffDuration = value; } get { return _buffDuration; } }
    protected int _buffDuration;

    public int BuffID { set { _buffID = value; } get { return _buffID; } }
    protected int _buffID;

    public int GetAtk()
    {
        return _atk;
    }

}

public enum RoleType
{
    Idle,
    Player,
    Monster,
}