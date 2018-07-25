using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class NarvigationBar : BaseWindow
{
    private Dictionary<NarvigationType, GButton> mButtons = new Dictionary<NarvigationType, GButton>();
    
    public override void OnComponentPrepared()
    {
        float offset = 113.0f / 128 * 10.0f;
        transform.localPosition = new Vector3(0, -offset, 0);
        mPanel.SetSortingOrder(10, true);

        for (int i = 0; i < 5; i++)
        {
            string name = "button" + (i + 1).ToString();
            GButton btn = mView.GetChild(name).asButton;
            btn.onClick.Add(OnClickNar);
            NarvigationType type = (NarvigationType)i;
            mButtons.Add(type, btn);
        }
    }

    private void OnClickNar(EventContext context)
    {
        GButton btn = (GButton)context.sender;
        if (btn == mButtons[NarvigationType.Home])
        {
            MyLog.Log("click home");
        }
        else if (btn == mButtons[NarvigationType.Bag])
        {
            MyLog.Log("click bag");
        }
        else if (btn == mButtons[NarvigationType.Battle])
        {
            MyLog.Log("click battle");
            UIManager.Instance.ShowWindow<BattleUI>(DefineWindow.WindowID.Battle);
        }
        else if (btn == mButtons[NarvigationType.Soul])
        {
            MyLog.Log("click soul");
        }
        else if (btn == mButtons[NarvigationType.More])
        {
            MyLog.Log("click more");
        }
    }

}
