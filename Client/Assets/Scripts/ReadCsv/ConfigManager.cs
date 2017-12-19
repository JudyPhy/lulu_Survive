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

    public ConfigScene ReqSceneData(int id)
    {
        if (configData.CfgScene.ContainsKey(id))
        {
            return configData.CfgScene[id];
        }
        return null;
    }

    public ConfigStory ReqStory(int id)
    {
        if (configData.CfgStory.ContainsKey(id))
        {
            return configData.CfgStory[id];
        }
        return null;
    }

    public List<ConfigStory> ReqSceneStory(int sceneId)
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

    public List<ConfigEvent> ReqEventList(int sceneId)
    {
        Debug.Log("ReqEventList: sceneId=" + sceneId);
        List<ConfigEvent> list = new List<ConfigEvent>();
        foreach (ConfigEvent data in configData.CfgEvent.Values)
        {
            if (data._sceneId == sceneId)
            {
                list.Add(data);
            }
        }
        return list;
    }

    public List<ConfigMonster> ReqMonsterList(int sceneId)
    {
        Debug.Log("ReqMonsterList: sceneId=" + sceneId);
        List<ConfigMonster> list = new List<ConfigMonster>();
        foreach (ConfigMonster data in configData.CfgMonster.Values)
        {
            if (data._sceneId == sceneId)
            {
                list.Add(data);
            }
        }
        return list;
    }

    public List<ConfigDrop> ReqDropList(int sceneId)
    {
        Debug.Log("ReqDropList: sceneId=" + sceneId);
        List<ConfigDrop> list = new List<ConfigDrop>();
        foreach (ConfigDrop data in configData.CfgDrop.Values)
        {
            if (data._sceneId == sceneId)
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