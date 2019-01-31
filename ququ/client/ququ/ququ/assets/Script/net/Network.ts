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

import { NetManager } from "./../net/NetManager"

@ccclass
export class Network extends cc.Component {

    private connectState: number = 0;

    private reconnectTime: number = 0;
    private reconnectTimeLimite: number = 5;

    onLoad() {
        NetManager.getInstance().connectGS("ws://localhost:9000");
    }

    start() {

    }

    reconnect() {
        if (this.reconnectTime >= this.reconnectTimeLimite) {
            return;
        }
        this.reconnectTime++;
        console.log("reconnectTime=" + this.reconnectTime);
        NetManager.getInstance().connectGS("ws://localhost:9000");
    }

    update(dt) {
        let curState = NetManager.getInstance().getConnectState();
        if (curState != this.connectState) {
            console.log('net connect state changed:' + curState);
            this.connectState = curState;
            if (this.connectState == 1) {
                console.log('connect success');
                this.connectedCallback();
            } else if (this.connectState == 2) {
                console.log('connect is closing');
            } else if (this.connectState == 3) {
                console.log('connect failed');
                this.reconnect();
            }
        }
    }

    connectedCallback() {
        let prefab = cc.loader.getRes("Prefabs/login")
        let login = cc.instantiate(prefab)
        login.parent = this.node;
    }
    
}
