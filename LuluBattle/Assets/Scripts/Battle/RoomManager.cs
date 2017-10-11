using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTransmit;
using System;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    private SelfRole _selfRole;
    private Dictionary<int, Item_Role> _otherPlayers = new Dictionary<int, Item_Role>();

    private bool playCurFrame;
    private DateTime prevTime;

    private void Awake()
    {
        Instance = this;
        GameObject obj = GameObject.Find("SelfRole");
        obj.SetActive(false);
        _selfRole = obj.AddComponent<SelfRole>();
    }

    private void Start()
    {
        //_selfRole.Born();
    }

    private void Update()
    {
        if (FrameSync.Instance.ClientFrameIndex > 0 && DisplayFrame() && FrameSync.Instance.LockFrameIndex > 0)
        {            
            Debug.Log("ClientFrameIndex:" + FrameSync.Instance.ClientFrameIndex + ", LockFrameIndex:" + FrameSync.Instance.LockFrameIndex);
            FrameSync.Instance.UpdateNextFrameTimes();
            if (FrameSync.Instance.FrameList.Count > 0)
            {
                FramePacket curFrameData = FrameSync.Instance.FrameList[0];
                //Debug.Log("FrameList[0]:" + curFrameData.frameIndex +
                //    ", FrameList[max]:" + FrameSync.Instance.FrameList[FrameSync.Instance.FrameList.Count - 1].frameIndex);
                if (curFrameData.frameIndex <= FrameSync.Instance.ClientFrameIndex)
                {
                    Debug.Log("Refresh cur frame ui...");
                    FrameSync.Instance.FrameList.RemoveAt(0);
                    RefreshCurFrameUI(curFrameData);
                }
            }
            FrameSync.Instance.ClientFrameIndex++;
        }
    }

    private void RefreshCurFrameUI(FramePacket data)
    {
        //Debug.Log("RefreshCurFrameUI roleCount:" + data.roleDatas.Count);
        foreach (uint playerId in data.roleDatas.Keys)
        {
            if (BattleManager.Instance.RoomPlayersInfo.ContainsKey(playerId))
            {
                BattleManager.Instance.RoomPlayersInfo[playerId].UpdateCurInfo(data.roleDatas[playerId]);
            }
        }
    }

    private bool DisplayFrame()
    {
        if (DateTime.Now.Subtract(prevTime).TotalMilliseconds > (FrameSync.Instance.FrameIntervalTime / FrameSync.Instance.NextFrameTimes))
        {
            prevTime = DateTime.Now;
            return true;
        }
        return false;
    }

}
