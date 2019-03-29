import { EventDispatch } from "../handler/EventDispatch";
import { EventType } from "../handler/EventType";
import { PlayerManager } from "../player/playerManager";
import { UIManager } from "../uimanager/uimanager";
import { GameState, Side, Player } from "../player/player";
import { pb } from "../pbproc/pb";
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
export class Room extends cc.Component {

    roomId: cc.Label;
    roundIndex: cc.Label;

    blue_bet_bar: cc.Sprite;
    blue_bet_bar_shine: cc.Sprite;
    blue_bet_value: cc.Label;
    button_blue_bet: cc.Button;

    red_bet_bar: cc.Sprite;
    red_bet_bar_shine: cc.Sprite;
    red_bet_value: cc.Label;
    button_red_bet: cc.Button;

    bet_value: cc.Label;
    button_all: cc.Button;
    button_follow: cc.Button;
    button_add: cc.Button;

    bottomContainer: cc.Node;
    button_bet200: cc.Button;
    button_bet400: cc.Button;
    button_bet600: cc.Button;
    button_bet1600: cc.Button;

    onLoad() {
        this.roomId = cc.find("table/room_num/roomId", this.node).getComponent(cc.Label);
        this.roundIndex = cc.find("table/title/roundIndex", this.node).getComponent(cc.Label);

        this.blue_bet_bar = cc.find("betContainer/betBar/betBar_blue", this.node).getComponent(cc.Sprite);
        this.blue_bet_bar_shine = cc.find("betContainer/betBar/shine_blue", this.node).getComponent(cc.Sprite);
        this.blue_bet_value = cc.find("betContainer/betBar/betBar_blue/blue_value", this.node).getComponent(cc.Label);
        this.button_blue_bet = cc.find("betContainer/betBar/betBar_blue/buttonBet_blue", this.node).getComponent(cc.Button);
        this.button_blue_bet.node.on('click', this.onClickBetSide, this);

        this.red_bet_bar = cc.find("betContainer/betBar/betBar_red", this.node).getComponent(cc.Sprite);
        this.red_bet_bar_shine = cc.find("betContainer/betBar/shine_red", this.node).getComponent(cc.Sprite);
        this.red_bet_value = cc.find("betContainer/betBar/betBar_red/red_value", this.node).getComponent(cc.Label);
        this.button_red_bet = cc.find("betContainer/betBar/betBar_red/buttonBet_red", this.node).getComponent(cc.Button);
        this.button_red_bet.node.on('click', this.onClickBetSide, this);

        this.bet_value = cc.find("betContainer/bet/value", this.node).getComponent(cc.Label);
        this.button_all = cc.find("betContainer/bet/all", this.node).getComponent(cc.Button);
        this.button_all.node.on('click', this.onClickBetType, this);
        this.button_follow = cc.find("betContainer/bet/follow", this.node).getComponent(cc.Button);
        this.button_follow.node.on('click', this.onClickBetType, this);
        this.button_add = cc.find("betContainer/bet/add", this.node).getComponent(cc.Button);
        this.button_add.node.on('click', this.onClickBetType, this);

        this.bottomContainer = cc.find("betContainer/bottom", this.node);

        UIManager.Instance.showPlayerInfoNode(true);
    }

    start() {
        EventDispatch.register(EventType.ROOM_UPDATE_ROUND_INDEX, this.updateRoundIndex, null);
        EventDispatch.register(EventType.ROOM_TURN_TO_BET, this.toBet, null);
        EventDispatch.register(EventType.ROOM_BET_SUCCESS, this.betSuccess, null);
        this.initUI();
    }

    initUI() {
        this.roomId.string = String(PlayerManager.getInstance().mMy.getRoomId());
        this.updateRoundIndex();
        this.shineBetSide();
        this.bet_value.string = String(PlayerManager.getInstance().mMy.getBetValue());
        this.bottomContainer.active = false;
        let state = PlayerManager.getInstance().mMy.getGameState();
        if (state == GameState.Bet) {
            this.showTimer(true);
        }
    }

    onClickBetSide(button) {
        if (button == this.button_blue_bet) {
            PlayerManager.getInstance().mMy.updateBetSide(Side.Side_Blue);
        } else if (button == this.button_red_bet) {
            PlayerManager.getInstance().mMy.updateBetSide(Side.Side_Red);
        }
        this.shineBetSide();
    }

    toBet() {
        console.log("toBet");
        PlayerManager.getInstance().mMy.updateGameState(GameState.Bet);
        PlayerManager.getInstance().mMy.updateBetSide(Side.Side_Idle);
        PlayerManager.getInstance().mMy.updateBetValue(0);
        this.shineBetSide();
        this.bet_value.string = "0";
        this.bottomContainer.active = false;
    }

    onClickBetType(button) {
        if (PlayerManager.getInstance().mMy.getGameState() != GameState.Bet) {
            console.log("Not your turn, please waiting other player.");
            return;
        }
        if (PlayerManager.getInstance().mMy.getBetSide() == Side.Side_Idle) {
            console.log("Please select side first!");
            return;
        }
        this.bottomContainer.active = (button == this.button_add);
        let playerInfo: Player = PlayerManager.getInstance().mMy;
        if (button == this.button_all) {
            let roomId = playerInfo.getRoomId();
            let roundIndex = playerInfo.getRoomRoundIndex();
            let betSide = playerInfo.getBetSide() == Side.Side_Blue ? 1 : 2;
            GameMessageHandler.getInstance().sendC2GSBet(roomId, roundIndex, betSide, playerInfo.coin);
        } else if (button == this.button_follow) {
        } else if (button == this.button_add) {
        }
    }

    shineBetSide() {
        let betSide: Side = PlayerManager.getInstance().mMy.getBetSide();
        this.blue_bet_bar_shine.node.active = (betSide == Side.Side_Blue);
        this.red_bet_bar_shine.node.active = (betSide == Side.Side_Red);
    }

    showTimer(show: boolean) {

    }

    betSuccess() {
        PlayerManager.getInstance().mMy.updateBetSide(Side.Side_Idle);
        this.shineBetSide();
        this.showTimer(false);
        this.bottomContainer.active = false;
    }

    updateRoundIndex() {
        let index = PlayerManager.getInstance().mMy.getRoomRoundIndex();
        this.roundIndex.string = this.getChineseWords(index);
    }

    getChineseWords(index) {
        let text = String(index);
        let chWords = "";
        for (let i = 0; i < text.length; i++) {
            if (text[i] == '1') {
                chWords += "一";
            } else if (text[i] == '2') {
                chWords += "二";
            } else if (text[i] == '3') {
                chWords += "三";
            } else if (text[i] == '4') {
                chWords += "四";
            } else if (text[i] == '5') {
                chWords += "五";
            } else if (text[i] == '6') {
                chWords += "六";
            } else if (text[i] == '7') {
                chWords += "七";
            } else if (text[i] == '8') {
                chWords += "八";
            } else if (text[i] == '9') {
                chWords += "九";
            }
        }
        return chWords;
    }


    // update (dt) {}
}

