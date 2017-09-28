using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

// 网络状态
public enum e_SocketState {
    SCK_CLOSED = -1,
    SCK_CONNECTING = 1,
    SCK_CONNECT_SUCCESS = 2,
    SCK_CONNECTED = 3,
    SCK_CONNECT_OUTTIME = 4,        //连接超时
    SCK_CONNECT_FAIL = 5,           //连接失败
    SCK_CONNECT_EXCEPTION = 6,      //连接异常
}

public sealed class TcpNetworkProcessor
{

    //当前TCP进程ID
    public int ID_;
    private string IPAddr_;
    private ushort Port_;
    private int BufferMaxSize = 65535;      //接收/发送缓冲区容量上限(64kb)
    //收发线程
    private Thread SendThread_;
    private Thread RecvThread_;
    //消息队列
    private Queue<Packet> RevQueue_ = new Queue<Packet>();      //接收到的消息队列
    //private Dictionary<int, PacketHandle> MsgHandleDic_ = new Dictionary<int, PacketHandle>();
    private Queue<byte[]> SendQueue_ = new Queue<byte[]>();     //发送队列
    //Socket
    private Socket NativeSocket_;
    //同步锁
    private readonly object SendQueueLocker_ = new object();    //发送消息时的同步锁
    private object socketLock_ = new object();  //套接字连接时的同步锁
    private readonly object RecvQueueLocker_ = new object();    //接收消息同步锁
    //当前连接状态
    public e_SocketState SocketState_;
    //拆包
    public const int PacketHeadSize = 4; //消息包包头,消息长度2字节 + 消息id 2字节
    private byte[] MsgIncompleteBuffer_ = null;     //临时存储区（接收到不完整消息时存储）
    //长连接心跳时刻
    private DateTime RecvHeartTickTime_;
    private int SendHeartTickInterval_ = 3;
    private int HeartTickInterval_ = 5;

    public TcpNetworkProcessor()
    {
        this.SendThread_ = new Thread(new ThreadStart(this.ProcessAsyncSend));
        this.SendThread_.IsBackground = true;
        if (!this.SendThread_.IsAlive)
        {
            this.SendThread_.Start();
        }
    }

    //发送监听线程
    private void ProcessAsyncSend()
    {
        while (true)
        {
            DoSend();
            Thread.Sleep(20);
        }
    }

    //检测Socket连接状态
    public bool IsConnected()
    {
        if (null == this.NativeSocket_)
            return false;
        return this.NativeSocket_.Connected;
    }

    public bool IsNativeSocketNull()
    {
        return this.NativeSocket_ == null;
    }

