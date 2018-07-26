using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class MapUI : BaseWindow
{
    private GList mList;

    public override void OnComponentPrepared()
    {
        mList.onClickItem.Add(OnClickItem);
        mList.itemRenderer = RenderListItem;
    }

    void RenderListItem(int index, GObject obj)
    {
        GButton button = (GButton)obj;
        button.icon = "map_" + index.ToString();
    }

    void OnClickItem(EventContext context)
    {
        GButton item = (GButton)context.data;
        //this.contentPane.GetChild("n11").asLoader.url = item.icon;
        //this.contentPane.GetChild("n13").text = item.icon;
    }

}
