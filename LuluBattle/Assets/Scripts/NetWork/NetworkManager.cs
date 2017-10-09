using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using SimpleJSON;

public class NetworkManager : MonoBehaviour
{

    public static NetworkManager Instance;

    private DateTime ProcessNetworkEventTime_;      //定时检测网络连接状态  

    private NetConfigData _netConfigData;

    //socket线程
    //public TcpNetworkProcessor GateServerTcpConnect_ = new TcpNetworkProcessor();
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

        ConfigManager.Instance.InitConfigs();
    }

    //初始化socket连接
    private void InitNetwork()
    {
        _netConfigData = NetConfig.LoadConfig();
        Debug.LogError("addr=" + _netConfigData.NetAddr + ", port=" + _netConfigData.Port + ", local=" + _netConfigData.UseLocalAddr);
        this.GameServerTcpConnect_ = CreateTcpConnect();    //一个线程执行，游戏服务器TCP连接Socket
    }

    // 创建TCP连接
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

    //注册需要处理的消息函数
    public void RegisterAllNetworkMsgHandler()
    {
        RegisterMessageHandler((int)MsgDef.GS2CLoginRet, LoginMsgHandler.Instance.RevMsgGS2CLoginRet);
        RegisterMessageHandler((int)MsgDef.GS2CChooseRoleRet, LoginMsgHandler.Instance.RevMsgGS2CChooseRoleRet);
        RegisterMessageHandler((int)MsgDef.GS2CEnterGame, FrameSyncHandler.Instance.RevMsgGS2CEnterGame);
        RegisterMessageHandler((int)MsgDef.GSSyncPkgSend, FrameSyncHandler.Instance.RevMsgGSSyncPkgSend);
    }

    private void RegisterMessageHandler(int pid, PacketHandle hander)
    {
        if (!this.MsgHandleDic_.ContainsKey(pid))
        {
            this.MsgHandleDic_.Add(pid, hander);
        }
        else
        {
            Debug.LogError("MsgHandler[" + pid.ToString() + "] has registered.");
        }
    }

    //连接游戏服务器
    public void ConnectGameServer(string addr, ushort port)
    {
        this.GameServerTcpConnect_.ClearWillSendMessage();
        Debug.Log(string.Format("连接游戏服务器[{0}:{1}]", addr, port));
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

    //每100毫秒检测一次网络连接状况
    private bool IsOverProcessNetworkEventTimeInterval()
    {
        double interval = DateTime.Now.Subtract(this.ProcessNetworkEventTime_).TotalMilliseconds;
        return interval >= 100;
    }

    //根据网络状态处理网络事件
    private void ProcessNetworkEventByState()
    {
        if (IsOverProcessNetworkEventTimeInterval())
        {
            //游戏服务器连接
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

    //游戏服务器连接成功
    private void OnConnectedGameServer()
    {
        if (null == this.GameServerTcpConnect_)
        {
            Debug.LogError("OnConnectedGateServer error!");
        }
        else
        {
            Debug.Log("游戏服务器连接成功，跳转主场景");
            //UIManager.Instance.ShowMainWindow<Panel_Login>(eWindowsID.LoginUI);
        }
    }

    //获取游戏服务器消息
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

    //处理消息逻辑
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
                Debug.LogError(string.Format("消息id:{0} 没有注册处理函数句柄!", msg.pid));
                continue;
            }
            this.MsgHandleDic_[msg.pid](msg.pid, msg.data, msg.length);
        }
    }


    // 发送消息到游戏服务器
    public bool SendToGS(UInt16 id, System.Object msg)
    {
        if (null == this.GameServerTcpConnect_ || !this.GameServerTcpConnect_.IsConnected())
        {
            Debug.LogError(string.Format("游戏服务器连接断开，不能发送消息[msgid:{0}]", id));
            return false;
        }
        return this.GameServerTcpConnect_.Send(id, msg);
    }

    //断开游戏服务器
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
