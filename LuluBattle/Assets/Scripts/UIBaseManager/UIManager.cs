using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    //UI摄像机
    public static Camera UICamera_;
    //Center Root
    private GameObject _centerRoot;
    //当前显示窗口
    private WindowsBasePanel CurShowingWindow_;
    private bool IsShowingWindow_ = false;
    //等待删除的窗口
    public Dictionary<eWindowsID, WindowsBasePanel> DeletingWindowsDict_ = new Dictionary<eWindowsID, WindowsBasePanel>();
    //当前提示窗口
    private Panel_Tips _curTipsWindow;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        UICamera_ = this.transform.FindChild("Camera").GetComponent<Camera>();
        _centerRoot = UICamera_.transform.FindChild("CenterAnchor").gameObject;
    }

    public void ShowMainWindow<T>(eWindowsID windowId) where T : WindowsBasePanel
    {
        this.IsShowingWindow_ = true;
        if (this.CurShowingWindow_ != null)
        {
            if (this.CurShowingWindow_.WindowID == windowId)
            {
                this.IsShowingWindow_ = false;
                return;
            }
            else
            {
                CloseMainWindow(this.CurShowingWindow_.WindowID);
            }
        }
        if (this.DeletingWindowsDict_.ContainsKey(windowId))
        {
            this.CurShowingWindow_ = this.DeletingWindowsDict_[windowId];
            this.CurShowingWindow_.gameObject.SetActive(true);
            this.DeletingWindowsDict_.Remove(windowId);
        }
        else
        {
            T windowScript = AddChild<T>(_centerRoot);
            if (windowScript != null)
            {
                windowScript.gameObject.SetActive(true);
                windowScript.WindowID = windowId;
                this.CurShowingWindow_ = windowScript;
            }
            else
            {
                Debug.LogError("窗口[" + windowId.ToString() + "] 创建失败，请检查预制路径或实例化是否成功。");
            }
        }
        this.IsShowingWindow_ = false;
    }

    public void CloseMainWindow(eWindowsID windowId)
    {
        if (this.CurShowingWindow_ == null)
        {
            Debug.LogError("当前没有显示的窗口，严重bug!!!!!!!");
            return;
        }
        if (this.CurShowingWindow_.WindowID != windowId)
        {
            Debug.LogError("当前显示的窗口 [" + this.CurShowingWindow_.WindowID + "] 与想要关闭的窗口 [" + windowId + "] 不一致。");
            return;
        }
        this.CurShowingWindow_.CloseWindow();
        this.DeletingWindowsDict_.Add(windowId, this.CurShowingWindow_);
    }

    public void ShowTips(TipsType type, params object[] args)
    {
        if (_curTipsWindow == null)
        {
            _curTipsWindow = AddChild<Panel_Tips>(_centerRoot);
        }
        switch (type)
        {
            case TipsType.text:
                _curTipsWindow.ShowTextTips((string)args[0]);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (!this.IsShowingWindow_)
        {
            foreach (eWindowsID id in this.DeletingWindowsDict_.Keys)
            {
                System.DateTime now = System.DateTime.Now;
                if ((now - this.DeletingWindowsDict_[id].CloseTime_).TotalMilliseconds > 10000)
                {
                    GameObject obj = this.DeletingWindowsDict_[id].gameObject;
                    this.DeletingWindowsDict_.Remove(id);
                    DestroyImmediate(obj);
                    break;
                }
            }
        }
    }
    public static T AddChild<T>(GameObject parent)
    {
        string prefabName = typeof(T).Name;
        string prefabPath = ResourcesManager.Instance.GetResPath(prefabName);
        GameObject obj = ResourcesManager.Instance.GetUIPrefabs(prefabPath);
        if (obj != null)
        {
            obj.AddComponent(typeof(T));
            AddGameObject(parent, obj);
            return obj.GetComponent<T>();
        }
        return default(T);
    }

    private static void AddGameObject(GameObject parentObj, GameObject obj)
    {
        obj.transform.parent = parentObj.transform;
        obj.transform.localEulerAngles = Vector3.zero;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
    }

    public static GameObject AddGameObject(string prefabPath, GameObject parent)
    {
        GameObject obj = ResourcesManager.Instance.GetUIPrefabs(prefabPath);
        if (obj != null)
        {
            AddGameObject(parent, obj);
        }
        return obj;
    }

}
