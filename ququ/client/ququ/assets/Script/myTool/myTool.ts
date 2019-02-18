export class MyTool {

    public static AddChild(parent: cc.Node, resUrl: string): cc.Node {
        let obj = null;
        cc.loader.loadRes(resUrl, function (error, prefab) {
            if (error) {
                console.log("载入资源失败, 原因:" + error);
                return;
            }
            if (!(prefab instanceof cc.Prefab)) {
                console.log('载入的不是预制资源!');
                return;
            }
            obj = cc.instantiate(prefab);
            parent.addChild(obj);
        });
        return obj;
    }
}
