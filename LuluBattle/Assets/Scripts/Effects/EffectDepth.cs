using UnityEngine;

public class EffectDepth : MonoBehaviour {
    [SerializeField]
    string m_SortingLayerName;
    [SerializeField]
    int m_SortingOrder;

    // Use this for initialization  
    void Start() {
        SortingLayerName = m_SortingLayerName;
        SortingOrder = m_SortingOrder;
    }

    public string SortingLayerName {
        get {
            return m_SortingLayerName;
        }
        set {
            m_SortingLayerName = value;
            Renderer[] renders = this.GetComponentsInChildren<Renderer>();
            foreach (Renderer render in renders) {
                render.sortingLayerName = m_SortingLayerName;
            }
        }
    }

    public int SortingOrder {
        get {
            return m_SortingOrder;
        }
        set {
            m_SortingOrder = value;
            Renderer[] renders = this.GetComponentsInChildren<Renderer>();
            foreach (Renderer render in renders) {
                render.sortingOrder = m_SortingOrder;
            }
        }
    }
}