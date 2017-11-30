using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigManager
{

    private static ConfigManager instance;
    public static ConfigManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ConfigManager();
            }
            return instance;
        }
    }

    private static ConfigData configData;

    public void InitConfigs()
    {
        configData = new ConfigData();
        configData.LoadConfigs();
    }

    public ConfigMap ReqMapData(int id)
    {
        if (configData.CfgMap.ContainsKey(id))
        {
            return configData.CfgMap[id];
        }
        return null;
    }

    public List<ConfigStory> ReqStory(int sceneId)
    {
        List<ConfigStory> list = new List<ConfigStory>();
        foreach (ConfigStory data in configData.CfgStory.Values)
        {
            if (data._sceneId == sceneId)
            {
                list.Add(data);
            }
        }
        return list;
    }

    public List<ConfigEventPackage> ReqEvents(int eventPackId)
    {
        List<ConfigEventPackage> list = new List<ConfigEventPackage>();
        foreach (ConfigEventPackage data in configData.CfgEventPackage.Values)
        {
            if (data._packId == eventPackId)
            {
                list.Add(data);
            }
        }
        return list;
    }

    public ConfigEvent ReqEvent(int eventId)
    {
        if (configData.CfgEvent.ContainsKey(eventId))
        {
            return configData.CfgEvent[eventId];
        }
        return null;
    }

    public ConfigMonster ReqMonster(int id)
    {
        if (configData.CfgMonster.ContainsKey(id))
        {
            return configData.CfgMonster[id];
        }
        return null;
    }

    public ConfigDrop ReqDrop(int id)
    {
        if (configData.CfgDrop.ContainsKey(id))
        {
            return configData.CfgDrop[id];
        }
        return null;
    }

    public ConfigItem ReqItem(int id)
    {
        if (configData.CfgItem.ContainsKey(id))
        {
            return configData.CfgItem[id];
        }
        return null;
    }

}