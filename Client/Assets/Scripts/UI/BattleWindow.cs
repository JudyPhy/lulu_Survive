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
        UpdateStatusBtn();
        mBtnFight = this.contentPane.GetChild("n3").asButton;
        mBtnFight.onClick.Add(OnClickFight);
        mBtnRun = this.contentPane.GetChild("n4").asButton;
        mBtnRun.onClick.Add(OnClickRun);

        mMonsterName = this.contentPane.GetChild("textMonsterName").asTextField;
        mMonsterHp = this.contentPane.GetChild("textMosterHp").asTextField;
        mDesc = this.contentPane.GetChild("textEvent").asTextField;
        mRunRate = this.contentPane.GetChild("textRunPro").asTextField;
    }

    private void UpdateStatusBtn()
    {
        switch (Process.Instance.Player.Status)
        {
            case 0:
                mBtnStatus.text = "平衡";
                break;
            case 1:
                mBtnStatus.text = "拼命";
                break;
            case 2:
                mBtnStatus.text = "猥琐";
                break;
        }
    }

    private void OnClickStatus(EventContext context)
    {
        Process.Instance.Player.GoToNextStatus();
        UpdateStatusBtn();
    }

    private void OnClickFight(EventContext context)
    {        
        int atk = Process.Instance.Player.Atk - mMonsterInfo._def;
        atk = atk < 0 ? 0 : atk;        
        hp -= atk;
        Debug.Log("Monster be attacked, lost hp:" + atk + ", left hp:" + hp);
        mMonsterHp.text = hp.ToString();
        if (hp <= 0)
        {
            this.Hide();
            UIManager.Instance.mBottomWindow.Show();
            UIManager.Instance.mBottomWindow.Tips("战斗胜利");
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
        int rate = Random.Range(0, 101);
        if (rate > 50)
        {
            Hide();
            UIManager.Instance.mBottomWindow.Show();
            UIManager.Instance.mBottomWindow.Tips("逃跑成功");
        }
        else
        {
            MonsterAttack(null);
        }
    }

    protected override void OnShown()
    {
        Debug.Log("BattleWindow shown");
        mMonsterName.text = mMonsterInfo._name;
        mMonsterHp.text = "HP:" + mMonsterInfo._hp.ToString();
        mDesc.text = mMonsterInfo._desc;
        mRunRate.text = "50%";
        hp = mMonsterInfo._hp;
    }
}
