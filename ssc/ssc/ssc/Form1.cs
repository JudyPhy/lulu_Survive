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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_result_Click(object sender, EventArgs e)
        {
            ReadCsv csv = new ReadCsv("ssc.csv");
            DataStruct result = csv.GetAllResult();
            label_odd.Text = "单：" + result.odd.ToString("00.00") + "%";
            label_even.Text = "双：" + result.even.ToString("00.00") + "%";
            label_big.Text = "大：" + result.big.ToString("00.00") + "%";
            label_little.Text = "小：" + result.little.ToString("00.00") + "%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReadCsv csv = new ReadCsv("ssc.csv");
            string day = dayInput.Text;
            DataStruct result = csv.GetDayResult(day);
            label_day_odd.Text = "单：" + result.odd.ToString("00.00") + "%";
            label_day_even.Text = "双：" + result.even.ToString("00.00") + "%";
            label_day_big.Text = "大：" + result.big.ToString("00.00") + "%";
            label_day_little.Text = "小：" + result.little.ToString("00.00") + "%";
        }
    }

}
