using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTips : MonoBehaviour {

    private UILabel _text;
    private UISprite _bg;

    private void Awake()
    {
        _text = transform.FindChild("Label").GetComponent<UILabel>();
        _bg = transform.GetComponent<UISprite>();
    }

    public void Show(string text)
    {
        _text.text = text;     
        _bg.width = (int)_text.printedSize.x + 10;

        // animation
        transform.localPosition = new Vector3(0, -50, 0);
        _bg.color = new Color(1, 1, 1, 0);
        _text.color = new Color(1, 1, 1, 0);
        iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "easytype", iTween.EaseType.easeInOutQuad, "time", 0.3,
            "onupdate", "OnUpdateAlphaIn"));
        iTween.MoveTo(this.gameObject, iTween.Hash("y", 30, "islocal", true, "easytype", iTween.EaseType.easeInOutQuad, "time", 0.8,
            "delay", 0.3));
        iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "easytype", iTween.EaseType.easeInOutQuad, "time", 0.3,
            "delay", 1.6, "onupdate", "OnUpdateAlphaOut"));
    }

    private void OnUpdateAlphaIn(float value)
    {
        _bg.color = new Color(1, 1, 1, value);
        _text.color = new Color(1, 1, 1, value);
    }

    private void OnUpdateAlphaOut(float value)
    {
        _bg.color = new Color(1, 1, 1, value);
        _text.color = new Color(1, 1, 1, value);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
