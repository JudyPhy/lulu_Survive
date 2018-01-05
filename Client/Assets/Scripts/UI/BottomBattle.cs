using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class BottomBattle : BaseWindow
{
    private GButton mBtnStatus;
    private GButton mBtnFight;
    private GButton mBtnRun;

    private GTextField mMonsterName;
    private GTextField mMonsterHp;
    private GTextField mDesc;
    private GTextField mRunRate;

    public override void OnAwake()
    {
        mBtnStatus = mWindowObj.GetChild("n5").asButton;
        mBtnStatus.onClick.Add(OnClickStatus);

        mBtnFight = mWindowObj.GetChild("n3").asButton;
        mBtnFight.onClick.Add(OnClickFight);
        mBtnRun = mWindowObj.GetChild("n4").asButton;
        mBtnRun.onClick.Add(OnClickRun);

        mMonsterName = mWindowObj.GetChild("textMonsterName").asTextField;
        mMonsterHp = mWindowObj.GetChild("textMosterHp").asTextField;
        mDesc = mWindowObj.GetChild("textEvent").asTextField;
        mRunRate = mWindowObj.GetChild("textRunPro").asTextField;
    }

    protected override void OnRegisterEvent()
    {
        UIManager.mEventDispatch.AddEventListener(EventDefine.UPDATE_MONSTER_UI, UpdateMonsterUI);
    }

    protected override void OnRemoveEvent()
    {
        UIManager.mEventDispatch.RemoveEventListener(EventDefine.UPDATE_MONSTER_UI, UpdateMonsterUI);
    }

    public override void OnEnable()
    {
        int monsterId = Process.Instance.CurEventData._id;
        MyLog.Log("show battle, monster:" + monsterId);
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
        if (!Process.Instance.Player.InBattle)
        {
            Process.Instance.Player.PlayAtk();
        }
    }

    private void OnClickRun(EventContext context)
    {
        int rate = Random.Range(0, 101);
        if (rate > 50)
        {
            Process.Instance.CurEventData = null;
            EventContext param = new EventContext();
            param.data = "逃跑成功";
            UIManager.mEventDispatch.DispatchEvent(EventDefine.UPDATE_TIPS, param);
        }
        else
        {
            BattleManager.Instance.Monster.PlayAtk(null);
        }
    }

    public void UpdateMonsterUI(EventContext context)
    {
        Debug.LogError("context.data:" + context.data);
        mDesc.text = (string)context.data;
        mMonsterHp.text = "HP:" + BattleManager.Instance.Monster.Hp.ToString();
    }
}
