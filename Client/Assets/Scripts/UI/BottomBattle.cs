﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BottomBattle : BottomUI
{
    GButton mBtnStatus;
    GButton mBtnFight;
    GButton mBtnRun;

    GTextField mMonsterName;
    GTextField mMonsterHp;
    GTextField mDesc;
    GTextField mRunRate;

    private ConfigMonster mMonsterInfo;
    private int hp;

    public BottomBattle()
    {
        mObj = UIPackage.CreateObject("wuxia", "fn_moster").asCom;
        mObj.visible = false;
        mBtnStatus = this.mObj.GetChild("n5").asButton;
        mBtnStatus.onClick.Add(OnClickStatus);
        UpdateStatusBtn();
        mBtnFight = this.mObj.GetChild("n3").asButton;
        mBtnFight.onClick.Add(OnClickFight);
        mBtnRun = this.mObj.GetChild("n4").asButton;
        mBtnRun.onClick.Add(OnClickRun);

        mMonsterName = this.mObj.GetChild("textMonsterName").asTextField;
        mMonsterHp = this.mObj.GetChild("textMosterHp").asTextField;
        mDesc = this.mObj.GetChild("textEvent").asTextField;
        mRunRate = this.mObj.GetChild("textRunPro").asTextField;
    }

    public void UpdateUI(int monsterId)
    {
        MyLog.Log("Enter battle, monster:" + monsterId);
        mMonsterInfo = ConfigManager.Instance.ReqMonster(monsterId);
        if (mMonsterInfo != null)
        {
            mMonsterName.text = mMonsterInfo._name;
            mMonsterHp.text = "HP:" + mMonsterInfo._hp.ToString();
            mDesc.text = mMonsterInfo._desc;
            mRunRate.text = "50%";
            hp = mMonsterInfo._hp;
        }
        else
        {
            MyLog.Log("Monster " + monsterId + " not exist.");
        }
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
        MyLog.Log("Monster be attacked, lost hp:" + atk + ", left hp:" + hp);
        mMonsterHp.text = hp.ToString();
        if (hp <= 0)
        {
            Process.Instance.CurEventData = null;
            UIManager.Instance.mMainWindow.UpdateUI();
            UIManager.Instance.mMainWindow.Tips("战斗胜利");
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
            Process.Instance.CurEventData = null;
            UIManager.Instance.mMainWindow.UpdateUI();
            UIManager.Instance.mMainWindow.Tips("逃跑成功");
        }
        else
        {
            MonsterAttack(null);
        }
    }
}
