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
import { MyTool } from "./../myTool/myTool"

@ccclass
export class NetManager extends cc.Component {

    private connectState: number = 0;

    private reconnectTime: number = 0;
    private reconnectTimeLimite: number = 5;

    public static Instance: NetManager;
    private gameNet: Network;

    onLoad() {
        NetManager.Instance = this;
        this.gameNet = new Network();
        this.gameNet.connectGS("ws://localhost:9000");
    }

    start() {
        this.registerNetHandler();
    }

    registerNetHandler() {
        // RegisterMessageHandler((int)MsgDef.GS2CLoginRet, GameMsgHandler.Instance.RevMsgGS2CLoginRet);
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
                this.connectedCallback();
            } else if (this.connectState == 2) {
                console.log('connect is closing');
            } else if (this.connectState == 3) {
                console.log('connect failed');
                // this.reconnect();
            }
        }
    }

    connectedCallback() {
        let login = MyTool.AddChild(this.node, "Prefabs/login");
    }

    public SendToGS(id: number, msg: Uint8Array) {
        console.log("SendToGS [id:" + id + "]" + "[msg:" + msg + "]");
        if (null == this.gameNet || !this.gameNet.IsConnected()) {
            console.log("ws disconnect，can't send message[msgid:" + id + "]");
            return false;
        }
        this.gameNet.Send(id, msg);
        this.scheduleOnce(function () {
            console.log("message send timeout: id[" + id + "]");
        }, 5);
    }

}
