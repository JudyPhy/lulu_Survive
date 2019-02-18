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

import { NetManager } from "./../net/NetManager"
import { MessageID } from "./../net/messageID"
import { pb } from "./../pbProc/pb"

@ccclass
export class Login extends cc.Component {

    private userEdit: cc.EditBox = null;

    private passwordEdit: cc.EditBox = null;

    private buttonLogin: cc.Button = null;

    onLoad() {
        this.userEdit = cc.find("Container/User", this.node).getComponent(cc.EditBox);
        this.passwordEdit = cc.find("Container/Password", this.node).getComponent(cc.EditBox);
        this.buttonLogin = cc.find("Container/ButtonLogin", this.node).getComponent(cc.Button);
        this.buttonLogin.node.on('click', this.onClickLogin, this);
    }

    start() {

    }

    onClickLogin(button) {
        console.log("onClickLogin");
        // let msg = pb.C2GSLogin.create({ user: this.userEdit.string, password: this.passwordEdit.string });
        let msg = pb.C2GSLogin.create({ user: "lulu", password: "123456" });
        let buffer = pb.C2GSLogin.encode(msg).finish();
        NetManager.Instance.SendToGS(MessageID.MSG_C2GS_LOGIN, buffer);
    }

    update(dt) {

    }
}
