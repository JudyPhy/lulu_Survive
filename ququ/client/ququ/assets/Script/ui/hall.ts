import { PlayerManager } from "../player/playerManager";
import { Player } from "../player/player";
import { GameMessageHandler } from "./gameMessageHandler";

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

    mName: cc.Label;
    mHeadicon: cc.Sprite;
    mCard: cc.Label;
    mCoin: cc.Label;
    mButtonCharge: cc.Button;

    mButtonFree: cc.Button;
    mButtonFriend: cc.Button;
    mButtonJoin: cc.Button;

    onLoad() {
        let topContainer = cc.find("topContainer", this.node);
        this.mName = cc.find("nameContainer/name", topContainer).getComponent(cc.Label);
        // this.mHeadicon = topContainer.getChildByName("Container/Password").getComponent(cc.Sprite);
        this.mCard = cc.find("cardContainer/card", topContainer).getComponent(cc.Label);
        this.mCoin = cc.find("coinContaner/coin", topContainer).getComponent(cc.Label);

        this.mButtonCharge = cc.find("button_charge", topContainer).getComponent(cc.Button);
        this.mButtonCharge.node.on('click', this.onClickCharge, this);

        this.mButtonFree = cc.find("button_free", this.node).getComponent(cc.Button);
        this.mButtonFree.node.on('click', this.onClickFree, this);

        this.mButtonFriend = cc.find("button_friend", this.node).getComponent(cc.Button);
        this.mButtonFriend.node.on('click', this.onClickFriend, this);

        this.mButtonJoin = cc.find("button_join", this.node).getComponent(cc.Button);
        this.mButtonJoin.node.on('click', this.onClickJoin, this);
    }

    start() {
        this.initUI();
    }

    initUI() {
        let player: Player = PlayerManager.getInstance().mMy;
        this.mName.string = player.nickname;
        this.mCard.string = player.card.toString();
        this.mCoin.string = player.coin.toString();
    }

    onClickCharge(button) {
        console.log("onClickCharge");
    }

    onClickFree(button) {
        console.log("onClickFree");
        GameMessageHandler.getInstance().SendC2GSEnterRoom(null, null);
    }

    onClickFriend(button) {
        console.log("onClickFriend");
    }

    onClickJoin(button) {
        console.log("onClickJoin");
    }

    // update (dt) {}
}
