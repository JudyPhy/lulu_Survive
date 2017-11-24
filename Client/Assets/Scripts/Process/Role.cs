using UnityEngine;
using System.Collections;

public class Role
{
    private int _hp;
    public int Hp { get { return _hp; } }

    public Role(int hp)
    {
        _hp = hp;

    }

}