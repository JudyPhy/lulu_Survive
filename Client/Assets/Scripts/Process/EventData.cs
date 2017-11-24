using UnityEngine;
using System.Collections;

public class EventData
{
    public int Type;
    public int ID;

    public EventData(int type, int id)
    {
        Type = type;
        ID = id;
    }
}