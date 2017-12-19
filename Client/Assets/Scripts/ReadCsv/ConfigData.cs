using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigData
{
    //Scene
    public Dictionary<int, ConfigScene> CfgScene = new Dictionary<int, ConfigScene>();
    //Story
    public Dictionary<int, ConfigStory> CfgStory = new Dictionary<int, ConfigStory>();
    //Event
    public Dictionary<int, ConfigEvent> CfgEvent = new Dictionary<int, ConfigEvent>();
    //Item
    public Dictionary<int, ConfigItem> CfgItem = new Dictionary<int, ConfigItem>();
    //Drop
    public Dictionary<int, ConfigDrop> CfgDrop = new Dictionary<int, ConfigDrop>();
    //Monster
    public Dictionary<int, ConfigMonster> CfgMonster = new Dictionary<int, ConfigMonster>();

    public void LoadConfigs()
    {
        //Scene
        ReadCsv config = new ReadCsv("Scene2");
        for (int i = 3; i < config.GetRow(); i++)
        {
            ConfigScene data = new ConfigScene(config, i);
            this.CfgScene.Add(data._id, data);
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

public class ConfigScene
{
    public int _id;
    public string _name;
    public string _desc;
    public int _stage;
    public int _shop;
    public Vector2 _pos;
    public Vector4 _range;
    public Dictionary<int, Vector2> _outList = new Dictionary<int, Vector2>();

    public ConfigScene(ReadCsv config, int row)
    {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        _stage = int.Parse(config.GetDataByRowAndName(row, "Stage"));
        _shop = int.Parse(config.GetDataByRowAndName(row, "Shop"));

        string pos = config.GetDataByRowAndName(row, "Pos");
        string[] strs1 = pos.Split(',');
        _pos = new Vector2(int.Parse(strs1[0]), int.Parse(strs1[1]));

        string range = config.GetDataByRowAndName(row, "InnerRange");
        string[] strs2 = range.Split(',');
        _range = new Vector4(int.Parse(strs2[0]), int.Parse(strs2[1]), int.Parse(strs2[2]), int.Parse(strs2[3]));

        for (int i = 0; i < 3; i++)
        {
            int outId = int.Parse(config.GetDataByRowAndName(row, "Out" + (i + 1).ToString()));
            if (outId != 0 && !_outList.ContainsKey(outId))
            {
                string outPos = config.GetDataByRowAndName(row, "OutPos" + (i + 1).ToString());
                string[] strs3 = outPos.Split(',');
                _outList.Add(outId, new Vector2(int.Parse(strs3[0]), int.Parse(strs3[1])));
            }
        }
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
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        _sceneId = int.Parse(config.GetDataByRowAndName(row, "Scene"));
        _rate = int.Parse(config.GetDataByRowAndName(row, "Rate"));
        for (int i = 0; i < 2; i++)
        {
            EventResult data;
            data.resultDesc = config.GetDataByRowAndName(row, "ResultDesc" + (i + 1).ToString());
            data.type = int.Parse(config.GetDataByRowAndName(row, "Type" + (i + 1).ToString()));
            data.result = int.Parse(config.GetDataByRowAndName(row, "Result" + (i + 1).ToString()));
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
        public int _item;
        public int _count;
    }

    public int _id;
    public int _sceneId;
    public int _rate;
    public int _gold;
    public List<DropData> _itemList = new List<DropData>();

    public ConfigDrop(ReadCsv config, int row)
    {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _sceneId = int.Parse(config.GetDataByRowAndName(row, "Scene"));
        _rate = int.Parse(config.GetDataByRowAndName(row, "Rate"));
        _gold = int.Parse(config.GetDataByRowAndName(row, "Gold"));
        for (int i = 0; i < 4; i++)
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
    public int _sceneId;
    public int _rate;

    public ConfigMonster(ReadCsv config, int row)
    {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        _hp = int.Parse(config.GetDataByRowAndName(row, "HP"));
        _atk = int.Parse(config.GetDataByRowAndName(row, "Atk"));
        _def = int.Parse(config.GetDataByRowAndName(row, "Def"));
        _skill = int.Parse(config.GetDataByRowAndName(row, "Skill"));
        _sceneId = int.Parse(config.GetDataByRowAndName(row, "Scene"));
        _rate = int.Parse(config.GetDataByRowAndName(row, "Rate"));
    }
}