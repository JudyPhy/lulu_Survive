// Learn TypeScript:
//  - [Chinese] http://docs.cocos.com/creator/manual/zh/scripting/typescript.html
//  - [English] http://www.cocos2d-x.org/docs/creator/manual/en/scripting/typescript.html
// Learn Attribute:
//  - [Chinese] http://docs.cocos.com/creator/manual/zh/scripting/reference/attributes.html
//  - [English] http://www.cocos2d-x.org/docs/creator/manual/en/scripting/reference/attributes.html
// Learn life-cycle callbacks:
//  - [Chinese] http://docs.cocos.com/creator/manual/zh/scripting/life-cycle-callbacks.html
//  - [English] http://www.cocos2d-x.org/docs/creator/manual/en/scripting/life-cycle-callbacks.html

const { ccclass, property } = cc._decorator;

import { ResManager, WindowId } from "./windowDefine"
import { MyTool } from "./../myTool/myTool"

@ccclass
export class UIManager extends cc.Component {

    public static Instance: UIManager;

    private curWindowId: WindowId;
    private curWindow: cc.Node = null;

    onLoad() {
        UIManager.Instance = this;
        this.curWindowId = WindowId.Init;
        this.curWindow = cc.find("init", this.node);
    }

    start() {
    }

    public showWindow(windowId: WindowId) {
        if (this.curWindowId == windowId) {
            console.log("window[" + windowId + "] has been shown.")
            return;
        }
        let prefabPath = ResManager.getPrefabPath(windowId);
        if (prefabPath != '') {
            let window = MyTool.AddChild(this.node, prefabPath);
            if (this.curWindow != null) {
                this.curWindow.destroy();
            }
            this.curWindow = window;
        }
    }

    update(dt) {
    }

}
