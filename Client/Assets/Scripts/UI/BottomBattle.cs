using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BottomBattle : BottomUI
{
    private GButton mBtnStatus;
    private GButton mBtnFight;
    private GButton mBtnRun;

    private GTextField mMonsterName;
    private GTextField mMonsterHp;
    private GTextField mDesc;
    private GTextField mRunRate;
    
    public BottomBattle()
    {
        mObj = UIPackage.CreateObject("wuxia", "fn_moster").asCom;
        mObj.visible = false;
        mBtnStatus = this.mObj.GetChild("n5").asButton;
        mBtnStatus.onClick.Add(OnClickStatus);

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
        BattleManager.Instance.CreateMonster(monsterId);
        mMonsterName.text = BattleManager.Instance.Monster.Name;
        mMonsterHp.text = "HP:" + BattleManager.Instance.Monster.Hp.ToString();
        mDesc.text = BattleManager.Instance.Monster.Desc;
        mRunRate.text = "50%";
        UpdateStatusBtn();
    }

    private void UpdateStatusBtn()
    {
        switch (Process.Instance.Player.Status)
        {
            case PlayerBattleStatus.Balance:
                mBtnStatus.text = "平衡";
                break;
            case PlayerBattleStatus.Risk:
                mBtnStatus.text = "拼命";
                break;
            case PlayerBattleStatus.Filthy:
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
        Process.Instance.Player.PlayAtk();
    }

    private void OnClickRun(EventContext context)
    {
        int rate = Random.Range(0, 101);
        if (rate > 50)
        {
            Process.Instance.CurEventData = null;
            UIManager.Instance.mMainWindow.CommonTips("逃跑成功");
        }
        else
        {
            BattleManager.Instance.Monster.PlayAtk(null);
        }
    }

    public void UpdateMonsterUI(string desc)
    {
        mDesc.text = desc;
        mMonsterHp.text = "HP:" + BattleManager.Instance.Monster.Hp.ToString();
    }
}
