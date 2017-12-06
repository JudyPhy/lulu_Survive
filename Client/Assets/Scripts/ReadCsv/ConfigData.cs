using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigData
{

    //Map
    public Dictionary<int, ConfigMap> CfgMap = new Dictionary<int, ConfigMap>();
    //Story
    public Dictionary<int, ConfigStory> CfgStory = new Dictionary<int, ConfigStory>();
    //Event
    public Dictionary<int, ConfigEvent> CfgEvent = new Dictionary<int, ConfigEvent>();
    //EventPackage
    public Dictionary<int, ConfigEventPackage> CfgEventPackage = new Dictionary<int, ConfigEventPackage>();
    //Item
    public Dictionary<int, ConfigItem> CfgItem = new Dictionary<int, ConfigItem>();
    //Drop
    public Dictionary<int, ConfigDrop> CfgDrop = new Dictionary<int, ConfigDrop>();
    //Monster
    public Dictionary<int, ConfigMonster> CfgMonster = new Dictionary<int, ConfigMonster>();

    public void LoadConfigs()
    {
        //Map
        ReadCsv config = new ReadCsv("Scene");
        for (int i = 3; i < config.GetRow(); i++)
        {
            ConfigMap data = new ConfigMap(config, i);
            this.CfgMap.Add(data._id, data);
        }

        //Story
        config = new ReadCsv("Story");
        for (int i = 3; i < config.GetRow(); i++)
        {
            ConfigStory data = new ConfigStory(config, i);
            this.CfgStory.Add(data._id, data);
        }

        //Event
        config = new ReadCsv("Event");
        for (int i = 3; i < config.GetRow(); i++)
        {
            ConfigEvent data = new ConfigEvent(config, i);
            this.CfgEvent.Add(data._id, data);
        }

        //EventPackage
        config = new ReadCsv("EventPackage");
        for (int i = 3; i < config.GetRow(); i++)
        {
            ConfigEventPackage data = new ConfigEventPackage(config, i);
            this.CfgEventPackage.Add(data._id, data);
        }

        //Item
        config = new ReadCsv("Item");
        for (int i = 3; i < config.GetRow(); i++)
        {
            ConfigItem data = new ConfigItem(config, i);
            this.CfgItem.Add(data._id, data);
        }

        //Drop
        config = new ReadCsv("Drop");
        for (int i = 3; i < config.GetRow(); i++)
        {
            ConfigDrop data = new ConfigDrop(config, i);
            this.CfgDrop.Add(data._id, data);
        }

        //Monster
        config = new ReadCsv("Monster");
        for (int i = 3; i < config.GetRow(); i++)
        {
            ConfigMonster data = new ConfigMonster(config, i);
            this.CfgMonster.Add(data._id, data);
        }
    }
}

public class ConfigMap
{
    public int _id;
    public string _name;
    public string _desc;
    public int _eventPack;
    public int _stage;
    public int _shop;
    public int _destination;
    public int _distance;

    public ConfigMap(ReadCsv config, int row) {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        _eventPack = int.Parse(config.GetDataByRowAndName(row, "EventPackage"));
        _stage = int.Parse(config.GetDataByRowAndName(row, "Stage"));
        _shop = int.Parse(config.GetDataByRowAndName(row, "Shop"));
        _destination = int.Parse(config.GetDataByRowAndName(row, "Destination"));
        _distance = int.Parse(config.GetDataByRowAndName(row, "Distance"));
    }
}

public class ConfigStory
{
    public struct StoryOption
    {
        public string option;
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
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _desc = config.GetDataByRowAndName(row, "Describe");        
        _type = int.Parse(config.GetDataByRowAndName(row, "Type"));
        _nextId = int.Parse(config.GetDataByRowAndName(row, "NextID"));
        if (_type == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                StoryOption data;
                data.option = config.GetDataByRowAndName(row, "Option" + (i + 1).ToString());
                data.result = int.Parse(config.GetDataByRowAndName(row, "Result" + (i + 1).ToString()));
                _optionList.Add(data);
            }
        }
        _sceneId = int.Parse(config.GetDataByRowAndName(row, "Scene"));
    }
}

public class ConfigEvent
{
    public struct EventResult
    {
        public string resultDesc;
        public int reward;
    }

    public int _id;
    public string _name;
    public string _desc;
    public List<EventResult> _resultList = new List<EventResult>();

    public ConfigEvent(ReadCsv config, int row)
    {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        for (int i = 0; i < 2; i++)
        {
            EventResult data;
            data.resultDesc = config.GetDataByRowAndName(row, "Result" + (i + 1).ToString());
            data.reward = int.Parse(config.GetDataByRowAndName(row, "Reward" + (i + 1).ToString()));
            if (!string.IsNullOrEmpty(data.resultDesc))
            {
                _resultList.Add(data);
            }           
        }
    }
}

public class ConfigEventPackage
{
    public int _id;
    public int _packId;
    public int _type;
    public int _event;
    public int _condition;
    public int _weight;

    public ConfigEventPackage(ReadCsv config, int row)
    {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _packId = int.Parse(config.GetDataByRowAndName(row, "PackID"));
        _type = int.Parse(config.GetDataByRowAndName(row, "Type"));
        _event = int.Parse(config.GetDataByRowAndName(row, "Event"));
        _condition = int.Parse(config.GetDataByRowAndName(row, "Condition"));
        _weight = int.Parse(config.GetDataByRowAndName(row, "Weight"));
    }
}

public class ConfigDrop
{
    public struct DropData
    {
        public int _item;
        public int _count;
    }

    public int _id;
    public List<DropData> _itemList = new List<DropData>();

    public ConfigDrop(ReadCsv config, int row)
    {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        for (int i = 0; i < 5; i++)
        {
            DropData data;
            data._item = int.Parse(config.GetDataByRowAndName(row, "Item" + (i + 1).ToString()));
            data._count = int.Parse(config.GetDataByRowAndName(row, "Num" + (i + 1).ToString()));
            _itemList.Add(data);
        }
    }
}

public class ConfigItem
{
    public int _id;
    public string _name;
    public string _desc;
    public string _icon;
    public int _price;

    public ConfigItem(ReadCsv config, int row)
    {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        _icon = config.GetDataByRowAndName(row, "Icon");
        _price = int.Parse(config.GetDataByRowAndName(row, "Price"));
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
    public int _skill;

    public ConfigMonster(ReadCsv config, int row)
    {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        _hp = int.Parse(config.GetDataByRowAndName(row, "HP"));
        _atk = int.Parse(config.GetDataByRowAndName(row, "Atk"));
        _def = int.Parse(config.GetDataByRowAndName(row, "Def"));
        _skill = int.Parse(config.GetDataByRowAndName(row, "Skill"));
    }
}