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
        CreatePlayers();
    }

    private void CreatePlayers()
    {
        List<Role> roles = BattleManager.Instance.GetPlayersData();
        for (int i = 0; i < roles.Count; i++)
        {
            if (roles[i].OID == Player.Instance.OID)
            {
                _selfRole.Born(roles[i].BornPos);
            }
            else
            {

            }
            Debug.Log("Create player" + roles[i].Nickname + " over.");
        }
    }

    private void Update()
    {
        //客户端收到第一帧信息后才开始正式渲染，启动客户端第一帧
        if (FrameSync.Instance.ClientFrameIndex > 0 && DisplayFrame() && FrameSync.Instance.LockFrameIndex > 0)
        {            
            Debug.Log("ClientFrameIndex:" + FrameSync.Instance.ClientFrameIndex + ", LockFrameIndex:" + FrameSync.Instance.LockFrameIndex);
            FrameSync.Instance.UpdateNextFrameTimes();
            foreach (Role role in BattleManager.Instance.Players.Values)
            {
                Debug.Log("Refresh player " + role.OID);
                role.RemoveFrame(FrameSync.Instance.ClientFrameIndex);
            }
            FrameSync.Instance.SendCurProcData(FrameSync.Instance.ClientFrameIndex);
            FrameSync.Instance.ClientFrameIndex++;
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
