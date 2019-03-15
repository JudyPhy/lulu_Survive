export enum WindowId {
    Init = "Init",
    Login = "Login",
    Hall = "Hall"
}

export class ResManager {

    public static getPrefabPath(windowId: WindowId) {
        switch (windowId) {
            case WindowId.Login:
                return "Prefabs/login";
            case WindowId.Hall:
                return "Prefabs/hall";
            default:
                return '';
        }
    }
}
