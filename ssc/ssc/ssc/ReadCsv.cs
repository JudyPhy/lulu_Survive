using System.Collections.Generic;
using System.IO;
using System;

namespace ssc
{
    public class ReadCsv
    {
        private List<Data> DataList = new List<Data>();
        private KillNum Kill;

        public ReadCsv(string csvName)
        {
            string filePath = csvName;
            string[] lineOfArray = File.ReadAllLines(filePath);
            initUnitData(lineOfArray);
            initKillNum(lineOfArray);
        }

        private void initKillNum(string[] lineOfArray)
        {
            Kill = new KillNum();
            for (int i = 0; i < lineOfArray.Length; i++)
            {
                string[] str = lineOfArray[i].Split(',');
                if (str.Length != 6)
                {
                    Console.WriteLine("str.Length error:" + str.Length);
                    continue;
                }
                string time = str[0].Trim().Substring(0, 6);
                int term = int.Parse(str[0].Trim().Substring(6));
                int[] nums = new int[5];
                nums[0] = int.Parse(str[1].Trim());
                nums[1] = int.Parse(str[2].Trim());
                nums[2] = int.Parse(str[3].Trim());
                nums[3] = int.Parse(str[4].Trim());
                nums[4] = int.Parse(str[5].Trim());

                bool find = false;
                for (int n = 0; n < Kill.termNum.Count; n++)
                {
                    if (Kill.termNum[n].time == time && Kill.termNum[n].term == term)
                    {
                        find = true;
                        break;
                    }                    
                }
                if (!find)
                {
                    KillTerm data = new KillTerm(time, term, nums);
                    Kill.termNum.Add(data);
                }
            }
        }

        private void initUnitData(string[] lineOfArray)
        {
            for (int i = 0; i < lineOfArray.Length; i++)
            {
                string[] str = lineOfArray[i].Split(',');
                string time = str[0].Trim().Substring(0, 6);
                int term = int.Parse(str[0].Trim().Substring(6));
                uint num = uint.Parse(str[str.Length - 1].Trim());

                bool find = false;
                for (int n = 0; n < DataList.Count; n++)
                {
                    if (DataList[n].time == time)
                    {
                        find = true;
                        DataList[n].termNum.Add(new Term(term, num));
                        break;
                    }
                }
                if (!find)
                {
                    Data data = new Data();
                    data.time = time;
                    data.termNum.Add(new Term(term, num));
                    DataList.Add(data);
                }
            }
        }

        public List<string> GetDays()
        {
            List<string> result = new List<string>();
            for (int i = 0; i < DataList.Count; i++)
            {
                result.Add(DataList[i].time);
            }
            return result;
        }

        public Data GetDayResult(string day)
        {
            for (int i = 0; i < DataList.Count; i++)
            {
                if (DataList[i].time == day)
                    return DataList[i];
            }
            return null;
        }

        public List<AfterInfo> GetAfterInfo(string day)
        {
            for (int i = 0; i < DataList.Count; i++)
            {
                if (DataList[i].time == day)
                    return DataList[i].GetAfterInfo();
            }
            return null;
        }

        public List<KillTerm> GetRecentTenData()
        {
            return Kill.GetRecentTenData();
        }

        public List<int> GetKilledNum()
        {
            return Kill.GetKilledNum();
        }

        public string GetKilledTime()
        {
            return Kill.killedTerm;
        }

    }
}
