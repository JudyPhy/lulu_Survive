using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

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
    public uint ClientFrameIndex = 0;
    //下一个客户端帧的帧率
    public float NextFrameTimes = 1;
    

    public void ResetFrame()
    {
        ClientFrameIndex = 0;        
    }

    public void RecvSyncPkg(pb.GS2CSyncPkg msg)
    {
        for (int i = 0; i < msg.RoleList.Count; i++)
        {
            uint plaierOid = msg.RoleList[i].PlayerID;
            if (BattleManager.Instance.Players.ContainsKey(plaierOid))
            {
                BattleManager.Instance.Players[plaierOid].UpdateFrameInfo(msg.RoleList[i].FrameList);
            }            
        }
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
            NextFrameTimes = (LockFrameIndex - ClientFrameIndex);
            if (NextFrameTimes == 0)
            {
                Debug.LogError("Fatal! ClientFrameIndex < LockFrameIndex, but FrameList.Count=0.");
                NextFrameTimes = 1;
            }
        }
        Debug.Log("NextFrameTimes:" + NextFrameTimes);
    }

    public void SendCurProcData(uint index)
    {
        pb.FrameData data = new pb.FrameData();
        data.Index = index;
        data.Attr = Player.Instance.Attr.ToPbAttr();
        data.Move = new pb.RoleMove();
        data.Move.Rot = 0;
        data.Move.Status = pb.MoveStatus.Run;
        BattleMsgHandler.Instance.SendMsgC2GSSyncPkg(index, data);
    }

}
