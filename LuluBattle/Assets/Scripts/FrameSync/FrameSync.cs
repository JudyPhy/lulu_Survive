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

public sealed class FrameSync
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
    
    //当前锁定客户端帧
    private long lockFrameIndex;
    //当前客户端帧号
    public long CurFrameIndex = 0;
    //下一个客户端帧的帧率
    public float NextFrameTimes = 1;
    //接收到的服务器帧队列
    public List<FramePacket> FrameList = new List<FramePacket>();

    public void ResetFrame()
    {
        CurFrameIndex = 0;
        FrameList.Clear();
    }

    public void RecvSyncPkg(pb.GSSyncPkgSend msg)
    {
        Debug.Log("RecvSyncPkg: Service FrameIndex=" + msg.Act);
        FramePacket pkg = new FramePacket();
        pkg.frameIndex = msg.Act;
        for (int i = 0; i < msg.Role.Count; i++)
        {
            if (!pkg.roleDatas.ContainsKey(msg.Role[i].PlayerID))
            {
                Role role = new Role();
                role.UpdateTrs(msg.Role[i].Trs);
                pkg.roleDatas.Add(msg.Role[i].PlayerID, role);
            }
        }
        FrameList.Add(pkg);
    }

    public void RefreshCurFrame()
    {
        if (CurFrameIndex > 0 && FrameList.Count > 0 && FrameList[0].frameIndex <= CurFrameIndex)
        {
            Debug.Log("RefreshCurFrame");
            //refresh ui
            //
            //

            FrameList.RemoveAt(0);
        }
    }

    public void UpdateNextFrameTimes()
    {
        List<FramePacket> nextFrameDatas = new List<FramePacket>();
        for (int i = 0; i < FrameList.Count; i++)
        {
            if (FrameList[i].frameIndex <= CurFrameIndex)
            {
                nextFrameDatas.Add(FrameList[i]);
            }
        }
        NextFrameTimes = nextFrameDatas.Count;
        Debug.Log("NextFrameTimes:" + NextFrameTimes);
    }
}
