import { pb } from "./../pbProc/pb"

export class Player {

    public mId: number | Long;
    public nickname: string;
    public headicon: string;
    public card: number | Long;
    public coin: number | Long;

    newPlayer(playerinfo: pb.IPlayerInfo) {
        this.mId = playerinfo.playerId;
        this.nickname = playerinfo.nickname;
        this.headicon = playerinfo.headicon;
        this.card = playerinfo.card;
        this.coin = playerinfo.coin;
    }

}
