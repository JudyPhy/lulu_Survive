using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;

public class BattleManager
{
    private static BattleManager _instance;
    public static BattleManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new BattleManager();
            return _instance;
        }
    }

    public Role Monster { get { return _monster; } }
    private Role _monster;
    private int _dropId;

    public void CreateMonster(int monsterId)
    {
        ConfigMonster monster = ConfigManager.Instance.ReqMonster(monsterId);
        if (monster != null)
        {
            _monster = new Role();
            _monster.Hp = monster._hp;
            _monster.Def = monster._def;
            _monster.Atk = monster._atk;
            _monster.Power = monster._power;
            _monster.Agile = monster._agile;
            _monster.Physic = monster._physic;
            _monster.Charm = monster._charm;
            _monster.Perception = monster._perception;
            _monster.BuffID = 0;
            _monster.BuffDuration = 0;
            _dropId = monster._drop;
        }
        else
        {
            MyLog.LogError("Monster " + monsterId + " not exist.");
        }
    }

    public void PlayerAtk()
    {
        int atk = Mathf.Max(0, Process.Instance.Player.Atk - _monster.Def);
        _monster.Hp -= atk;
        if (_monster.Hp <= 0)
        {
            Process.Instance.CurEventData = new EventData(EventType.Drop, _dropId);
            UIManager.Instance.mMainWindow.UpdateBottom();
        }
        else
        {
            UIManager.Instance.mMainWindow.PlayBattleAtkAni();
            Timers.inst.Add(1f, 1, PlayerBeAtked);
        }
    }

    public void PlayerBeAtked(object param)
    {
        int atk = Mathf.Max(0, _monster.Atk - Process.Instance.Player.Def);
        Process.Instance.Player.Hp -= atk;
        if (Process.Instance.Player.Hp <= 0)
        {
            if (Process.Instance.Player.Hp < 0)
            {
                Process.Instance.Player.Healthy--;
                if (Process.Instance.Player.Healthy <= 0)
                {
                    Process.Instance.GameOver();
                    return;
                }
                UIManager.Instance.mMainWindow.UpdateHealthy();
            }
            Process.Instance.Player.Hp = 0;
            UIManager.Instance.mMainWindow.UpdateBattleAttr();
            UIManager.Instance.mMainWindow.Tips("战斗失败");
        }
        else
        {
            UIManager.Instance.mMainWindow.PlayBattleAtkAni();
        }        
    }

}