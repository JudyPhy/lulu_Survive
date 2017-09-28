using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeSync
{
    public static long delayTime = 0;
    private static long delayTimeThreshold = 100;
    private static Queue<long> _reqtime;

    public static void ReqSyncTime()
    {
        long now = GetNowLocalTimeStamp();
        //send
        _reqtime.Enqueue(now);
    }

    public static void RevSyncTime(long serviceTime)
    {
        Debug.Log("RevSyncTime: serviceTime=" + serviceTime.ToString());
        long now = GetNowLocalTimeStamp();
        long prev = _reqtime.Dequeue();
        long netDelay = (now - prev) / 2;
        long correctTime = serviceTime + netDelay;
        long curDelayTime = correctTime - now;
        delayTime += curDelayTime;
        Debug.Log("curDelayTime=" + curDelayTime.ToString());
        if (Mathf.Abs(curDelayTime) > delayTimeThreshold)
        {
            ReqSyncTime();
        }
    }

    private static long GetNowLocalTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalMilliseconds) + delayTime;
    }

}
