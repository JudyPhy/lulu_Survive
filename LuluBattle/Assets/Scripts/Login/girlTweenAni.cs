using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girlTweenAni : MonoBehaviour {

    private UITexture _girl;

    void Awake()
    {
        _girl = GetComponent<UITexture>();
    }

	// Use this for initialization
	void Start () {
		
	}

    public void UpdateGirlAlpha(float value)
    {
        _girl.alpha = value;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
