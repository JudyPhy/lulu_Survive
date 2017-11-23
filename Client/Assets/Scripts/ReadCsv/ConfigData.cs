﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigData {

    //Map
    public Dictionary<int, ConfigMap> CfgMap = new Dictionary<int, ConfigMap>();
    //Event
    public Dictionary<int, ConfigEvent> CfgEvent = new Dictionary<int, ConfigEvent>();
    //EventPackage
    public Dictionary<int, ConfigEventPackage> CfgEventPackage = new Dictionary<int, ConfigEventPackage>();
    //Drop
    public Dictionary<int, ConfigDrop> CfgDrop = new Dictionary<int, ConfigDrop>();
    //Monster
    public Dictionary<int, ConfigMonster> CfgMonster = new Dictionary<int, ConfigMonster>();

    public void LoadConfigs() {
        //Map
        ReadCsv config = new ReadCsv("Map");
        for (int i = 3; i < config.GetRow(); i++) {
            ConfigMap data = new ConfigMap(config, i);
            this.CfgMap.Add(data._id, data);
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

    public ConfigMap(ReadCsv config, int row) {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        _eventPack = int.Parse(config.GetDataByRowAndName(row, "EventPackage"));
        _stage = int.Parse(config.GetDataByRowAndName(row, "Stage"));
        _shop = int.Parse(config.GetDataByRowAndName(row, "Shop"));
    }
}

public class ConfigEvent
{
    public int _id;
    public string _name;
    public string _desc;
    public string _result1;
    public string _result2;
    public int _reward1;
    public int _reward2;

    public ConfigEvent(ReadCsv config, int row)
    {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        _result1 = config.GetDataByRowAndName(row, "Result1");
        _result2 = config.GetDataByRowAndName(row, "Result2");
        _reward1 = int.Parse(config.GetDataByRowAndName(row, "Reward1"));
        _reward2 = int.Parse(config.GetDataByRowAndName(row, "Reward2"));
    }
}

public class ConfigEventPackage
{
    public int _id;
    public string _name;
    public string _desc;
    public string _result1;
    public string _result2;
    public int _reward1;
    public int _reward2;

    public ConfigEventPackage(ReadCsv config, int row)
    {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _desc = config.GetDataByRowAndName(row, "Describe");
        _result1 = config.GetDataByRowAndName(row, "Result1");
        _result2 = config.GetDataByRowAndName(row, "Result2");
        _reward1 = int.Parse(config.GetDataByRowAndName(row, "Reward1"));
        _reward2 = int.Parse(config.GetDataByRowAndName(row, "Reward2"));
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
            data._item = int.Parse(config.GetDataByRowAndName(row, "Item" + i.ToString()));
            data._count = int.Parse(config.GetDataByRowAndName(row, "Num" + i.ToString()));
            _itemList.Add(data);
        }
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