using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssc
{
    public class Term
    {
        public int term;
        public uint num;

        public Term(int value1, uint value2)
        {
            term = value1;
            num = value2;
        }
    }

    public class AfterInfo
    {
        public uint key;
        public int keyCount;
        public Dictionary<uint, int> afternum_count = new Dictionary<uint, int>();
        public uint[] maxCountNum = new uint[3];
    }

    public class Data
    {
        public string time;
        public List<Term> termNum = new List<Term>();

        public int Count_Odd
        {
            get
            {
                int count = 0;
                for (int i = 0; i < termNum.Count; i++)
                {
                    if (termNum[i].num % 2 != 0)
                        count++;
                }
                return count;
            }
        }

        public float Count_Odd_Rate
        {
            get
            {
                return Count_Odd * 100.00f / termNum.Count;
            }
        }

        public int Count_Even
        {
            get
            {
                int count = 0;
                for (int i = 0; i < termNum.Count; i++)
                {
                    if (termNum[i].num % 2 == 0)
                        count++;
                }
                return count;
            }
        }

        public float Count_Even_Rate
        {
            get
            {
                return Count_Even * 100.00f / termNum.Count;
            }
        }

        public int Count_Big
        {
            get
            {
                int count = 0;
                for (int i = 0; i < termNum.Count; i++)
                {
                    if (termNum[i].num > 4)
                        count++;
                }
                return count;
            }
        }

        public float Count_Big_Rate
        {
            get
            {
                return Count_Big * 100.00f / termNum.Count;
            }
        }

        public int Count_Little
        {
            get
            {
                int count = 0;
                for (int i = 0; i < termNum.Count; i++)
                {
                    if (termNum[i].num < 5)
                        count++;
                }
                return count;
            }
        }

        public float Count_Little_Rate
        {
            get
            {
                return Count_Little * 100.00f / termNum.Count;
            }
        }

        public int Count_0369
        {
            get
            {
                int count = 0;
                for (int i = 0; i < termNum.Count; i++)
                {
                    if (termNum[i].num == 0 || termNum[i].num == 3 || termNum[i].num == 6 || termNum[i].num == 9)
                        count++;
                }
                return count;
            }
        }

        public float Count_0369_Rate
        {
            get
            {
                return Count_0369 * 100.00f / termNum.Count;
            }
        }

        public int Count_258
        {
            get
            {
                int count = 0;
                for (int i = 0; i < termNum.Count; i++)
                {
                    if (termNum[i].num == 2 || termNum[i].num == 5 || termNum[i].num == 8)
                        count++;
                }
                return count;
            }
        }

        public float Count_258_Rate
        {
            get
            {
                return Count_258 * 100.00f / termNum.Count;
            }
        }

        public int Count_147
        {
            get
            {
                int count = 0;
                for (int i = 0; i < termNum.Count; i++)
                {
                    if (termNum[i].num == 1 || termNum[i].num == 4 || termNum[i].num == 7)
                        count++;
                }
                return count;
            }
        }

        public float Count_147_Rate
        {
            get
            {
                return Count_147 * 100.00f / termNum.Count;
            }
        }

        private uint getMaxNum(Dictionary<uint, int> dict)
        {
            uint num = 10;
            int count = 0;
            foreach (uint key in dict.Keys)
            {
                if (dict[key] >= count)
                {
                    num = key;
                    count = dict[key];
                }
            }
            return num;
        }

        private int getCount(uint value)
        {
            int result = 0;
            for (int i = 0; i < termNum.Count; i++)
            {
                if (termNum[i].num == value)
                {
                    result++;
                }
            }
            return result;
        }

        public List<AfterInfo> GetAfterInfo()
        {
            List<AfterInfo> result = new List<AfterInfo>();
            for (uint i = 0; i < 10; i++)
            {
                AfterInfo info = new AfterInfo();
                info.key = i;
                info.keyCount = getCount(i);
                info.afternum_count = afterNum(i);
                
                Dictionary<uint, int> temp = new Dictionary<uint, int>(info.afternum_count);
                for (int n = 0; n < 3; n++)
                {
                    info.maxCountNum[n] = getMaxNum(temp);
                    temp.Remove(info.maxCountNum[n]);
                }

                result.Add(info);
            }
            return result;
        }
        
        //当前期号码value，下一期开出各号码的概率
        private Dictionary<uint, int> afterNum(uint value)
        {
            Dictionary<uint, int> num_count = new Dictionary<uint, int>();
            termNum.Sort((data1, data2) =>
            {
                if (data2.term < data1.term)
                    return 1;
                else if (data2.term > data1.term)
                    return -1;
                else
                    return 0;
            });
            for (uint key = 0; key < 10; key++)
            {
                num_count.Add(key, 0);
                for (int i = 0; i < termNum.Count; i++)
                {
                    if (termNum[i].num == value && i + 1 < termNum.Count && termNum[i + 1].num == key)
                    {
                        num_count[key]++;
                    }
                }
            }
            return num_count;
        }

    }

}
