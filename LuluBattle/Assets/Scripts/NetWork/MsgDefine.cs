using UnityEngine;
using System.Collections;

public enum MsgDef
{
    None = -1,
    C2GSLogin = 0,
    GS2CLoginRet = 1,
    C2GSChooseRole = 2,
    GS2CChooseRoleRet = 3,
    C2GSStartGame = 4,
    GS2CStartGameRet = 5,
    C2GSSyncPkg = 6,
    GS2CSyncPkg = 7,
}
