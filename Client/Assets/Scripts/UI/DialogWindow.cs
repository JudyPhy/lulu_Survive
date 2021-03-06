﻿using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using DG.Tweening;

public class DialogWindow : BaseWindow
{
    private GButton mBtn1;
    private GButton mBtn2;
    private GButton mBtnOver;
    private GButton mBtnSkip;
    private List<GTextField> mTextFiledList = new List<GTextField>();

    private ConfigStory mStoryInfo;
    private string mHideToScene;

    private List<string> mDialogList = new List<string>();
    private int mCurDialogIndex;
    private int mCurWordCount;
    private float mYStart = 50;
    private float mYSpace = 60;

    public override void OnAwake()
    {
        mBtn1 = mWindowObj.GetChild("btnChooseA").asButton;
        mBtn1.onClick.Add(OnClickOptionBtn);       
        mBtn2 = mWindowObj.GetChild("btnChooseB").asButton;
        mBtn2.onClick.Add(OnClickOptionBtn);

        mBtnOver = mWindowObj.GetChild("btnOver").asButton;
        mBtnOver.onClick.Add(OnClickBtnOver);
        mBtnSkip = mWindowObj.GetChild("btnSkip").asButton;
        mBtnSkip.onClick.Add(OnClickBtnSkip);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        mHideToScene = mParam != null ? (string)mParam : WindowType.WINDOW_MAIN;
        HideAllText();
        HideBtns();
        mStoryInfo = ConfigManager.Instance.ReqStory(Process.Instance.NextStoryID);
        if (mStoryInfo != null)
        {
            mDialogList.Clear();
            SplitDialog(mStoryInfo._desc);
            mCurDialogIndex = 0;
            mCurWordCount = 1;
            Timers.inst.Add(0.1f, 0, UpdateDialog);
        }
        else
        {
            MyLog.LogError("Dialog[" + Process.Instance.NextStoryID + "] not exist");
            SwitchToWindow();
        }
    }

    private void SwitchToWindow()
    {
        if (mHideToScene == WindowType.WINDOW_SLEEP)
        {
            UIManager.Instance.ShowWindow<SleepWindow>(WindowType.WINDOW_SLEEP);
        }
        else
        {
            UIManager.Instance.ShowWindow<MainWindow>(WindowType.WINDOW_MAIN);
        }
    }

    private void OnClickOptionBtn(EventContext context)
    {       
        int resultType = context.sender == mBtn1 ? mStoryInfo._optionList[0].type : mStoryInfo._optionList[1].type;
        int resultId = context.sender == mBtn1 ? mStoryInfo._optionList[0].result : mStoryInfo._optionList[1].result;
        switch (resultType)
        {
            case (int)DialogChooseResultType.ToNextDialog:
                Process.Instance.TurnToNextDialog(resultId);
                OnEnable();
                break;
            default:
                Process.Instance.TurnToNextDialog(0);
                if (mStoryInfo._type == 3 && Process.Instance.CurScene != mStoryInfo._sceneId)
                {
                    Process.Instance.SwitchScene(mStoryInfo._sceneId);
                }
                SwitchToWindow();
                break;
        }
    }

    private void OnClickBtnOver(EventContext context)
    {
        if (mStoryInfo._type == (int)DialogType.ToNextDialog)
        {
            Process.Instance.TurnToNextDialog(mStoryInfo._nextId);
            OnEnable();
        }
        else
        {
            Process.Instance.TurnToNextDialog(0);
            if (mStoryInfo._type == 3 && Process.Instance.CurScene != mStoryInfo._sceneId)
            {
                Process.Instance.SwitchScene(mStoryInfo._sceneId);
            }
            SwitchToWindow();
        }
    }

    private void OnClickBtnSkip(EventContext context)
    {
        Timers.inst.Remove(UpdateDialog);
        HideAllText();
        for (int i = 0; i < mDialogList.Count; i++)
        {
            GTextField textField = GetTextField(i);
            textField.visible = true;
            textField.text = mDialogList[i];
            textField.SetPosition(0, mYStart + mYSpace * i, 0);
        }
        DialogOver();
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
        mWindowObj.AddChild(com);
        GTextField textField = com.GetChild("n0").asTextField;
        mTextFiledList.Add(textField);
        return textField;
    }    

    private void SplitDialog(string fullContent)
    {
        int startIndex = 0;
        for (int i = 0; i < fullContent.Length; i++)
        {
            if (fullContent[i] == '|')
            {
                string str = fullContent.Substring(startIndex, i - startIndex);
                mDialogList.Add(str);
                startIndex = i + 1;
            }
        }
        mDialogList.Add(fullContent.Substring(startIndex));
    }

    private void DialogOver()
    {
        //MyLog.Log("Dialog over, type=" + mStoryInfo._type);
        switch (mStoryInfo._type)
        {
            case 1:
            case 3:
                mBtnOver.visible = true;
                mBtnSkip.visible = false;
                break;
            case 2:
                ShowOptionBtns();
                break;            
            default:
                break;
        }
    }

    private void UpdateDialog(object param)
    {
        if (mCurDialogIndex >= mDialogList.Count)
        {            
            Timers.inst.Remove(UpdateDialog);
            DialogOver();
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
            mBtnSkip.visible = true;
        }
    }

    private void ShowOptionBtns()
    {        
        mBtn1.visible = true;
        mBtn1.text = mStoryInfo._optionList[0].option;
        mBtn2.visible = true;
        mBtn2.text = mStoryInfo._optionList[1].option;
        mBtnSkip.visible = false;
        mBtnOver.visible = false;
    }

    private void HideBtns()
    {
        mBtn1.visible = false;
        mBtn2.visible = false;
        mBtnOver.visible = false;
        mBtnSkip.visible = false;
    }
}


