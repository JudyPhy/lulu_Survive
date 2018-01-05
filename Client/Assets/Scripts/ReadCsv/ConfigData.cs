using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigData
{
    //Scene
    public readonly Dictionary<int, ConfigScene> CfgScene = new Dictionary<int, ConfigScene>();
    //Story
    public readonly Dictionary<int, ConfigStory> CfgStory = new Dictionary<int, ConfigStory>();
    //Event
    public readonly Dictionary<int, ConfigEvent> CfgEvent = new Dictionary<int, ConfigEvent>();
    //Item
    public readonly Dictionary<int, ConfigItem> CfgItem = new Dictionary<int, ConfigItem>();
    //Drop
    public readonly Dictionary<int, ConfigDrop> CfgDrop = new Dictionary<int, ConfigDrop>();
    //Monster
    public readonly Dictionary<int, ConfigMonster> CfgMonster = new Dictionary<int, ConfigMonster>();
    //Equipment
    public readonly Dictionary<int, ConfigEquipment> CfgEquipment = new Dictionary<int, ConfigEquipment>();

    public void LoadConfigs()
    {
        foreach (string path in ResourcesManager.CsvDict.Keys)
        {
            ReadCsv config = ResourcesManager.CsvDict[path];
            for (int i = 3; i < config.GetRow(); i++)
            {
                if (path.Contains("Scene.csv"))
                {
                    ConfigScene data = new ConfigScene(config, i);
                    this.CfgScene.Add(data._id, data);
                }
                else if (path.Contains("Story.csv"))
                {
                    ConfigStory data = new ConfigStory(config, i);
                    this.CfgStory.Add(data._id, data);
                }
                else if (path.Contains("Event.csv"))
                {
                    ConfigEvent data = new ConfigEvent(config, i);
                    this.CfgEvent.Add(data._id, data);
                }
                else if (path.Contains("Item.csv"))
                {
                    ConfigItem data = new ConfigItem(config, i);
                    this.CfgItem.Add(data._id, data);
                }
                else if (path.Contains("Drop.csv"))
                {
                    ConfigDrop data = new ConfigDrop(config, i);
                    this.CfgDrop.Add(data._id, data);
                }
                else if (path.Contains("Monster.csv"))
                {
                    ConfigMonster data = new ConfigMonster(config, i);
                    this.CfgMonster.Add(data._id, data);
                }
                else if (path.Contains("Equipment.csv"))
                {
                    ConfigEquipment data = new ConfigEquipment(config, i);
                    this.CfgEquipment.Add(data._id, data);
                }
            }
        }
    }
}

public class ConfigScene
{
    public int _id;
    public string _name;
    public string _desc;
    public int[] _stage;
    public int _shop;
    public int _destination;
    public int _distance;

    public ConfigScene(ReadCsv config, int row)
    {
        _id = (int)float.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        string stageStr = config.GetDataByRowAndName(row, "Stage");
        if (!string.IsNullOrEmpty(stageStr))
        {
            string[] stageStrs = stageStr.Split(',');
            _stage = new int[stageStrs.Length];
            for (int i = 0; i < stageStrs.Length; i++)
            {
                _stage[i] = (int)float.Parse(stageStrs[i]);
            }
        }
        _shop = (int)float.Parse(config.GetDataByRowAndName(row, "Shop"));
        _destination = (int)float.Parse(config.GetDataByRowAndName(row, "Destination"));
        _distance = (int)float.Parse(config.GetDataByRowAndName(row, "Distance"));
    }
}

public class ConfigStory
{
    public struct StoryOption
    {
        public string option;
        public int type;
        public int result;
    }

    public int _id;
    public string _desc;    
    public int _type;
    public int _nextId;
    public List<StoryOption> _optionList = new List<StoryOption>();
    public int _sceneId;

    public ConfigStory(ReadCsv config, int row)
    {
        _id = (int)float.Parse(config.GetDataByRowAndName(row, "ID"));
        _desc = config.GetDataByRowAndName(row, "Describe");        
        _type = (int)float.Parse(config.GetDataByRowAndName(row, "Type"));
        _nextId = (int)float.Parse(config.GetDataByRowAndName(row, "NextID"));
        if (_type == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                StoryOption data;
                data.option = config.GetDataByRowAndName(row, "Option" + (i + 1).ToString());
                data.type = (int)float.Parse(config.GetDataByRowAndName(row, "ResultType" + (i + 1).ToString()));
                data.result = (int)float.Parse(config.GetDataByRowAndName(row, "Result" + (i + 1).ToString()));
                _optionList.Add(data);
            }
        }
        _sceneId = (int)float.Parse(config.GetDataByRowAndName(row, "Scene"));
    }
}

public class ConfigEvent
{
    public struct EventResult
    {
        public string resultDesc;
        public int type;
        public int result;
    }

    public int _id;
    public string _name;
    public string _desc;
    public int _sceneId;
    public int _rate;
    public List<EventResult> _resultList = new List<EventResult>();

    public ConfigEvent(ReadCsv config, int row)
    {
        _id = (int)float.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        _sceneId = (int)float.Parse(config.GetDataByRowAndName(row, "Scene"));
        _rate = (int)float.Parse(config.GetDataByRowAndName(row, "Rate"));
        for (int i = 0; i < 2; i++)
        {
            EventResult data;
            data.resultDesc = config.GetDataByRowAndName(row, "ResultDesc" + (i + 1).ToString());
            data.type = (int)float.Parse(config.GetDataByRowAndName(row, "Type" + (i + 1).ToString()));
            data.result = (int)float.Parse(config.GetDataByRowAndName(row, "Result" + (i + 1).ToString()));
            if (!string.IsNullOrEmpty(data.resultDesc))
            {
                _resultList.Add(data);
            }           
        }
    }
}

