import { NetManager } from "./../net/NetManager"
import { MessageID } from "./../net/messageID"
import { pb } from "./../pbProc/pb"
import { PlayerManager } from "./../player/playerManager"
import { UIManager } from "../uimanager/uimanager";
import { WindowId } from "../uimanager/windowDefine";

export class GameMessageHandler {

    private static instance: GameMessageHandler = null;
    public static getInstance(): GameMessageHandler {
        if (GameMessageHandler.instance == null) {
            GameMessageHandler.instance = new GameMessageHandler();
        }
        return GameMessageHandler.instance;
    }

    public SendC2GSLogin(user: string, password: string) {
        let msg = pb.C2GSLogin.create({ user: user, password: password });
        let buffer = pb.C2GSLogin.encode(msg).finish();
        NetManager.Instance.SendToGS(MessageID.MSG_C2GS_LOGIN, buffer);
    }

    public RecieveGS2CLoginRet(buffer: Uint8Array) {
        let data = pb.GS2CLoginRet.decode(buffer);
        console.log(data.user, data.errorCode);
        if (data.errorCode == pb.GS2CLoginRet.ErrorCode.Success) {
            PlayerManager.getInstance().initMy(data.user);
            UIManager.Instance.showWindow(WindowId.Hall);
        } else {
            console.error("login failed");
        }
    }

}
