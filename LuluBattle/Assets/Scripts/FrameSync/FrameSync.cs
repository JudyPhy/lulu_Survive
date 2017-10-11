using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

public class FramePacket
{
    public long frameIndex;
    public Dictionary<uint, Role> roleDatas = new Dictionary<uint, Role>();   //pos、rotate、buff
}

public class FrameSync
{
    private static FrameSync instance;
    public static FrameSync Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FrameSync();
            }
            return instance;
        }
    }

    public int FrameIntervalTime = 99;

    //当前锁定帧
    public long LockFrameIndex;
    //当前客户端帧号
    public long ClientFrameIndex = 0;
    //下一个客户端帧的帧率
    public float NextFrameTimes = 1;
    //接收到的服务器帧队列
    public List<FramePacket> FrameList = new List<FramePacket>();
    

    public void ResetFrame()
    {
        ClientFrameIndex = 0;
        FrameList.Clear();
    }

    public void RecvSyncPkg(pb.GSSyncPkgSend msg)
    {
        FramePacket pkg = new FramePacket();
        pkg.frameIndex = msg.Act;
        for (int i = 0; i < msg.Role.Count; i++)
        {
            Role role = new Role();
            role.UpdateFrameInfo(msg.Role[i]);
            pkg.roleDatas.Add(msg.Role[i].PlayerID, role);
        }
        FrameList.Add(pkg);
        LockFrameIndex = msg.Act;
        if (ClientFrameIndex == 0)
        {
            ClientFrameIndex = msg.Act;
        }
    }

    public void UpdateNextFrameTimes()
    {
        if (ClientFrameIndex >= LockFrameIndex)
        {
            NextFrameTimes = 1;
        }
        else if (ClientFrameIndex < LockFrameIndex)
        {
            NextFrameTimes = FrameList.Count;
            if (NextFrameTimes == 0)
            {
                Debug.LogError("Fatal! ClientFrameIndex < LockFrameIndex, but FrameList.Count=0.");
                NextFrameTimes = 1;
            }
        }
        Debug.Log("NextFrameTimes:" + NextFrameTimes);
    }
}
