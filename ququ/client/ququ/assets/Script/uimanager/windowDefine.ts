export enum WindowId {
    Init = "init",
    Login = "login",
    PlayerInfo = "playerInfo",
    Hall = "hall",
    Room = "room"
}

export class ResManager {

    public static getPrefabPath(windowId: WindowId) {
        switch (windowId) {
            case WindowId.Login:
                return "Prefabs/login";
            case WindowId.Hall:
                return "Prefabs/hall";
            case WindowId.Room:
                return "Prefabs/room";
            case WindowId.PlayerInfo:
                return "Prefabs/playerInfo";
            default:
                return '';
        }
    }
}
