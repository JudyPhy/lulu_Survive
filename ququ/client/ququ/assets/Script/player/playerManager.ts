import { pb } from "../pbproc/pb";
import { PlayerData } from "./playerData";
import { RoomData, GameState, RoomPlayer } from "./roomData";

export class PlayerManager {

    private static instance: PlayerManager = null;
    public static getInstance(): PlayerManager {
        if (PlayerManager.instance == null) {
            PlayerManager.instance = new PlayerManager();
        }
        return PlayerManager.instance;
    }

    public mMy: PlayerData = new PlayerData();

    public mMyRoom: RoomData = null;

    newRoom(roomId: number | Long, roundIndex: number, players: pb.IPlayerInfo[]) {
        this.mMyRoom = new RoomData();
        this.mMyRoom.roomId = roomId;
        this.mMyRoom.roundIndex = roundIndex;
        this.mMyRoom.players = [];
        for (let i = 0; i < players.length; i++) {
            let p = new RoomPlayer();
            p.player = players[i];
            p.betValue = 0;
            this.mMyRoom.players.push(p);
        }
        this.mMyRoom.gameState = GameState.Idle;
    }

}
