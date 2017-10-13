using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventTransmit;

public class SelfRole : MonoBehaviour
{

    private void Awake()
    {

    }

    public void Born(Vector2 pos)
    {
        Debug.Log("Self born pos:" + pos.x + "," + pos.y);
        
    }

}
