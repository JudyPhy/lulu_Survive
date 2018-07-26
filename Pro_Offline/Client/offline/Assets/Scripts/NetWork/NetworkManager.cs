using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class NetworkManager : MonoBehaviour
{

    public static NetworkManager Instance;

    private DateTime ProcessNetworkEventTime_;      //定时检测网络连接状态  

    private NetConfigData _netConfigData;

    //socket线程
    public TcpNetworkProcessor GameServerTcpConnect_ = new TcpNetworkProcessor();

    private static readonly object TcpIDLock = new object();    //同步锁
    private int TcpConnID = 0; // TCP网络连接ID
    //处理消息相关
    private Dictionary<int, PacketHandle> MsgHandleDic_ = new Dictionary<int, PacketHandle>();  //注册消息字典，只处理字典里注册过的消息
    public delegate void PacketHandle(int pid, byte[] msg, int msgSize);        //消息处理托管
    public Queue<Packet> MessageQueue_ = new Queue<Packet>();  //待处理的消息队列

    public bool Test_GateConnectSuccess_ = false;

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this.gameObject);

        InitNetwork();

        RegisterAllNetworkMsgHandler();

        prevTime = DateTime.Now;
    }

    private void Start()
    {
        if (_netConfigData.UseLocalAddr)
        {
            _netConfigData.NetAddr = "127.0.0.1";
        }
        ConnectGameServer(_netConfigData.NetAddr, _netConfigData.Port);
    }
    
    private void InitNetwork()
    {
        _netConfigData = NetConfig.LoadConfig();
        MyLog.LogError("addr=" + _netConfigData.NetAddr + ", port=" + _netConfigData.Port + ", local=" + _netConfigData.UseLocalAddr);
        this.GameServerTcpConnect_ = CreateTcpConnect();    //一个线程执行，游戏服务器TCP连接Socket
    }
    
    private TcpNetworkProcessor CreateTcpConnect()
    {
        lock (TcpIDLock)
        {
            this.TcpConnID++;
            TcpNetworkProcessor tcp = new TcpNetworkProcessor();    //创建TCP连接时，顺带创建发送线程，后台一直监听发送消息
            tcp.ID_ = ++this.TcpConnID;
            return tcp;
        }
    }
    
    public void RegisterAllNetworkMsgHandler()
    {
        RegisterMessageHandler((int)MsgDef.GS2CLoginRet, LoginMsgHandler.Instance.RevGS2CLoginRet);
        //RegisterMessageHandler((int)MsgDef.C2GSSwitchMapRet, LoginMsgHandler.Instance.RevMsgGS2CChooseRoleRet);
        //RegisterMessageHandler((int)MsgDef.GS2CBattleInfo, BattleMsgHandler.Instance.RevMsgGS2CStartGameRet);
    }

    private void RegisterMessageHandler(int pid, PacketHandle hander)
    {
        if (!this.MsgHandleDic_.ContainsKey(pid))
        {
            this.MsgHandleDic_.Add(pid, hander);
        }
        else
        {
            MyLog.LogError("MsgHandler[" + pid.ToString() + "] has registered.");
        }
    }
    
    public void ConnectGameServer(string addr, ushort port)
    {
        this.GameServerTcpConnect_.ClearWillSendMessage();
        MyLog.Log(string.Format("连接游戏服务器[{0}:{1}]", addr, port));
        this.GameServerTcpConnect_.Connect(addr, port);     //连接游戏服务器成功后，开启新一个接收消息的线程，该接收线程一直存在，除非断开连接，套接字关闭
    }

    void Update()
    {
        ////心跳
        //ProcessHeartTick();

        //根据网络状态处理网络事件（定时器）
        ProcessNetworkEventByState();

        //取出游戏服务器消息
        FetchGameServerMsg();

        //处理取出的服务器消息
        ProcMsgLogic();
    }

    DateTime prevTime;

    private bool OverTestTimeInterval() {
        if (DateTime.Now.Subtract(prevTime).TotalMilliseconds > 1000)
        {
            prevTime = DateTime.Now;
            return true;
        }
        return false;
    }
    
    private bool IsOverProcessNetworkEventTimeInterval()
    {
        double interval = DateTime.Now.Subtract(this.ProcessNetworkEventTime_).TotalMilliseconds;
        return interval >= 100;
    }
    
    private void ProcessNetworkEventByState()
    {
        if (IsOverProcessNetworkEventTimeInterval())
        {
            if (this.GameServerTcpConnect_ != null)
            {
                if (e_SocketState.SCK_CONNECT_SUCCESS == this.GameServerTcpConnect_.SocketState_)
                {
                    this.GameServerTcpConnect_.SocketState_ = e_SocketState.SCK_CONNECTED;
                    OnConnectedGameServer();
                }
            }
        }
    }
    
    private void OnConnectedGameServer()
    {
        if (null == this.GameServerTcpConnect_)
        {
            MyLog.LogError("connect game server error!");
        }
        else
        {
            UIManager.Instance.StartGame();
        }
    }
    
    private void FetchGameServerMsg()
    {
        if (null == this.GameServerTcpConnect_)
        {
            return;
        }
        Queue<Packet> queue = this.GameServerTcpConnect_.QueryRevQueue();
        if (queue.Count <= 0)
        {
            return;
        }
        lock (this.MessageQueue_)
        {
            this.GameServerTcpConnect_.PopMessage(ref this.MessageQueue_);
        }
    }
    
    private void ProcMsgLogic()
    {
        int count = 10;
        while (this.MessageQueue_.Count != 0 && count > 0)
        {
            count--;
            Packet msg = this.MessageQueue_.Dequeue();
            if (null == msg)
            {
                continue;
            }
            if (!this.MsgHandleDic_.ContainsKey(msg.pid))
            {
                MyLog.LogError(string.Format("消息id:{0} 没有注册处理函数句柄!", msg.pid));
                continue;
            }
            this.MsgHandleDic_[msg.pid](msg.pid, msg.data, msg.length);
        }
    }

    public bool SendToGS(UInt16 id, System.Object msg)
    {
        if (null == this.GameServerTcpConnect_ || !this.GameServerTcpConnect_.IsConnected())
        {
            MyLog.LogError(string.Format("游戏服务器连接断开，不能发送消息[msgid:{0}]", id));
            return false;
        }
        return this.GameServerTcpConnect_.Send(id, msg);
    }
    
    public void DisconnectGameServer()
    {
        if (null != this.GameServerTcpConnect_)
        {
            this.GameServerTcpConnect_.DisConnect();
        }
    }

    private void OnDestroy()
    {
        DisconnectGameServer();
    }
    

}
