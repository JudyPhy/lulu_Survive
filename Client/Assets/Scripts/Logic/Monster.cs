using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;

public class Monster : Role
{
    public int Drop { get { return _dropId; } }
    private int _dropId;

    public string Desc { get { return _desc; } }
    private string _desc;

    public Monster(int monsterId)
    {
        ConfigMonster monster = ConfigManager.Instance.ReqMonster(monsterId);
        if (monster != null)
        {
            _name = monster._name;
            _desc = monster._desc;
            _hp = monster._hp;
            _def = monster._def;
            _atk = monster._atk;
            _power = monster._power;
            _agile = monster._agile;
            _physic = monster._physic;
            _charm = monster._charm;
            _perception = monster._perception;
            _buffID = 0;
            _buffDuration = 0;
            _dropId = monster._drop;
        }
        else
        {
            MyLog.LogError("Monster " + monsterId + " not exist.");
        }
    }

    public void BeHurt(int atk)
    {
        _hp -= Mathf.Max(atk - _def, 0);
        if (_hp <= 0)
        {
            BattleManager.Instance.BattleOver(true);           
        }
        else
        {
            string curDesc = _name + "失去" + atk + "点生命";
            EventContext param = new EventContext();
            param.data = curDesc;
            UIManager.mEventDispatch.DispatchEvent(EventDefine.UPDATE_MONSTER_UI, param);
            Timers.inst.Add(1.5f, 1, PlayAtk);
        }
    }

    public void PlayAtk(object param)
    {
        Process.Instance.Player.BeHurt(_atk);
        UIManager.mEventDispatch.DispatchEvent(EventDefine.PLAY_ATK_ANI);
        Process.Instance.Player.InBattle = false;
    }
}