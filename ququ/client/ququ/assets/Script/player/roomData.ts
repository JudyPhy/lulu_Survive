import { pb } from "../pbproc/pb";

export enum GameState {
    Idle,
    Bet,
    BetOver,
    Over
}

export class RoomPlayer {
    public player: pb.IPlayerInfo = null;
    public betSide: pb.Side = null;
    public betValue: any = 0;
}

export class RoomData {

    public roomId: any;
    public roundIndex: number;
    public gameState: GameState;
    public players: RoomPlayer[];
    public result: boolean;

    getBetSide(playerId: any): pb.Side {
        for (let i = 0; i < this.players.length; i++) {
            if (this.players[i].player.playerId == playerId) {
                return this.players[i].betSide;
            }
        }
        return null;
    }

    updateBetSide(playerId: any, side: pb.Side) {
        for (let i = 0; i < this.players.length; i++) {
            if (this.players[i].player.playerId == playerId) {
                this.players[i].betSide = side;
            }
        }
    }

    getBetValueBySide(side: pb.Side): number {
        let sum: number = 0;
        for (let i = 0; i < this.players.length; i++) {
            if (this.players[i].betSide == side) {
                sum += this.players[i].betValue;
            }
        }
        return sum;
    }

    getBetValueByPlayerId(playerId: any): number {
        for (let i = 0; i < this.players.length; i++) {
            if (this.players[i].player.playerId == playerId) {
                return this.players[i].betValue;
            }
        }
        return 0;
    }

    updateBetValue(playerId: any, value: any) {
        for (let i = 0; i < this.players.length; i++) {
            if (this.players[i].player.playerId == playerId) {
                this.players[i].betValue = value;
            }
        }
    }

    roundOver(result: boolean) {
        this.gameState = GameState.Over;
        this.result = result;
        for (let i = 0; i < this.players.length; i++) {
            this.players[i].betSide = null;
            this.players[i].betValue = 0;
        }
    }

    newRound(roundIndex: number) {
        this.gameState = GameState.Idle;
        this.roundIndex = roundIndex;
    }
}
