using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTransmit;

public class Item_LoginRole : MonoBehaviour
{
    private UISprite _headIcon;
    private UISprite _bg;
    private RoleTypeData _data;
    public RoleTypeData Data
    {
        get { return _data; }
    }

    public void Awake()
    {
        _headIcon = transform.FindChild("headIcon").GetComponent<UISprite>();
        _bg = transform.FindChild("bg").GetComponent<UISprite>();
        UIEventListener.Get(gameObject).onClick = OnChooseRole;
    }

    public void UpdateUI(RoleTypeData data)
    {
        _data = data;
        _headIcon.spriteName = data._headIcon;
    }

    private void OnChooseRole(GameObject go)
    {
        EventDispatcher.TriggerEvent<RoleTypeData>(EventDefine.ChooseRole, _data);
    }

    public void ChooseRole(bool choose)
    {
        _bg.gameObject.SetActive(choose);
    }

    private void Update()
    {

    }
}
