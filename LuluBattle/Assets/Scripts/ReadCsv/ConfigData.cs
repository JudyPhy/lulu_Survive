using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigData {

    //RoleType
    public Dictionary<int, RoleTypeData> RoleTypeDict = new Dictionary<int, RoleTypeData>();

    public void LoadConfigs() {
        //RoleType
        ReadCsv config = new ReadCsv("RoleType");
        for (int i = 3; i < config.GetRow(); i++) {
            RoleTypeData data = new RoleTypeData(config, i);
            this.RoleTypeDict.Add(data._id, data);
        }
    }
}

public class RoleTypeData
{
    public int _id;
    public string _name;
    public string _headIcon;

    public RoleTypeData(ReadCsv config, int row) {
        _id = int.Parse(config.GetDataByRowAndName(row, "ID"));
        _name = config.GetDataByRowAndName(row, "Name");
        _headIcon = config.GetDataByRowAndName(row, "HeadIcon");
    }
}