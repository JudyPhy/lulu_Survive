using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipsType
{
    text = 0,
}

public class Panel_Tips : MonoBehaviour {

    private GameObject _textTipsContainer;

    void Awake()
    {
        _textTipsContainer = transform.FindChild("TextTipsContainer").gameObject;
    }

    public void ShowTextTips(string text)
    {
        TextTips tips = UIManager.AddChild<TextTips>(_textTipsContainer);
        tips.Show(text);
    }
    
    void Update () {
		
	}
}
