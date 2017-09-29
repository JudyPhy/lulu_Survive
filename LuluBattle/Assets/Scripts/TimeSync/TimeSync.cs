using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeSync
{
    public static long delayTime = 0;
    public static bool syncOver = false;
    private static long delayTimeThreshold = 100;
    private static Queue<long> _reqtime = new Queue<long>();

    public static void ReqSyncTime()
    {
        syncOver = false;
        long now = GetNowLocalTimeStamp();  //1 5066 5415 2652
        TimeSyncMsgHandler.Instance.SendMsgC2GSReqSyncTime(now);
        _reqtime.Enqueue(now);
    }

    public static void RevSyncTime(long serviceTime)
    {
        Debug.Log("RevSyncTime: serviceTime=" + serviceTime.ToString());
        long now = GetNowLocalTimeStamp();
        Debug.Log("error Time:" + now);
        long prev = _reqtime.Dequeue();
        long netDelay = (long)((now - prev) / 2.0f + 0.5f);
        Debug.Log("netDelay=" + netDelay);
        long correctTime = serviceTime + netDelay;
        Debug.Log("correctTime=" + correctTime);
        long curDelayTime = correctTime - now;
        delayTime += curDelayTime;
        Debug.Log("curDelayTime=" + curDelayTime.ToString());
        if (Mathf.Abs(curDelayTime) > delayTimeThreshold)
        {
            ReqSyncTime();
        }
        else
        {
            syncOver = true;
        }
    }

    private static long GetNowLocalTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds) + delayTime;
    }

}
