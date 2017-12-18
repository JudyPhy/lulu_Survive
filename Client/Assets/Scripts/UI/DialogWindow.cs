using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using DG.Tweening;

public class DialogWindow : Window
{
    private GButton mBtn1;
    private GButton mBtn2;
    private GButton mBtnOver;
    private GButton mBtnSkip;
    private List<GTextField> mTextFiledList = new List<GTextField>();

    public ConfigStory mStoryInfo;

    private List<string> mDialogList = new List<string>();
    private int mCurDialogIndex;
    private int mCurWordCount;
    private float mYStart = 50;
    private float mYSpace = 60;

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("wuxia", "UI_storyA").asCom;
        this.Center();
        this.modal = true;

        mBtn1 = this.contentPane.GetChild("btnChooseA").asButton;
        mBtn1.onClick.Add(OnClickOptionBtn);       
        mBtn2 = this.contentPane.GetChild("btnChooseB").asButton;
        mBtn2.onClick.Add(OnClickOptionBtn);

        mBtnOver = this.contentPane.GetChild("btnOver").asButton;
        mBtnOver.onClick.Add(OnClickBtnOver);
        mBtnSkip = this.contentPane.GetChild("btnSkip").asButton;
        mBtnSkip.onClick.Add(OnClickBtnSkip);
    }

    private void OnClickOptionBtn(EventContext context)
    {       
        MyLog.Log("OnClickBtn");
        int nextDialogId = context.sender == mBtn1 ? mStoryInfo._optionList[0].result : mStoryInfo._optionList[1].result;
        TurnToNext(nextDialogId);
    }

    private void OnClickBtnOver(EventContext context)
    {        
        MyLog.Log("OnClickBtnOver");
        TurnToNext(mStoryInfo._nextId);
    }

    private void OnClickBtnSkip(EventContext context)
    {
        MyLog.Log("OnClickBtnSkip");
        HideAllText();
        for (int i = 0; i < mDialogList.Count; i++)
        {
            GTextField textField = GetTextField(i);
            textField.visible = true;
            textField.text = mDialogList[i];
            textField.SetPosition(0, mYStart + mYSpace * i, 0);
        }        
        if (mStoryInfo._type == 2)
        {
            ShowOptionBtns();
        }
        else
        {
            mBtnOver.visible = true;
        }
    }

    private void TurnToNext(int nextDialogId)
    {
        MyLog.Log("TurnToNext: nextDialogId=" + nextDialogId);
        if (nextDialogId != 0)
        {
            //to next dialog
            Process.Instance.TurnToNextDialog(nextDialogId);
            ConfigStory curStory = ConfigManager.Instance.ReqStory(Process.Instance.NextStoryID);
            if (curStory != null)
            {
                mStoryInfo = curStory;
                OnShown();
            }
        }
        else
        {
            //to main ui
            Hide();
            UIManager.Instance.mMainWindow.Show();
        }
    }

    private void HideAllText()
    {
        for (int i = 0; i < mTextFiledList.Count; i++)
        {
            mTextFiledList[i].visible = false;
        }
    }

    private GTextField GetTextField(int index)
    {
        if (index < mTextFiledList.Count)
        {
            return mTextFiledList[index];
        }
        GComponent com = UIPackage.CreateObject("wuxia", "textBox").asCom;
        this.contentPane.AddChild(com);
        GTextField textField = com.GetChild("n0").asTextField;
        mTextFiledList.Add(textField);
        return textField;
    }

    protected override void OnShown()
    {
        mDialogList.Clear();
        HideAllText();
        HideBtns();
        //split dialog
        int startIndex = 0;
        for (int i = 0; i < mStoryInfo._desc.Length; i++)
        {
            if (mStoryInfo._desc[i] == '|')
            {
                string str = mStoryInfo._desc.Substring(startIndex, i - startIndex);
                mDialogList.Add(str);
                startIndex = i + 1;
            }
        }
        mDialogList.Add(mStoryInfo._desc.Substring(startIndex));
        mCurDialogIndex = 0;
        mCurWordCount = 1;
        Timers.inst.Add(0.1f, 0, UpdateDialog);
        mBtnSkip.visible = true;
    }    

    private void UpdateDialog(object param)
    {
        if (mCurDialogIndex >= mDialogList.Count)
        {
            MyLog.Log("Dialog over");
            Timers.inst.Remove(UpdateDialog);
            if (mStoryInfo._type == 2)
            {
                ShowOptionBtns();
            }
        }
        else
        {
            string str = mDialogList[mCurDialogIndex].Substring(0, mCurWordCount);
            //MyLog.LogError("UpdateDialog: str=" + str);
            mCurWordCount++;
            GTextField textField = GetTextField(mCurDialogIndex);
            textField.visible = true;
            textField.text = str;
            textField.SetPosition(0, mYStart + mYSpace * mCurDialogIndex, 0);
            if (mCurWordCount > mDialogList[mCurDialogIndex].Length)
            {
                mCurDialogIndex++;
                mCurWordCount = 1;
            }
        }
    }

    private void ShowOptionBtns()
    {
        mBtnSkip.visible = false;
        mBtn1.visible = true;
        mBtn1.text = mStoryInfo._optionList[0].option;
        mBtn2.visible = true;
        mBtn2.text = mStoryInfo._optionList[1].option;
    }

    private void HideBtns()
    {
        mBtn1.visible = false;
        mBtn2.visible = false;
        mBtnOver.visible = false;
        mBtnSkip.visible = false;
    }
}
