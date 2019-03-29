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
import { PlayerInfoNode } from "../ui/playerInfo";

@ccclass
export class UIManager extends cc.Component {

    public static Instance: UIManager;

    private curWindowId: WindowId;

    onLoad() {
        UIManager.Instance = this;
        this.curWindowId = WindowId.Init;
        this.initPlayerInfo();
    }

    initPlayerInfo() {
        let prefabPath = ResManager.getPrefabPath(WindowId.PlayerInfo);
        if (prefabPath != '') {
            MyTool.AddChild(this.node, prefabPath);
        }
    }

    start() {
    }

    public showWindow(windowId: WindowId) {
        console.log("showWindow[" + windowId + "]");
        if (this.curWindowId == windowId) {
            console.log("window[" + windowId + "] has been shown.")
            return;
        }
        let prefabPath = ResManager.getPrefabPath(windowId);
        if (prefabPath != '') {
            MyTool.AddChild(this.node, prefabPath);
            let curWindow = cc.find(this.curWindowId, this.node);
            if (curWindow != null) {
                curWindow.destroy();
            } else {
                console.error("curWindow[", this.curWindowId, "] can't find");
            }
            this.curWindowId = windowId;
        }
    }

    public showPlayerInfoNode(show: boolean) {
        console.log("showPlayerInfoNode:", show);
        PlayerInfoNode.Instance.node.active = show;
        PlayerInfoNode.Instance.node.zIndex = show ? 1 : 0;
        if (show) {
            PlayerInfoNode.Instance.initUI();
        }
    }

    update(dt) {
    }

}
