import { EventDispatch } from "./../handler/EventDispatch"
import { EventType } from "./../handler/EventType"
import { pb } from "../pbproc/pb";


export class Network {

    private ws: WebSocket = null;

    private headPackageSize: number = 4;

    private msgIncompleteBuffer: Uint8Array = null;

    public RevMessageArray: Array<WsMessage> = new Array<WsMessage>();

    public connectGS(url: string) {
        console.log("connectGS url=" + url);
        let self = this;
        self.ws = new WebSocket(url);
        self.ws.onopen = function () {
            console.log("connect success");
            EventDispatch.fire(EventType.NET_CONNECT_SUCCESS);
        };

        self.ws.onmessage = function (evt) {
            let msg = self.splitMessage(evt);
            // EventDispatch.fire(EventType.NET_CONNECT_SUCCESS);
        };

        self.ws.onclose = function () {
            console.log("close ws");
        };

        self.ws.onerror = function () {
            console.log("ws error");
        };
    }

    splitMessage(obj: MessageEvent) {
        let arrayBuffer: ArrayBuffer = obj.data;
        let dataUnit8Array = new Uint8Array(arrayBuffer);   //接收源数据

        let msgBuffer: Uint8Array = null;
        if (this.msgIncompleteBuffer == null) {
            msgBuffer = dataUnit8Array;
        }
        else {
            let completeBuffer = new Uint8Array(this.msgIncompleteBuffer.length + dataUnit8Array.length);
            completeBuffer.set(this.msgIncompleteBuffer, 0);
            completeBuffer.set(dataUnit8Array, this.msgIncompleteBuffer.length);
            this.msgIncompleteBuffer = null;
            msgBuffer = completeBuffer;
        }

        let curPos: number = 0; // split package pos
        while (true) {
            if (curPos == msgBuffer.length)
                break;
            //包头长度不符，等待下一个包
            if (msgBuffer.length - curPos <= this.headPackageSize) {
                let incompleteBuffer = new Uint8Array(msgBuffer.length - curPos);    //临时缓冲区
                incompleteBuffer.set(msgBuffer.subarray(curPos), 0);
                this.msgIncompleteBuffer = incompleteBuffer;    //存储没有处理的消息
                break;
            }
            //消息长度不符，等待下一个包
            let lenArray = msgBuffer.subarray(2, 4);  //包头后2个字节是总长度（id+proto消息）
            let sizePackage = this.Uint8ArrayToInt(lenArray) + 2;
            if (sizePackage > msgBuffer.length - curPos) {
                let incompleteBuffer = new Uint8Array(msgBuffer.length - curPos);    //临时缓冲区
                incompleteBuffer.set(msgBuffer.subarray(curPos), 0);
                this.msgIncompleteBuffer = incompleteBuffer; //存储没有处理的消息
                break;
            }
            //消息完整，取出解析
            let idArray = msgBuffer.subarray(curPos, curPos + 2);  //包头前2个字节是消息ID
            let msgId = this.Uint8ArrayToInt(idArray);  //id
            console.log("receive message id = " + msgId);
            let protoBuffer = new Uint8Array(sizePackage - this.headPackageSize);   //msg
            protoBuffer.set(msgBuffer.subarray(curPos + this.headPackageSize, curPos + sizePackage), 0);
            console.log("receive message data = " + protoBuffer);
            this.BuildMessage(protoBuffer, msgId);
            curPos += sizePackage;
        }
    }

    //拆出一个包转为消息存储
    private BuildMessage(buffer: Uint8Array, msgId: number) {
        let wsMsg = new WsMessage();
        wsMsg.pid = msgId;
        wsMsg.buffer = buffer;
        this.RevMessageArray.push(wsMsg);
    }

    /**
     * Uint8Array[]转int
     * 相当于二进制加上4位。同时，使用|=号拼接数据，将其还原成最终的int数据
     * @param uint8Ary Uint8Array类型数组
     * @return int数字
     */
    Uint8ArrayToInt(uint8Ary: Uint8Array) {
        let retInt: number = 0;
        for (let i = 0; i < uint8Ary.length; i++)
            retInt |= (uint8Ary[i] << (8 * (uint8Ary.length - i - 1)));

        return retInt;
    }

    public getConnectState(): number {
        if (this.ws != null) {
            return this.ws.readyState;
        }
        return -1;
    }

    public IsConnected(): boolean {
        return this.ws.readyState == 1;
    }

    private IntToUint8Array(id: number, Bits: number): number[] {
        let resArry = [];
        let binaryStr: string = id.toString(2);  //转为二进制字符串
        for (let i = 0; i < binaryStr.length; i++)
            resArry.push(parseInt(binaryStr[i]));
        if (Bits) {
            for (let r = resArry.length; r < Bits; r++) {
                resArry.unshift(0); //不足位数则在开头补0
            }
        }

        let xresArry = [];
        let resArryStr = resArry.join("");
        for (let j = 0; j < Bits; j += 8)
            xresArry.push(parseInt(resArryStr.slice(j, j + 8), 2));
        return xresArry;
    }

    public Send(id: number, buffer: Uint8Array) {
        // 1 step: set send message
        let addPackageHead_buffer = new Uint8Array(buffer.length + 4);
        let idBinary = this.IntToUint8Array(id, 16);
        let idUnit8 = new Uint8Array(idBinary);
        addPackageHead_buffer.set(idUnit8, 0);
        // console.log("id:", idUnit8)

        let lenBinary = this.IntToUint8Array(buffer.length + 2, 16);
        let lenUnit8 = new Uint8Array(lenBinary);
        addPackageHead_buffer.set(lenUnit8, 2);
        // console.log("length:", lenUnit8)

        addPackageHead_buffer.set(buffer.subarray(0, buffer.length), 4);

        // 2 step: send message
        console.log("send ws");
        this.ws.send(addPackageHead_buffer.buffer);
    }

}

class WsMessage {
    public pid: number;
    public buffer: Uint8Array;
} 
