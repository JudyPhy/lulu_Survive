import { pb } from "./../pbProc/pb"

export class PlayerData {

    public mId: any = 0;
    public nickname: string = "";
    public headicon: string = "";
    public card: any = 0;
    public coin: any = 0;  

    initPlayer(playerinfo: pb.IPlayerInfo) {
        this.mId = playerinfo.playerId;
        this.nickname = playerinfo.nickname;
        this.headicon = playerinfo.headicon;
        this.card = playerinfo.card;
        this.coin = playerinfo.coin;
    }

}
