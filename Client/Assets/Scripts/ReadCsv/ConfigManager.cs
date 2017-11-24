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
        return null;
    }

    public List<ConfigEventPackage> GetEvents(int sceneId)
    {
        List<ConfigEventPackage> list = new List<ConfigEventPackage>();
        if (configData.CfgMap.ContainsKey(sceneId))
        {
            int eventPackId = configData.CfgMap[sceneId]._eventPack;
            foreach(ConfigEventPackage data in configData.CfgEventPackage.Values)
            {
                if (data._packId == eventPackId)
                {
                    list.Add(data);
                }
            }           
        }
        return list;
    }
}