    private void DoSend()
    {
        if (!this.IsConnected())
        {
            return;
        }
        if (this.SendQueue_.Count > 100)
        {
            Debug.LogError(string.Format("{0}:{1} 积压的消息包过多！", this.IPAddr_, this.Port_));
        }
        try
        {
            lock (this.SendQueueLocker_)
            {
                if (this.SendQueue_.Count == 0)
                {
                    return;
                }
                byte[] sendBuf = this.SendQueue_.Peek();
                if (sendBuf.Length >= this.BufferMaxSize || sendBuf.Length <= 0)
                {
                    Debug.Log("消息包过大！");
                    return;
                }
                int sendBytes = this.NativeSocket_.Send(sendBuf);
                if (sendBytes > 0)
                {
                    this.SendQueue_.Dequeue();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(string.Format("向{0}:{1}发送数据包异常,msg:{2}!", this.IPAddr_, this.Port_, ex.Message));
            DisConnect();
        }
    }

    //清除自己将要发送的消息
    public void ClearWillSendMessage()
    {
        if (this.SendQueue_.Count > 0)
        {
            lock (this.SendQueueLocker_)
            {
                this.SendQueue_.Clear();
            }
        }
    }

    //连接IP端口
    public void Connect(string ip, ushort port)
    {
        this.IPAddr_ = ip;
        this.Port_ = port;
        if (this.NativeSocket_ == null)
        {
            //创建新的socket
            this.NativeSocket_ = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.NativeSocket_.ReceiveBufferSize = this.BufferMaxSize;
            this.NativeSocket_.NoDelay = true;
        }
        else
        {
            //socket已连接，不再连接
            if (this.NativeSocket_.Connected)
            {
                return;
            }
        }
        IPAddress ipAddr = IPAddress.Parse(this.IPAddr_);
        IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, this.Port_);
        IAsyncResult result = this.NativeSocket_.BeginConnect(ipEndPoint, new AsyncCallback(OnConnectResult), this.NativeSocket_); //连接采用异步方式
        //超时
        bool success = result.AsyncWaitHandle.WaitOne(5000, true);
        if (!success)
        {
            Debug.LogError("连接超时");
            this.SocketState_ = e_SocketState.SCK_CONNECT_OUTTIME;
        }
        Debug.Log("启动链接网络 " + this.IPAddr_ + ":" + this.Port_ + "!");
    }

    //异步连接完成后执行
    private void OnConnectResult(IAsyncResult result)
    {
        lock (this.socketLock_)
        {
            try
            {
                if (null == result)
                {
                    return;
                }
                Socket nativeSocket = (Socket)result.AsyncState;
                if (null == nativeSocket)
                {
                    return;
                }
                nativeSocket.EndConnect(result);    //获取到BeginConnect的socket后，调用EndConnect完成连接尝试
                if (nativeSocket.Connected)
                {
                    //连接成功，创建接收线程
                    this.SocketState_ = e_SocketState.SCK_CONNECT_SUCCESS;
                    this.RecvThread_ = new Thread(new ThreadStart(ProcessAsyncRecv));
                    this.RecvThread_.IsBackground = true;
                    this.RecvThread_.Start();
                    Debug.LogError("connect " + this.IPAddr_ + ":" + this.Port_ + " success!");
                }
                else
                {
                    Debug.Log("连接失败");
                    this.SocketState_ = e_SocketState.SCK_CONNECT_FAIL;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("连接异常:" + e.Message);
                this.SocketState_ = e_SocketState.SCK_CONNECT_EXCEPTION;
                DisConnect();
            }
        }
    }

    //连接成功后，处理异步收取数据的线程
    private void ProcessAsyncRecv()
    {
        while (true)
        {
            try
            {
                if (null == this.NativeSocket_)
                {
                    this.SocketState_ = e_SocketState.SCK_CLOSED;
                    break;
                }
                if (!this.NativeSocket_.Connected)
                {
                    this.NativeSocket_.Close();
                    this.SocketState_ = e_SocketState.SCK_CLOSED;
                    break;
                }
                try
                {
                    byte[] buffer = new byte[4096];
                    //ProcessAsyncRecv方法中会一直等待服务端回发消息
                    //如果没有回发会一直在这里等着。
                    int buffSize = this.NativeSocket_.Receive(buffer);
                    if (buffSize <= 0)
                    {
                        DisConnect();
                        break;
                    }
                    if (buffer.Length > 0)
                    {
                        SplitPackage(buffer, buffSize);
                    }
                }
                catch (SocketException ex)
                {
                    Debug.LogError(ex);
                    DisConnect();
                    break;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                DisConnect();
                break;
            }
        }
    }

    //断开Socket连接
    public void DisConnect()
    {
        lock (this.socketLock_)
        {
            try
            {
                if (null != this.NativeSocket_)
                {
                    this.NativeSocket_.Close();
                    this.NativeSocket_ = null;
                }
                this.RecvThread_ = null;
            }
            catch
            {
                this.RecvThread_ = null;
                this.NativeSocket_ = null;
            }
            this.SocketState_ = e_SocketState.SCK_CLOSED;
        }
    }

    //拆包
    private void SplitPackage(byte[] buffer, int msgLength)
    {
        byte[] msgBuffer_ = null;
        if (this.MsgIncompleteBuffer_ == null)
        {
            msgBuffer_ = buffer;

            /*string str = "recv msg:";
            for (int i = 0; i < msgBuffer_.Length; i++)
            {
                str += msgBuffer_[i] + ", ";
            }
            Debug.LogError(str);*/
        }
        else
        {
            byte[] completeBuffer = new byte[this.MsgIncompleteBuffer_.Length + msgLength]; //接上一次拆包剩余消息
            Buffer.BlockCopy(this.MsgIncompleteBuffer_, 0, completeBuffer, 0, this.MsgIncompleteBuffer_.Length);
            Buffer.BlockCopy(buffer, 0, completeBuffer, this.MsgIncompleteBuffer_.Length, msgLength);
            this.MsgIncompleteBuffer_ = null;
            msgLength = completeBuffer.Length;
            msgBuffer_ = completeBuffer;
        }
        int curPos = 0; // split package pos
        while (true)
        {
            if (curPos == msgLength)
                break;
            //剩余未拆包消息长度小于包头，说明没接收完，等待下一段消息
            if (msgLength - curPos <= PacketHeadSize)
            {
                int remainByteCount = msgLength - curPos;
                byte[] incompleteBuffer = new byte[remainByteCount];    //临时缓冲区
                Buffer.BlockCopy(msgBuffer_, curPos, incompleteBuffer, 0, remainByteCount);
                this.MsgIncompleteBuffer_ = incompleteBuffer;    //存储没有处理的消息
                break;
            }
            //剩余未拆包消息大于包头，说明没接收完，等待后面消息到来
            byte[] array = { msgBuffer_[0], msgBuffer_[1] };  //包头前2个字节是（proto消息+id）总长度
            int sizePackage = ConvertByteArrayToInt(array) + 2;
            if (sizePackage > msgLength - curPos)
            {
                int remainByteCount = msgLength - curPos;
                byte[] incompleteBuffer = new byte[remainByteCount];
                Buffer.BlockCopy(msgBuffer_, curPos, incompleteBuffer, 0, remainByteCount);
                this.MsgIncompleteBuffer_ = incompleteBuffer;
                break;
            }
                        
            byte[] array2 = { msgBuffer_[2], msgBuffer_[3] };  //包头后2个字节是消息ID
            int pid = ConvertByteArrayToInt(array2);

            byte[] protobufMsg = new byte[sizePackage];
            Buffer.BlockCopy(msgBuffer_, curPos, protobufMsg, 0, sizePackage);
            int protoBuffSize = sizePackage - PacketHeadSize;
            byte[] protoBuffer = new byte[protoBuffSize];
            Buffer.BlockCopy(protobufMsg, PacketHeadSize, protoBuffer, 0, protoBuffSize);
            BuildMessage(protoBuffer, protoBuffSize, pid);
            curPos += sizePackage;
        }
    }

    //go消息 字节数组byte[] 转 整型int
    private int ConvertByteArrayToInt(byte[] arry)
    {
        string sumStr = "";
        for (int i = 0; i < arry.Length; i++)
        {
            string str = Convert.ToString(arry[i], 2);
            while (str.Length < 8)
            {
                str = "0" + str;
            }
            sumStr += str;
        }
        return Convert.ToInt32(sumStr, 2);
    }

    private void ConvertIntToByteArray(Int32 m)
    {
        byte[] arry = new byte[4];
        arry[0] = (byte)(m & 0xFF);
        arry[1] = (byte)((m & 0xFF00) >> 8);
        arry[2] = (byte)((m & 0xFF0000) >> 16);
        arry[3] = (byte)((m >> 24) & 0xFF);
        Debug.LogError(arry[0] + " " + arry[1] + " " + arry[2] + " " + arry[3]);
    }

    //拆出一个包转为消息存储
    private void BuildMessage(byte[] buffer, int buffSize, int pid)
    {
        //Debug.LogError("BuildMessage:" + pid);
        lock (this.RecvQueueLocker_)
        {
            Packet packet = new Packet();
            packet.pid = pid;
            packet.length = buffSize;
            packet.data = buffer;
            this.RevQueue_.Enqueue(packet);
        }
    }

    //发送消息
    public bool Send(UInt16 id, System.Object msg)
    {
        if (!(msg is ProtoBuf.IExtensible))
        {
            return false;
        }
        //Serialize pb message
        MemoryStream stream = new MemoryStream();
        ProtoBuf.Meta.RuntimeTypeModel.Default.Serialize(stream, msg);
        byte[] protobufBuffer = stream.ToArray();

        //prepare fill send msgbuffer
        byte[] tcpMessageBuffer = new byte[protobufBuffer.Length + PacketHeadSize];

        //1th step: set message length 
        UInt16 msgLen = (UInt16)(protobufBuffer.Length + 2);
        byte[] packetSizeBytes = BitConverter.GetBytes(msgLen);
        Array.Reverse(packetSizeBytes);
        Buffer.BlockCopy(packetSizeBytes, 0, tcpMessageBuffer, 0, packetSizeBytes.Length);

        //2th step: set message id
        byte[] pidBytes = BitConverter.GetBytes(id);
        Array.Reverse(pidBytes);
        Buffer.BlockCopy(pidBytes, 0, tcpMessageBuffer, 2, pidBytes.Length);

        //3th step: set protobuf messsage
        //Array.Reverse(protobufBuffer);
        Buffer.BlockCopy(protobufBuffer, 0, tcpMessageBuffer, PacketHeadSize, protobufBuffer.Length);

        /*string str = "tcpMessageBuffer:";
        for (int i = 0; i < tcpMessageBuffer.Length; i++)
        {
            str += tcpMessageBuffer[i] + ", ";
        }
        Debug.LogError(str);*/

        //send
        //enqueue msg package 
        Monitor.Enter(this.RecvQueueLocker_);
        this.SendQueue_.Enqueue(tcpMessageBuffer);
        Monitor.Exit(this.RecvQueueLocker_);
        return true;
    }

    //获取收到的消息队列
    public Queue<Packet> QueryRevQueue()
    {
        return this.RevQueue_;
    }

    // 取出网络消息(每次最多取10个)
    public bool PopMessage(ref Queue<Packet> msgQue, int count = 10)
    {
        while (this.RevQueue_.Count != 0 && count > 0)
        {
            count--;
            Packet msg = this.RevQueue_.Dequeue();
            msgQue.Enqueue(msg);
        }
        return true;
    }

    public void UpdateRecvHeartTickTime()
    {
        this.RecvHeartTickTime_ = DateTime.Now;
    }

    //心跳超时
    public bool IsConnectOverTime()
    {
        if (DateTime.Now.Subtract(this.RecvHeartTickTime_).TotalMilliseconds >= this.HeartTickInterval_ * 1000)
        {
            return true;
        }
        return false;
    }

    //心跳发送时刻
    public bool IsHeartTickSendTime()
    {
        int startupTime = (int)Time.realtimeSinceStartup;
        return startupTime % this.SendHeartTickInterval_ == 0;
    }

}

//内部消息包
public class Packet
{
    public int pid;
    public int length;
    public byte[] data;
}
