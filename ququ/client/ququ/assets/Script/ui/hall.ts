import { GameMessageHandler } from "./gameMessageHandler";
import { UIManager } from "../uimanager/uimanager";

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

@ccclass
export class Hall extends cc.Component {

    mButtonFree: cc.Button;
    mButtonFriend: cc.Button;
    mButtonJoin: cc.Button;

    onLoad() {
        this.mButtonFree = cc.find("button_free", this.node).getComponent(cc.Button);
        this.mButtonFree.node.on('click', this.onClickFree, this);

        this.mButtonFriend = cc.find("button_friend", this.node).getComponent(cc.Button);
        this.mButtonFriend.node.on('click', this.onClickFriend, this);

        this.mButtonJoin = cc.find("button_join", this.node).getComponent(cc.Button);
        this.mButtonJoin.node.on('click', this.onClickJoin, this);

        UIManager.Instance.showPlayerInfoNode(true);
    }

    start() {
    }

    onClickFree(button) {
        console.log("onClickFree");
        GameMessageHandler.getInstance().SendC2GSEnterRoom(0, 0);
    }

    onClickFriend(button) {
        console.log("onClickFriend");
    }

    onClickJoin(button) {
        console.log("onClickJoin");
    }

    // update (dt) {}
}
