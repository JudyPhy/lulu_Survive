import { pb } from "../pbproc/pb";
import { Player } from "./player";

export class PlayerManager {

    private static instance: PlayerManager = null;
    public static getInstance(): PlayerManager {
        if (PlayerManager.instance == null) {
            PlayerManager.instance = new PlayerManager();
        }
        return PlayerManager.instance;
    }

    public mMy: Player = new Player();

    initMy(info: pb.IPlayerInfo) {
        this.mMy.newPlayer(info);
    }

}
