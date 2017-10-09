using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Login : WindowsBasePanel
{
    private UITexture _girl;

    private GameObject _inputContainer;
    private UIInput _accountInput;
    private UIInput _passwordInput;
    private GameObject _btnEnterGame;

    public override void OnAwake()
    {
        base.OnAwake();
        _girl = transform.FindChild("BG/girl").GetComponent<UITexture>();
        _girl.alpha = 0;

        _inputContainer = transform.FindChild("InputContainer").gameObject;
        _inputContainer.SetActive(false);
        _accountInput = _inputContainer.transform.FindChild("AccountInput").GetComponent<UIInput>();
        _passwordInput = _inputContainer.transform.FindChild("PasswordInput").GetComponent<UIInput>();
        _btnEnterGame = _inputContainer.transform.FindChild("EnterButton").gameObject;
        UIEventListener.Get(_btnEnterGame).onClick = OnEnterGame;
    }

    public override void OnEnableWindow()
    {
        base.OnEnableWindow();
        Invoke("PlayLoginAni", 0.5f);
    }

    private void PlayLoginAni()
    {
        _girl.transform.localPosition = new Vector3(0, -35, 0);
        iTween.ValueTo(_girl.gameObject, iTween.Hash("from", 0, "to", 1, "time", 1f, "onupdate", "UpdateGirlAlpha",
            "oncomplete", "GilrMoveToLeft"));
        Invoke("GilrMoveToLeft", 1f);
    }

    private void GilrMoveToLeft()
    {
        iTween.MoveTo(_girl.gameObject, iTween.Hash("x", -320, "islocal", true, "time", 0.5f));
        Invoke("ShowInput", 0.5f);
    }

    private void ShowInput()
    {
        _inputContainer.transform.localScale = Vector3.zero;
        _inputContainer.SetActive(true);
        iTween.ScaleTo(_inputContainer, iTween.Hash("scale", Vector3.one, "time", 0.5f));
    }

    private void OnEnterGame(GameObject go)
    {
        if (string.IsNullOrEmpty(_accountInput.value))
        {
            UIManager.Instance.ShowTips(TipsType.text, "账号不能为空");
            return;
        }
        if (string.IsNullOrEmpty(_passwordInput.value))
        {
            UIManager.Instance.ShowTips(TipsType.text, "密码不能为空");
            return;
        }
        LoginMsgHandler.Instance.SendMsgC2GSLogin(_accountInput.value, _passwordInput.value);
    }
}
