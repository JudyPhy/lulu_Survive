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

    public List<RoleTypeData> GetLoginRoleTypes()
    {
        return new List<RoleTypeData>(configData.RoleTypeDict.Values);
    }

}