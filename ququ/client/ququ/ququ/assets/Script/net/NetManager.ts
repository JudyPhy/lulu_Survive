import {EventDispatch} from "./../handler/EventDispatch"

export class NetManager {

    private CONNECT_SUCCESS: string="NET.CONNECT_SUCCESS";

    private static instance: NetManager = null;
    public static getInstance(): NetManager {
        if (this.instance == null) {
            this.instance = new NetManager();
        }
        return this.instance;
    }

    public connectGS(url: String) {
        let self = this;
        let ws = new WebSocket("ws://localhost:9000");
        ws.onopen = function () {
            alert("connect success:");
            EventDispatch.fire(self.CONNECT_SUCCESS,"connect");
        };

        ws.onmessage = function (evt) {
            var received_msg = evt.data;
            alert("recieve meddage:" + received_msg);
            EventDispatch.fire(self.CONNECT_SUCCESS,"connect");
        };

        ws.onclose = function () {
            alert("close ws");
        };

        ws.onerror = function () {
            alert("ws error");
        };
    }





}
