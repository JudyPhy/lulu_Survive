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

    public Monster Monster { get { return _monster; } }
    private Monster _monster;

    public void CreateMonster(int monsterId)
    {
        _monster = new Monster(monsterId);
    }

    public void BattleOver(bool win)
    {
        MyLog.Log("battle win? " + win);
        Process.Instance.Player.InBattle = false;
        if (win)
        {
            Process.Instance.CurEventData = new EventData(EventType.Drop, _monster.Drop);
            UIManager.Instance.mMainWindow.UpdateBottom();
        }
        else
        {

            Process.Instance.CurEventData = new EventData(EventType.Idle, 0, "战斗失败");
            UIManager.Instance.mMainWindow.UpdateUI();
        }
    }
}