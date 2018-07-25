using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class BaseWindow : MonoBehaviour
{
    protected UIPanel mPanel;
    protected GComponent mView;
    void Awake()
    {
        mPanel = GetComponent<UIPanel>();
        mView = mPanel.ui;
        OnAwake();
        OnComponentPrepared();
    }

    void Start() { OnStart(); }

    void OnEnable() { OnReshow(); }

    void OnDisable() { Disable(); }

    void Update() { OnUpdate(); }

    void OnDestroy() { Destroy(); }

    public virtual void OnAwake() { }
    public virtual void OnComponentPrepared() { }
    public virtual void OnStart() { }
    public virtual void OnReshow() { }
    public virtual void Disable() { }
    public virtual void OnUpdate() { }
    public virtual void Destroy() { }
}
