import { PlayerManager } from "../player/playerManager";
import { Player } from "../player/player";
import { GameMessageHandler } from "./gameMessageHandler";
import { EventDispatch } from "../handler/EventDispatch";
import { EventType } from "../handler/EventType";
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
export class PlayerInfoNode extends cc.Component {

    public static Instance: PlayerInfoNode;
    mName: cc.Label;
    mHeadicon: cc.Sprite;
    mCard: cc.Label;
    mCoin: cc.Label;
    mButtonCharge: cc.Button;

    onLoad() {
        PlayerInfoNode.Instance = this;
        this.mName = cc.find("nameContainer/name", this.node).getComponent(cc.Label);
        // this.mHeadicon = topContainer.getChildByName("Container/Password").getComponent(cc.Sprite);
        this.mCard = cc.find("cardContainer/card", this.node).getComponent(cc.Label);
        this.mCoin = cc.find("coinContaner/coin", this.node).getComponent(cc.Label);
        this.mButtonCharge = cc.find("button_charge", this.node).getComponent(cc.Button);
        this.mButtonCharge.node.on('click', this.onClickCharge, this);
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
        GameMessageHandler.getInstance().sendC2GSGMAddCoin(1);
    }

    // update (dt) {}
}
