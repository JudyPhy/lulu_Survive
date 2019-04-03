import { EventDispatch } from "../handler/EventDispatch";
import { EventType } from "../handler/EventType";
import { PlayerManager } from "../player/playerManager";
import { UIManager } from "../uimanager/uimanager";
import { GameState } from "../player/roomData";
import { pb } from "../pbproc/pb";
import { GameMessageHandler } from "./gameMessageHandler";
import { RoomData } from "../player/roomData";
import { PlayerData } from "../player/playerData";

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
        this.bottomContainer.active = false;

        UIManager.Instance.showPlayerInfoNode(true);
    }

    registerEvent() {
        EventDispatch.register(EventType.ROOM_TURN_TO_BET, this.toBet, this);
        EventDispatch.register(EventType.ROOM_BET_SUCCESS, this.betSuccess, this);
        EventDispatch.register(EventType.ROOM_UPDATE_BET_INFO, this.updateBarValue, this);
        EventDispatch.register(EventType.ROOM_ROUND_OVER, this.roundOver, this);
        EventDispatch.register(EventType.ROOM_UPDATE_ROUND_INDEX, this.initUI, this);
    }

    start() {
        this.registerEvent();
        this.initUI();
    }

    initUI() {
        this.roomId.string = String(PlayerManager.getInstance().mMyRoom.roomId);
        this.updateRoundIndex();
        this.shineBetSide();
        this.updateBarValue();
        this.bet_value.string = String(PlayerManager.getInstance().mMyRoom.getBetValueByPlayerId(PlayerManager.getInstance().mMy.mId));
        this.bottomContainer.active = false;
        let state = PlayerManager.getInstance().mMyRoom.gameState;
        if (state == GameState.Bet) {
            this.toBet();
        }
    }

    onClickBetSide(button) {
        if (button == this.button_blue_bet) {
            PlayerManager.getInstance().mMyRoom.updateBetSide(PlayerManager.getInstance().mMy.mId, pb.Side.BLUE);
        } else if (button == this.button_red_bet) {
            PlayerManager.getInstance().mMyRoom.updateBetSide(PlayerManager.getInstance().mMy.mId, pb.Side.RED);
        }
        this.shineBetSide();
    }

    onClickBetType(button) {
        this.bottomContainer.active = (button == this.button_add);
        let playerInfo: PlayerData = PlayerManager.getInstance().mMy;
        let roomInfo: RoomData = PlayerManager.getInstance().mMyRoom;
        if (roomInfo.gameState != GameState.Bet) {
            console.log("Not your turn, please waiting other player.");
            return;
        }
        let betSide = roomInfo.getBetSide(playerInfo.mId);
        if (betSide == null) {
            console.log("Please select side first!");
            return;
        }
        let betValue = 0;
        if (button == this.button_all) {
            if (playerInfo.coin < 200) {
                console.log("Coin not enough!");
                return;
            }
            betValue = playerInfo.coin;
        } else if (button == this.button_follow) {
            let cost = roomInfo.getBetValueBySide(betSide);
            if (cost <= 0) {
                console.error("Has no this side bet!");
                return;
            }
            if (playerInfo.coin < cost) {
                console.error("Coin not enough!");
                return;
            }
            betValue = cost;
        } else if (button == this.button_add) {
            return;
        }
        roomInfo.updateBetValue(playerInfo.mId, betValue);
        roomInfo.gameState = GameState.BetOver;
        GameMessageHandler.getInstance().sendC2GSBet(roomInfo.roomId, roomInfo.roundIndex, betSide, betValue);
    }

    toBet() {
        this.showTimer(true);
    }

    showTimer(show: boolean) {

    }

    shineBetSide() {
        let betSide: pb.Side = PlayerManager.getInstance().mMyRoom.getBetSide(PlayerManager.getInstance().mMy.mId);
        this.blue_bet_bar_shine.node.active = (betSide == pb.Side.BLUE);
        this.red_bet_bar_shine.node.active = (betSide == pb.Side.RED);
    }

    updateBarValue() {
        let roomInfo = PlayerManager.getInstance().mMyRoom;
        if (roomInfo) {
            this.blue_bet_value.string = String(roomInfo.getBetValueBySide(pb.Side.BLUE));
            this.red_bet_value.string = String(roomInfo.getBetValueBySide(pb.Side.RED));
        } else {
            this.blue_bet_value.string = "";
            this.red_bet_value.string = "";
        }
    }

    betSuccess() {
        this.bottomContainer.active = false;
        let playerInfo = PlayerManager.getInstance().mMy;
        this.bet_value.string = String(PlayerManager.getInstance().mMyRoom.getBetValueByPlayerId(playerInfo.mId));
    }

    roundOver() {
        this.shineBetSide();
        this.bet_value.string = "0";
    }

    updateRoundIndex() {
        let index = PlayerManager.getInstance().mMyRoom.roundIndex;
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

