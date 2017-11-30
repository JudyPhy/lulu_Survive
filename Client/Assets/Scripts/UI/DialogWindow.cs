using System;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using DG.Tweening;

public class DialogWindow : Window
{
    private GButton mBtn1;
    private GButton mBtn2;
    private List<GTextField> mTextFiledList = new List<GTextField>();

    public ConfigStory mStoryInfo;

    private List<string> mDialogList = new List<string>();
    private int mCurDialogIndex;
    private int mCurWordCount;
    private float mYStart = 50;
    private float mYSpace = 30;

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("wuxia", "UI_dialog").asCom;
        this.Center();
        this.modal = true;

        mBtn1 = this.contentPane.GetChild("n1").asButton;
        mBtn1.onClick.Add(OnClickBtn);
        mBtn1.visible = false;
        mBtn2 = this.contentPane.GetChild("n2").asButton;
        mBtn2.onClick.Add(OnClickBtn);
        mBtn2.visible = false;
    }

    private void OnClickBtn(EventContext context)
    {
        int nextDialogId = context.sender == mBtn1 ? mStoryInfo._optionList[0].result : mStoryInfo._optionList[1].result;
        if (nextDialogId != 0)
        {

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
        GTextField textField = UIPackage.CreateObject("wuxia", "cp_dialogText").asTextField;
        mTextFiledList.Add(textField);
        return textField;
    }

    protected override void OnShown()
    {
        mDialogList.Clear();
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
        mCurDialogIndex = 0;
        mCurWordCount = 1;
        Timers.inst.Add(1f, 1, UpdateDialog);
    }

    private void UpdateDialog(object param)
    {
        if (mCurWordCount > mDialogList[mCurDialogIndex].Length)
        {
            mCurDialogIndex++;
            mCurWordCount = 1;         
        }
        if (mCurDialogIndex >= mDialogList.Count)
        {
            Debug.Log("Dialog over");
            if (mStoryInfo._type == 2)
            {
                ShowBtns();
            }
            else
            {

            }
        }
        else
        {
            string str = mDialogList[mCurDialogIndex].Substring(0, mCurWordCount);
            mCurWordCount++;
            GTextField textField = GetTextField(mCurDialogIndex);
            textField.visible = true;
            textField.text = str;
            textField.SetPosition(0, mYStart + mYSpace * mCurDialogIndex, 0);
        }
    }

    private void ShowBtns()
    {
        mBtn1.visible = true;
        mBtn1.text = mStoryInfo._optionList[0].option;
        mBtn2.visible = true;
        mBtn2.text = mStoryInfo._optionList[1].option;
    }

    private void ShowDialog(GLabel label, string context)
    {
        label.text = context;
    }

}
