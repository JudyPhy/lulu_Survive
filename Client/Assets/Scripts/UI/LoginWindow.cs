using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using DG.Tweening;

public class LoginWindow : BaseWindow
{
    private GButton mBtnNew;
    private GButton mBtnContinue;

    public override void OnAwake()
    {
        mBtnNew = mWindowObj.GetChild("btnNewGame").asButton;
        mBtnNew.onClick.Add(OnClickNew);
        mBtnContinue = mWindowObj.GetChild("btnLoadGame").asButton;
        mBtnContinue.visible = GameSaved.HasHistory();
        mBtnContinue.onClick.Add(OnClickContinue);
    }

    private void OnClickNew(EventContext context)
    {
        Process.Instance.StartNewGame();
        EnterGame();
    }

    private void OnClickContinue(EventContext context)
    {
        Process.Instance.StartHistoryGame();
        EnterGame();
    }

    private void EnterGame()
    {
        if (Process.Instance.NeedShowDialog())
        {
            MyLog.Log("Play dialog[" + Process.Instance.NextStoryID + "]");
            UIManager.Instance.ShowWindow<DialogWindow>(WindowType.WINDOW_DIALOG, WindowType.WINDOW_MAIN);
        }
        else
        {
            UIManager.Instance.ShowWindow<MainWindow>(WindowType.WINDOW_MAIN);
        }
    }
}
