using System.Collections.Generic;
using System.IO;

public class DataStruct
{
    public float odd;
    public float even;
    public float big;
    public float little;

    public int count_odd;
    public int count_even;
    public int count_big;
    public int count_little;
}

public class ReadCsv
{
    private Dictionary<string, uint> TimeNumDict = new Dictionary<string, uint>();

    public ReadCsv(string csvName)
    {
        string filePath = csvName;
        string[] lineOfArray = File.ReadAllLines(filePath);
        for (int i = 1; i < lineOfArray.Length; i++)
        {
            string[] str = lineOfArray[i].Split(',');
            string time = str[0];
            TimeNumDict.Add(time, uint.Parse(str[str.Length - 1]));
        }
    }

    public int GetRow()
    {
        return TimeNumDict.Count;
    }

    public DataStruct GetAllResult()
    {
        DataStruct data = new DataStruct();
        int count_odd = 0;
        int count_big = 0;
        foreach (uint value in TimeNumDict.Values)
        {
            if (value % 2 != 0)
            {
                count_odd++;
            }
            if (value > 4)
            {
                count_big++;
            }
        }
        data.odd = count_odd * 100.00f / TimeNumDict.Count;
        data.even = (TimeNumDict.Count - count_odd) / TimeNumDict.Count;
        data.big = count_big * 100.00f / TimeNumDict.Count;
        data.little = (TimeNumDict.Count - count_big) / TimeNumDict.Count;

        data.count_odd = count_odd;
        data.count_even = TimeNumDict.Count - count_odd;
        data.count_big = count_big;
        data.count_little = TimeNumDict.Count - count_big;
        return data;
    }

    public DataStruct GetDayResult(string day)
    {
        DataStruct data = new DataStruct();
        int count_day = 0;
        int count_odd = 0;
        int count_big = 0;
        foreach (string key in TimeNumDict.Keys)
        {
            string curDay = key.Trim().Substring(0, 6);
            if (curDay == day.Trim())
            {
                count_day++;
                if (TimeNumDict[key] % 2 != 0)
                {
                    count_odd++;
                }
                if (TimeNumDict[key] > 4)
                {
                    count_big++;
                }
            }
        }
        data.odd = count_odd * 100.00f / count_day;
        data.even = (count_day - count_odd) / count_day;
        data.big = count_big * 100.00f / count_day;
        data.little = (count_day - count_big) / count_day;

        data.count_odd = count_odd;
        data.count_even = count_day - count_odd;
        data.count_big = count_big;
        data.count_little = count_day - count_big;
        return data;
    }

}