public class ConfigDrop
{
    public struct DropData
    {
        public int _itemId;
        public int _countMax;
    }

    public int _id;
    public int _sceneId;
    public int _rate;
    public int _gold;
    public List<DropData> _itemList = new List<DropData>();

    public ConfigDrop(ReadCsv config, int row)
    {
        _id = (int)float.Parse(config.GetDataByRowAndName(row, "ID"));
        _sceneId = (int)float.Parse(config.GetDataByRowAndName(row, "Scene"));
        _rate = (int)float.Parse(config.GetDataByRowAndName(row, "Rate"));
        _gold = (int)float.Parse(config.GetDataByRowAndName(row, "Gold"));

        string itemStr = config.GetDataByRowAndName(row, "Item");
        if (!string.IsNullOrEmpty(itemStr))
        {
            string[] strs = itemStr.Split(';');
            for (int i = 0; i < strs.Length; i++)
            {
                string[] item = strs[i].Split(',');
                if (item.Length == 2)
                {
                    DropData data;
                    data._itemId = (int)float.Parse(item[0]);
                    data._countMax = (int)float.Parse(item[1]);
                    _itemList.Add(data);
                }
                else
                {
                    MyLog.LogError("drop[" + _id + "]'s items config error.");
                }
            }
        }
    }

}

public class ConfigItem
{
    public int _id;
    public string _name;
    public string _desc;
    public int _type;
    public string _icon;
    public int _price;

    public int _healthy;
    public int _energy;
    public int _hungry;

    public int _hp;
    public int _power;
    public int _agile;
    public int _physic;
    public int _charm;
    public int _perception;
    public int _duration;

    public ConfigItem(ReadCsv config, int row)
    {
        _id = (int)float.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        _type = (int)float.Parse(config.GetDataByRowAndName(row, "Type"));
        _icon = config.GetDataByRowAndName(row, "Icon");
        _price = (int)float.Parse(config.GetDataByRowAndName(row, "Price"));

        _healthy = (int)float.Parse(config.GetDataByRowAndName(row, "Healthy"));
        _energy = (int)float.Parse(config.GetDataByRowAndName(row, "Energy"));
        _hungry = (int)float.Parse(config.GetDataByRowAndName(row, "Hungry"));

        _hp = (int)float.Parse(config.GetDataByRowAndName(row, "Hp"));
        _power = (int)float.Parse(config.GetDataByRowAndName(row, "Power"));
        _agile = (int)float.Parse(config.GetDataByRowAndName(row, "Agile"));
        _physic = (int)float.Parse(config.GetDataByRowAndName(row, "Physic"));
        _charm = (int)float.Parse(config.GetDataByRowAndName(row, "Charm"));
        _perception = (int)float.Parse(config.GetDataByRowAndName(row, "Perception"));
        _duration = (int)float.Parse(config.GetDataByRowAndName(row, "Duration"));
    }
}

public class ConfigMonster
{
    public int _id;
    public string _name;
    public string _desc;

    public int _hp;
    public int _atk;
    public int _def;
    public int _power;
    public int _agile;
    public int _physic;
    public int _charm;
    public int _perception;

    public int _skill;
    public int _sceneId;
    public int _rate;
    public int _drop;

    public ConfigMonster(ReadCsv config, int row)
    {
        _id = (int)float.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");

        _hp = (int)float.Parse(config.GetDataByRowAndName(row, "HP"));
        _atk = (int)float.Parse(config.GetDataByRowAndName(row, "Atk"));        
        _def = (int)float.Parse(config.GetDataByRowAndName(row, "Def"));
        _power = (int)float.Parse(config.GetDataByRowAndName(row, "Power"));
        _agile = (int)float.Parse(config.GetDataByRowAndName(row, "Agile"));
        _physic = (int)float.Parse(config.GetDataByRowAndName(row, "Physic"));
        _charm = (int)float.Parse(config.GetDataByRowAndName(row, "Charm"));
        _perception = (int)float.Parse(config.GetDataByRowAndName(row, "Perception"));

        _skill = (int)float.Parse(config.GetDataByRowAndName(row, "Skill"));
        _sceneId = (int)float.Parse(config.GetDataByRowAndName(row, "Scene"));
        _rate = (int)float.Parse(config.GetDataByRowAndName(row, "Rate"));
        _drop = (int)float.Parse(config.GetDataByRowAndName(row, "Drop"));
    }
}

public class ConfigEquipment
{
    public struct MaterialData
    {
        public int _materialId;
        public int _baseCount;
        public int _increaseCount;
    }

    public struct AttrData
    {
        public int _type;
        public int _baseValue;
        public int _increaseValue;
    }

    public int _id;
    public string _name;
    public string _desc;
    public List<MaterialData> _materialList = new List<MaterialData>();
    public AttrData _attr;

    public ConfigEquipment(ReadCsv config, int row)
    {
        _id = (int)float.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        
        for (int i = 1; i < 3; i++)
        {
            MaterialData data;
            data._materialId = (int)float.Parse(config.GetDataByRowAndName(row, "Material" + i.ToString()));
            data._baseCount = (int)float.Parse(config.GetDataByRowAndName(row, "BaseCount" + i.ToString()));
            data._increaseCount = (int)float.Parse(config.GetDataByRowAndName(row, "IncreaseCount" + i.ToString()));
            if (data._baseCount > 0)
            {
                _materialList.Add(data);
            }            
        }
        _attr._type = (int)float.Parse(config.GetDataByRowAndName(row, "AttrType"));
        _attr._baseValue = (int)float.Parse(config.GetDataByRowAndName(row, "BaseAttr"));
        _attr._increaseValue = (int)float.Parse(config.GetDataByRowAndName(row, "IncreaseAttr"));
    }
}