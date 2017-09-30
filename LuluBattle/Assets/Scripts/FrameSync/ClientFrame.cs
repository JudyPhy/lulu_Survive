using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClientFrame : MonoBehaviour
{
    private bool playCurFrame;
    private DateTime prevTime;

    void Awake()
    {
        prevTime = DateTime.Now;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CanUpdateFrame())
        {
            Debug.Log("Frame ====>>> " + FrameSync.Instance.CurFrameIndex);
            FrameSync.Instance.RefreshCurFrame();
            FrameSync.Instance.UpdateNextFrameTimes();
            FrameSync.Instance.CurFrameIndex++;
        }
    }

    private bool CanUpdateFrame()
    {
        if (FrameSync.Instance.NextFrameTimes > 0 && DateTime.Now.Subtract(prevTime).TotalMilliseconds > (99 / FrameSync.Instance.NextFrameTimes))
        {
            prevTime = DateTime.Now;
            return true;
        }
        return false;
    }
}
