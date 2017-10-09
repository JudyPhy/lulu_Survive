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
        if (CanUpdateFrame())
        {
            FrameSync.Instance.CurFrameIndex++;
            FramePacket curFrameData = FrameSync.Instance.FrameList[0];
            Debug.Log("CurFrameIndex:" + FrameSync.Instance.CurFrameIndex + ", FrameList[0]:" + curFrameData.frameIndex);
            if (curFrameData.frameIndex <= FrameSync.Instance.CurFrameIndex)
            {
                Debug.Log("Refresh cur frame ui...");
                FrameSync.Instance.FrameList.RemoveAt(0);
                RefreshCurFrameUI(curFrameData);
            }
            FrameSync.Instance.UpdateNextFrameTimes();
        }
    }

    private void RefreshCurFrameUI(FramePacket data)
    {

    }

    private bool CanUpdateFrame()
    {
        if (FrameSync.Instance.FrameList.Count > 0 && 
            DateTime.Now.Subtract(prevTime).TotalMilliseconds > (FrameSync.Instance.FrameIntervalTime / FrameSync.Instance.NextFrameTimes))
        {
            prevTime = DateTime.Now;
            return true;
        }
        return false;
    }

}
