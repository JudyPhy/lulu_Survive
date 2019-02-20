export enum WindowId {
    Init = "Init",
    Login = "Login",
    Home = "Prefabs/home"
}

export class ResManager {

    public static getPrefabPath(windowId: WindowId) {
        switch (windowId) {
            case WindowId.Login:
                return "Prefabs/login";
            case WindowId.Home:
                return "Prefabs/home";
            default:
                return '';
        }
    }
}
