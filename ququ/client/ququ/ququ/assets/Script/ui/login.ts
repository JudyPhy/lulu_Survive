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

@ccclass
export class Login extends cc.Component {

    @property(cc.EditBox)
    userEdit: cc.EditBox = null;

    @property(cc.EditBox)
    passwordEdit: cc.EditBox = null;

    @property(cc.Button)
    buttonLogin: cc.Button = null;

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
    }

    update(dt) {
        
    }
}
