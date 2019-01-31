import { EventDispatch } from "./../handler/EventDispatch"
import { EventType } from "./../handler/EventType"


export class NetManager {

    private ws: WebSocket = null;

    private static instance: NetManager = null;
    public static getInstance(): NetManager {
        if (this.instance == null) {
            this.instance = new NetManager();
        }
        return this.instance;
    }

    public connectGS(url: string) {
        console.log("connectGS url=" + url);
        let self = this;
        self.ws = new WebSocket(url);
        self.ws.onopen = function () {
            console.log("connect success:");
            EventDispatch.fire(EventType.NET_CONNECT_SUCCESS, "connect");
        };

        self.ws.onmessage = function (evt) {
            var received_msg = evt.data;
            console.log("recieve message:" + received_msg);
            EventDispatch.fire(EventType.NET_CONNECT_SUCCESS, "connect");
        };

        self.ws.onclose = function () {
            console.log("close ws");
        };

        self.ws.onerror = function () {
            console.log("ws error");
        };
    }

    public getConnectState(): number {
        if (this.ws != null) {
            return this.ws.readyState;
        }
        return -1;
    }

}
