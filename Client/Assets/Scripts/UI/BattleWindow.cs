using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BattleWindow : Window
{
    GButton mBtnStatus;
    GButton mBtnFight;
    GButton mBtnRun;

    GTextField mMonsterName;
    GTextField mMonsterHp;
    GTextField mDesc;
    GTextField mRunRate;

    public ConfigMonster mMonsterInfo;
    private int hp;

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("wuxia", "fn_monster").asCom;
        this.Center();
        this.modal = true;

        mBtnStatus = this.contentPane.GetChild("n5").asButton;
        mBtnStatus.onClick.Add(OnClickStatus);
        mBtnFight = this.contentPane.GetChild("n3").asButton;
        mBtnFight.onClick.Add(OnClickFight);
        mBtnRun = this.contentPane.GetChild("n4").asButton;
        mBtnRun.onClick.Add(OnClickRun);

        mMonsterName = this.contentPane.GetChild("textMonsterName").asTextField;
        mMonsterHp = this.contentPane.GetChild("textMosterHp").asTextField;
        mDesc = this.contentPane.GetChild("textEvent").asTextField;
        mRunRate = this.contentPane.GetChild("textRunPro").asTextField;
    }

    private void OnClickStatus(EventContext context)
    {
    }

    private void OnClickFight(EventContext context)
    {
        int atk = Process.Instance.Player.Atk - mMonsterInfo._def;
        atk = atk < 0 ? 0 : atk;
        hp -= atk;
        mMonsterHp.text = hp.ToString();
        if (hp <= 0)
        {
            this.Hide();
            UIManager.Instance.mBottomWindow.Show();
        }
        else
        {
            Timers.inst.Add(1f, 0, MonsterAttack);
        }
    }

    private void MonsterAttack(object param)
    {
        int atk = mMonsterInfo._atk - Process.Instance.Player.Def;
        atk = atk < 0 ? 0 : atk;
        Process.Instance.Player.BeAtc(atk);
    }

    private void OnClickRun(EventContext context)
    {
    }

    protected override void OnShown()
    {
        Debug.Log("BattleWindow shown");
        mMonsterName.text = mMonsterInfo._name;
        mMonsterHp.text = mMonsterInfo._hp.ToString();
        mDesc.text = mMonsterInfo._desc;
        mRunRate.text = "50%";
        hp = mMonsterInfo._hp;
    }
}
