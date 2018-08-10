using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssc
{
    public partial class Form1 : Form
    {
        private ReadCsv FileDatas;

        private string selectedDay;
        private uint selectedAfterNum;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileDatas = new ReadCsv("ssc.txt");
            List<string> days = FileDatas.GetDays();
            for (int i = 0; i < days.Count; i++)
            {
                dataDays.Items.Add(days[i]);
            }

            initKillUI();
        }

        private void initKillUI()
        {
            recentNums.Items.Clear();
            List<KillTerm> recentData = FileDatas.GetRecentTenData();
            if (recentData == null)
                return;
            for (int i = 0; i < recentData.Count; i++)
            {
                string str = recentData[i].time + recentData[i].term.ToString("000")
                    + ": " + recentData[i].nums[0]
                    + recentData[i].nums[1]
                    + recentData[i].nums[2]
                    + recentData[i].nums[3]
                    + recentData[i].nums[4];
                recentNums.Items.Add(str);
            }

            List<int> nums = FileDatas.GetKilledNum();
            string str_nums = "";
            for (int i = 0; i < nums.Count; i++)
            {
                str_nums += nums[i] + " ";
            }
            label_nums.Text = FileDatas.GetKilledTime() + ": " + str_nums;
        }

        private void dataDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedDay = dataDays.SelectedItem.ToString();
            Data result = FileDatas.GetDayResult(selectedDay);
            label_day_odd.Text = "单：" + result.Count_Odd + "(" + result.Count_Odd_Rate.ToString("00.00") + "%)";
            label_day_even.Text = "双：" + result.Count_Even + "(" + result.Count_Even_Rate.ToString("00.00") + "%)";
            label_day_big.Text = "大：" + result.Count_Big + "(" + result.Count_Big_Rate.ToString("00.00") + "%)";
            label_day_little.Text = "小：" + result.Count_Little + "(" + result.Count_Little_Rate.ToString("00.00") + "%)";
            label_0396.Text = "0369组：" + result.Count_0369 + "(" + result.Count_0369_Rate.ToString("00.00") + "%)";
            label_258.Text = "258组：" + result.Count_258 + "(" + result.Count_258_Rate.ToString("00.00") + "%)";
            label_147.Text = "147组：" + result.Count_147 + "(" + result.Count_147_Rate.ToString("00.00") + "%)";

            afterCount.Items.Clear();
            updateAfterCount(selectedDay);
        }

        private void updateAfterCount(string day)
        {
            List<AfterInfo> list = FileDatas.GetAfterInfo(day);
            for (int i = 0; i < list.Count; i++)
            {
                string num = list[i].key.ToString() + "(" + list[i].keyCount + "): ";
                string desc = "";
                for (int n = 0; n < list[i].maxCountNum.Length; n++)
                {
                    uint key = list[i].maxCountNum[n];
                    int count = list[i].afternum_count[key];
                    desc += key + "(" + count + ") ";
                }
                
                afterCount.Items.Add(num + desc);
            }
        }

        private void afterCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedAfterNum = (uint)afterCount.SelectedIndex;
            afterNumAll.Items.Clear();
            List<AfterInfo> list = FileDatas.GetAfterInfo(selectedDay);
            for (int i = 0; i < list.Count; i++)
            {
                if (selectedAfterNum == list[i].key)
                {
                    foreach (uint key in list[i].afternum_count.Keys)
                    {
                        string str0 = key.ToString() + "(";
                        string str1 = list[i].afternum_count[key].ToString() + ")";
                        string str = str0 + str1;
                        afterNumAll.Items.Add(str);
                    }                    
                    break;
                }
            }
        }
    }

}
