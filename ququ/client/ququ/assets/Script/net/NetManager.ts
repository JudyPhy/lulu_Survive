// Learn TypeScript:
//  - [Chinese] http://docs.cocos.com/creator/manual/zh/scripting/typescript.html
//  - [English] http://www.cocos2d-x.org/docs/creator/manual/en/scripting/typescript.html
// Learn Attribute:
//  - [Chinese] http://docs.cocos.com/creator/manual/zh/scripting/reference/attributes.html
//  - [English] http://www.cocos2d-x.org/docs/creator/manual/en/scripting/reference/attributes.html
// Learn life-cycle callbacks:
//  - [Chinese] http://docs.cocos.com/creator/manual/zh/scripting/life-cycle-callbacks.html
//  - [English] http://www.cocos2d-x.org/docs/creator/manual/en/scripting/life-cycle-callbacks.html

const { ccclass, property } = cc._decorator;

import { Network } from "./../net/Network"
import { UIManager } from "./../uimanager/uimanager"
import { WindowId } from "./../uimanager/windowDefine"
import { EventDispatch } from "./../handler/EventDispatch"
import { EventType } from "./../handler/EventType"
import { MessageID } from "./../net/messageID"
import { GameMessageHandler } from "./../ui/gameMessageHandler"

@ccclass
export class NetManager extends cc.Component {

    private connectState: number = 0;

    private reconnectTime: number = 0;
    private reconnectTimeLimite: number = 5;

    public static Instance: NetManager;
    private gameNet: Network;

    private messageHandlerArray: Array<MessageHandler> = new Array<MessageHandler>();

    onLoad() {
        NetManager.Instance = this;
        this.gameNet = new Network();
        this.gameNet.connectGS("ws://localhost:9000");
    }

    start() {
        this.registerNetHandler();
        EventDispatch.register(EventType.NET_CONNECT_SUCCESS, this.connectedCallback, null);
    }

    registerNetHandler() {
        this.registerMessageHandler(MessageID.MSG_GS2C_LOGIN_RET, GameMessageHandler.getInstance().RecieveGS2CLoginRet);
        this.registerMessageHandler(MessageID.MSG_C2GS_ENTERROOM_RET, GameMessageHandler.getInstance().RecieveGS2CEnterRoomRet);
        this.registerMessageHandler(MessageID.MSG_GS2C_NEW_ROUND, GameMessageHandler.getInstance().RecieveGS2CNewRoundStart);
        this.registerMessageHandler(MessageID.MSG_GS2C_TURN_TO_BET, GameMessageHandler.getInstance().RecieveGS2CTurnToBet);
        this.registerMessageHandler(MessageID.MSG_GS2C_BET_RET, GameMessageHandler.getInstance().RecieveGS2CBetRet);
        this.registerMessageHandler(MessageID.MSG_GS2C_BET_INFO, GameMessageHandler.getInstance().RecieveGS2CBetInfo);
        this.registerMessageHandler(MessageID.MSG_GS2C_GM_ADD_COIN_RET, GameMessageHandler.getInstance().RecieveGS2CGMAddCoinRet);
        this.registerMessageHandler(MessageID.MSG_GS2C_GAME_RESULT, GameMessageHandler.getInstance().RecieveGS2CGameResults);
    }

    registerMessageHandler(msgId: number, func: Function) {
        let find = false;
        for (let msg of this.messageHandlerArray) {
            if (msg.pid == msgId) {
                find = true;
                console.log("MsgHandler[" + msg.pid + "] has registered.");
                break;
            }
        }
        if (!find) {
            let handler = new MessageHandler();
            handler.pid = msgId;
            handler.function = func;
            this.messageHandlerArray.push(handler);
        }
    }

    reconnect() {
        if (this.reconnectTime >= this.reconnectTimeLimite) {
            return;
        }
        this.reconnectTime++;
        console.log("reconnectTime=" + this.reconnectTime);
        this.gameNet.connectGS("ws://localhost:9000");
    }

    update(dt) {
        let curState = this.gameNet.getConnectState();
        if (curState != this.connectState) {
            console.log('net connect state changed:' + curState);
            this.connectState = curState;
            if (this.connectState == 1) {
                // console.log('connect success');
                // this.connectedCallback();
            } else if (this.connectState == 2) {
                console.log('connect is closing');
            } else if (this.connectState == 3) {
                console.log('connect failed');
                // this.reconnect();
            }
        }
        this.procRevMsgLogic();
    }

    connectedCallback() {
        UIManager.Instance.showWindow(WindowId.Login);
    }

    public SendToGS(id: number, msg: Uint8Array) {
        console.log("SendToGS [id:" + id + "]" + "[msg:" + msg + "]");
        if (null == this.gameNet || !this.gameNet.IsConnected()) {
            console.log("ws disconnect，can't send message[msgid:" + id + "]");
            return false;
        }
        this.gameNet.Send(id, msg);
    }

    private procRevMsgLogic() {
        let count = 10;
        while (this.gameNet.RevMessageArray.length > 0 && count > 0) {
            count--;
            let revMsg = this.gameNet.RevMessageArray.shift();
            if (null == revMsg) {
                continue;
            }
            let find = false;
            for (let handler of this.messageHandlerArray) {
                if (handler.pid == revMsg.pid) {
                    find = true;
                    handler.function(revMsg.buffer);
                    break;
                }
            }
            if (!find) {
                console.log("消息id: ", revMsg.pid, "没有注册处理函数句柄!");
            }
        }
    }

}

export class MessageHandler {
    public pid: number;
    public function: Function;

    public init(id: number, func: Function) {
        this.pid = id;
        this.function = func;
    }
}


