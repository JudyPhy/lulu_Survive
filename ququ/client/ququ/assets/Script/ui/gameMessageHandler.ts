import { NetManager } from "./../net/NetManager"
import { MessageID } from "./../net/messageID"
import { pb } from "./../pbProc/pb"
import { PlayerManager } from "./../player/playerManager"
import { UIManager } from "../uimanager/uimanager";
import { WindowId } from "../uimanager/windowDefine";
import { EventDispatch } from "../handler/EventDispatch";
import { EventType } from "../handler/EventType";
import { GameState, Side } from "../player/player";
import { PlayerInfoNode } from "./playerInfo";

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
        console.log("RecieveGS2CLoginRet:", data.user, data.errorCode);
        if (data.errorCode == pb.GS2CLoginRet.ErrorCode.Success) {
            PlayerManager.getInstance().initMy(data.user);
            UIManager.Instance.showWindow(WindowId.Hall);
        } else {
            console.error("login failed");
        }
    }

    public SendC2GSEnterRoom(room: number, psw: number) {
        let msg = pb.C2GSEnterRoom.create({ roomId: room, password: psw });
        let buffer = pb.C2GSEnterRoom.encode(msg).finish();
        NetManager.Instance.SendToGS(MessageID.MSG_C2GS_ENTERROOM, buffer);
    }

    public RecieveGS2CEnterRoomRet(buffer: Uint8Array) {
        let data = pb.GS2CEnterRoomRet.decode(buffer);
        console.log("RecieveGS2CEnterRoomRet:", data.roomId, data.errorCode);
        if (data.errorCode == pb.GS2CEnterRoomRet.ErrorCode.Success) {
            PlayerManager.getInstance().mMy.updateRoomId(data.roomId);
            PlayerManager.getInstance().mMy.updateRoomRoundIndex(data.rountIndex);
            PlayerManager.getInstance().mMy.updateGameState(GameState.Idle);
            UIManager.Instance.showWindow(WindowId.Room);
        } else if (data.errorCode == pb.GS2CEnterRoomRet.ErrorCode.GameStart) {
            console.error("game start");
        } else if (data.errorCode == pb.GS2CEnterRoomRet.ErrorCode.NeedPassword) {

        } else {
            console.error("fail");
        }
    }

    public RecieveGS2CNewRoundStart(buffer: Uint8Array) {
        let data = pb.GS2CNewRoundStart.decode(buffer);
        console.log("RecieveGS2CNewRoundStart:", data.rountIndex);
        PlayerManager.getInstance().mMy.updateRoomRoundIndex(data.rountIndex);
        EventDispatch.fire(EventType.ROOM_UPDATE_ROUND_INDEX);
    }

    public RecieveGS2CTurnToBet(buffer: Uint8Array) {
        console.log("RecieveGS2CTurnToBet");        
        EventDispatch.fire(EventType.ROOM_TURN_TO_BET);
    }

    public sendC2GSBet(roomId: number | Long, roundIndex: number, betSide: number, value: number | Long) {
        let msg = pb.C2GSBet.create({ roomId: roomId, rountIndex: roundIndex, betSide: betSide, bet: value });
        let buffer = pb.C2GSBet.encode(msg).finish();
        NetManager.Instance.SendToGS(MessageID.MSG_C2GS_BET, buffer);
    }

    public RecieveGS2CBetRet(buffer: Uint8Array) {
        let data = pb.GS2CBetRet.decode(buffer);
        console.log("RecieveGS2CBetRet:", data.errorCode);
        if (data.errorCode == pb.GS2CBetRet.ErrorCode.Fail || data.errorCode == pb.GS2CBetRet.ErrorCode.CoinLess
            || data.errorCode == pb.GS2CBetRet.ErrorCode.IndexError) {
            if (data.errorCode == pb.GS2CBetRet.ErrorCode.Fail) {
                console.log("bet fail");
            } else if (data.errorCode == pb.GS2CBetRet.ErrorCode.CoinLess) {
                console.log("bet coin error");
            } else if (data.errorCode == pb.GS2CBetRet.ErrorCode.IndexError) {
                console.log("bet round index error");
            }
            PlayerManager.getInstance().mMy.updateBetValue(0);
            EventDispatch.fire(EventType.ROOM_TURN_TO_BET);
        } else if (data.errorCode == pb.GS2CBetRet.ErrorCode.Success) {
            PlayerManager.getInstance().mMy.updateGameState(GameState.BetOver);
            EventDispatch.fire(EventType.ROOM_BET_SUCCESS);
        }
    }


    public sendC2GSGMAddCoin(value: number) {
        let msg = pb.C2GSGMAddCoin.create({ value: value });
        let buffer = pb.C2GSGMAddCoin.encode(msg).finish();
        NetManager.Instance.SendToGS(MessageID.MSG_C2GS_GM_ADD_COIN, buffer);
    }

    public RecieveGS2CGMAddCoinRet(buffer: Uint8Array) {
        let data = pb.GS2CGMAddCoinRet.decode(buffer);
        PlayerManager.getInstance().mMy.coin = data.user.coin;
        PlayerInfoNode.Instance.initUI();
        console.log("RecieveGS2CGMAddCoinRet: coin=", data.user.coin);
    }

}
