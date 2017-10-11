using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTransmit;

public class Panel_ChooseRole : WindowsBasePanel
{
    //private UIGrid _roleRoot;
    //private List<Item_LoginRole> _roles = new List<Item_LoginRole>();
    //private UIInput _nickName;
    //private GameObject _btnStartGame;
    //private GameObject _btnBack;

    private string _curChoosedRole;

    public override void OnAwake()
    {
        base.OnAwake();

        //_roleRoot = transform.FindChild("UIGrid").GetComponent<UIGrid>();
        //_nickName = transform.FindChild("nickName").GetComponent<UIInput>();
        //_btnStartGame = transform.FindChild("UIGrid").gameObject;
        //_btnBack = transform.FindChild("btnBack").gameObject;
        //UIEventListener.Get(_btnStartGame).onClick = OnClickStartGame;
        //UIEventListener.Get(_btnBack).onClick = OnClickBack;
    }

    public override void OnStart()
    {
        base.OnStart();

        LoginMsgHandler.Instance.SendMsgC2GSChooseRole("lulu", "lulu");

        //List<RoleTypeData> roleDatas = ConfigManager.Instance.GetLoginRoleTypes();
        //_curChoosedRole = roleDatas[0]._headIcon;
        //for (int i = 0; i < roleDatas.Count; i++)
        //{
        //    Item_LoginRole script = UIManager.AddChild<Item_LoginRole>(_roleRoot.gameObject);
        //    script.UpdateUI(roleDatas[i]);
        //    script.ChooseRole(i == 0);
        //    _roles.Add(script);
        //}
        //_roleRoot.repositionNow = true;
    }

    public override void OnRegisterEvent()
    {
        EventDispatcher.AddEventListener<RoleTypeData>(EventDefine.ChooseRole, ChooseRole);
    }

    public override void OnRemoveEvent()
    {
        EventDispatcher.RemoveEventListener<RoleTypeData>(EventDefine.ChooseRole, ChooseRole);
    }

    private void ChooseRole(RoleTypeData data)
    {
        //_curChoosedRole = data._headIcon;
        //for (int i = 0; i < _roles.Count; i++)
        //{
        //    _roles[i].ChooseRole(_roles[i].Data._id == data._id);            
        //}
    }

    private void OnClickBack(GameObject go)
    {
        UIManager.Instance.ShowMainWindow<Panel_Login>(eWindowsID.LoginUI);
    }

    private void OnClickStartGame(GameObject go)
    {
        //if (string.IsNullOrEmpty(_nickName.value))
        //{
        //    UIManager.Instance.ShowTips(TipsType.text, "nickName can't be null.");
        //    return;
        //}
        //LoginMsgHandler.Instance.SendMsgC2GSChooseRole(_nickName.value, _curChoosedRole);        
    }

}
