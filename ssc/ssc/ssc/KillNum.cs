using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssc
{
    public class KillTerm
    {
        public string time;
        public int term;
        public int[] nums;

        public KillTerm(string value0, int value1, int[] value2)
        {
            time = value0;
            term = value1;
            nums = value2;
        }
    }

    public class KillNum
    {
        
        public List<KillTerm> termNum = new List<KillTerm>();

        public string killedTerm;
        public List<int> killedNum = new List<int>();

        public List<int> GetKilledNum()
        {
            if (termNum.Count < 10)
            {
                Console.WriteLine("data count error:" + termNum.Count);
                return null;
            }
            termNum.Sort((data1, data2) =>
            {
                if (data2.term < data1.term)
                    return -1;
                else if (data2.term > data1.term)
                    return 1;
                else
                    return 0;
            });

            killedNum.Clear();
            int num = SumLast();
            killedNum.Add(num);

            num = LastAndLastNine();
            if (!HasNum(num))
                killedNum.Add(num);

            num = LastAndLastAndLastFive();
            if (!HasNum(num))
                killedNum.Add(num);

            int nextTerm = termNum[0].term + 1;
            killedTerm = termNum[0].time + nextTerm.ToString("000");
            return killedNum;
        }

        private int SumLast()
        {
            int sum = 0;
            for (int i = 0; i < termNum[0].nums.Length; i++)
            {
                sum += termNum[0].nums[i];
            }
            return sum % 10;
        }

        private int LastAndLastNine()
        {
            int add = termNum[0].nums[4] + termNum[8].nums[4];
            return add % 10;
        }

        private int LastAndLastAndLastFive()
        {
            int add = termNum[0].nums[4] + termNum[1].nums[4] + termNum[4].nums[4];
            return add % 10;
        }

        public List<KillTerm> GetRecentTenData()
        {
            if (termNum.Count < 10)
            {
                Console.WriteLine("data count error:" + termNum.Count);
                return null;
            }
            termNum.Sort((data1, data2) =>
            {
                if (data2.term < data1.term)
                    return -1;
                else if (data2.term > data1.term)
                    return 1;
                else
                    return 0;
            });

            List<KillTerm> result = new List<KillTerm>();
            for (int i = 0; i < Math.Min(10, termNum.Count); i++)
            {
                result.Add(termNum[i]);
            }
            return result;
        }

        private bool HasNum(int num)
        {
            for (int i = 0; i < killedNum.Count; i++)
            {
                if (killedNum[i] == num)
                    return true;
            }
            return false;
        }

    }
}
