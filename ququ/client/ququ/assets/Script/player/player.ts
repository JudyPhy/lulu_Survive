import { pb } from "./../pbProc/pb"

export enum GameState {
    Idle,
    WaitingBet,
    Bet,
    BetOver,
    Over
}

export enum Side {
    Side_Idle = 0,
    Side_Blue = 1,
    Side_Red = 2,
}

export class Player {

    public mId: number | Long = 0;
    public nickname: string = "";
    public headicon: string = "";
    public card: number | Long = 0;
    public coin: number | Long = 0;

    private roomId: number | Long;
    private roundIndex: number;
    private gameState: GameState;
    private betSide: Side = Side.Side_Idle;
    private betValue: number | Long = 0;

    newPlayer(playerinfo: pb.IPlayerInfo) {
        this.mId = playerinfo.playerId;
        this.nickname = playerinfo.nickname;
        this.headicon = playerinfo.headicon;
        this.card = playerinfo.card;
        this.coin = playerinfo.coin;
    }

    updateRoomId(rId: number | Long) {
        this.roomId = rId;
    }

    getRoomId(): number | Long {
        return this.roomId;
    }

    updateRoomRoundIndex(index: number) {
        this.roundIndex = index;
    }

    getRoomRoundIndex(): number {
        return this.roundIndex;
    }

    updateGameState(state: GameState) {
        this.gameState = state;
    }

    getGameState() {
        return this.gameState;
    }

    updateBetSide(side: Side) {
        this.betSide = side;
    }

    getBetSide() {
        return this.betSide;
    }

    updateBetValue(value: number | Long) {
        this.betValue = value;
    }

    getBetValue() {
        return this.betValue;
    }

}
