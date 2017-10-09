using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTransmit;

public class Player : Role
{

    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Player();
            }
            return instance;
        }
    }

    public bool HasBorn = false;

    public void SetBornTrs(pb.RoleTrs trs)
    {
        _pos.x = trs.pos_x / 100.0f;
        _pos.y = trs.pos_y / 100.0f;
        _rot = trs.rot / 100.0f;
        HasBorn = true;
    }


}
