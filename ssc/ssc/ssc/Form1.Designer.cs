namespace ssc
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_odd = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_result = new System.Windows.Forms.Button();
            this.label_little = new System.Windows.Forms.Label();
            this.label_big = new System.Windows.Forms.Label();
            this.label_even = new System.Windows.Forms.Label();
            this.dayInput = new System.Windows.Forms.TextBox();
            this.label_day = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_day_result = new System.Windows.Forms.Button();
            this.label_day_little = new System.Windows.Forms.Label();
            this.label_day_big = new System.Windows.Forms.Label();
            this.label_day_even = new System.Windows.Forms.Label();
            this.label_day_odd = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_odd
            // 
            this.label_odd.AutoSize = true;
            this.label_odd.Location = new System.Drawing.Point(17, 30);
            this.label_odd.Name = "label_odd";
            this.label_odd.Size = new System.Drawing.Size(29, 12);
            this.label_odd.TabIndex = 0;
            this.label_odd.Text = "单：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_result);
            this.groupBox1.Controls.Add(this.label_little);
            this.groupBox1.Controls.Add(this.label_big);
            this.groupBox1.Controls.Add(this.label_even);
            this.groupBox1.Controls.Add(this.label_odd);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 110);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "全部";
            // 
            // button_result
            // 
            this.button_result.Location = new System.Drawing.Point(19, 81);
            this.button_result.Name = "button_result";
            this.button_result.Size = new System.Drawing.Size(170, 23);
            this.button_result.TabIndex = 2;
            this.button_result.Text = "输出";
            this.button_result.UseVisualStyleBackColor = true;
            this.button_result.Click += new System.EventHandler(this.button_result_Click);
            // 
            // label_little
            // 
            this.label_little.AutoSize = true;
            this.label_little.Location = new System.Drawing.Point(108, 58);
            this.label_little.Name = "label_little";
            this.label_little.Size = new System.Drawing.Size(29, 12);
            this.label_little.TabIndex = 3;
            this.label_little.Text = "小：";
            // 
            // label_big
            // 
            this.label_big.AutoSize = true;
            this.label_big.Location = new System.Drawing.Point(17, 58);
            this.label_big.Name = "label_big";
            this.label_big.Size = new System.Drawing.Size(29, 12);
            this.label_big.TabIndex = 2;
            this.label_big.Text = "大：";
            // 
            // label_even
            // 
            this.label_even.AutoSize = true;
            this.label_even.Location = new System.Drawing.Point(108, 30);
            this.label_even.Name = "label_even";
            this.label_even.Size = new System.Drawing.Size(29, 12);
            this.label_even.TabIndex = 1;
            this.label_even.Text = "双：";
            // 
            // dayInput
            // 
            this.dayInput.Location = new System.Drawing.Point(49, 17);
            this.dayInput.Name = "dayInput";
            this.dayInput.Size = new System.Drawing.Size(94, 21);
            this.dayInput.TabIndex = 2;
            // 
            // label_day
            // 
            this.label_day.AutoSize = true;
            this.label_day.Location = new System.Drawing.Point(14, 22);
            this.label_day.Name = "label_day";
            this.label_day.Size = new System.Drawing.Size(29, 12);
            this.label_day.TabIndex = 3;
            this.label_day.Text = "日期";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.button_day_result);
            this.groupBox2.Controls.Add(this.label_day);
            this.groupBox2.Controls.Add(this.label_day_little);
            this.groupBox2.Controls.Add(this.dayInput);
            this.groupBox2.Controls.Add(this.label_day_even);
            this.groupBox2.Controls.Add(this.label_day_big);
            this.groupBox2.Controls.Add(this.label_day_odd);
            this.groupBox2.Location = new System.Drawing.Point(12, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 153);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "日";
            // 
            // button_day_result
            // 
            this.button_day_result.Location = new System.Drawing.Point(19, 123);
            this.button_day_result.Name = "button_day_result";
            this.button_day_result.Size = new System.Drawing.Size(170, 23);
            this.button_day_result.TabIndex = 6;
            this.button_day_result.Text = "输出";
            this.button_day_result.UseVisualStyleBackColor = true;
            this.button_day_result.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_day_little
            // 
            this.label_day_little.AutoSize = true;
            this.label_day_little.Location = new System.Drawing.Point(108, 100);
            this.label_day_little.Name = "label_day_little";
            this.label_day_little.Size = new System.Drawing.Size(29, 12);
            this.label_day_little.TabIndex = 8;
            this.label_day_little.Text = "小：";
            // 
            // label_day_big
            // 
            this.label_day_big.AutoSize = true;
            this.label_day_big.Location = new System.Drawing.Point(17, 100);
            this.label_day_big.Name = "label_day_big";
            this.label_day_big.Size = new System.Drawing.Size(29, 12);
            this.label_day_big.TabIndex = 7;
            this.label_day_big.Text = "大：";
            // 
            // label_day_even
            // 
            this.label_day_even.AutoSize = true;
            this.label_day_even.Location = new System.Drawing.Point(108, 72);
            this.label_day_even.Name = "label_day_even";
            this.label_day_even.Size = new System.Drawing.Size(29, 12);
            this.label_day_even.TabIndex = 5;
            this.label_day_even.Text = "双：";
            // 
            // label_day_odd
            // 
            this.label_day_odd.AutoSize = true;
            this.label_day_odd.Location = new System.Drawing.Point(17, 72);
            this.label_day_odd.Name = "label_day_odd";
            this.label_day_odd.Size = new System.Drawing.Size(29, 12);
            this.label_day_odd.TabIndex = 4;
            this.label_day_odd.Text = "单：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "如：2018年8月1日格式为\"180801\"";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 317);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_odd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_even;
        private System.Windows.Forms.Label label_little;
        private System.Windows.Forms.Label label_big;
        private System.Windows.Forms.Button button_result;
        private System.Windows.Forms.TextBox dayInput;
        private System.Windows.Forms.Label label_day;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_day_result;
        private System.Windows.Forms.Label label_day_little;
        private System.Windows.Forms.Label label_day_big;
        private System.Windows.Forms.Label label_day_even;
        private System.Windows.Forms.Label label_day_odd;
        private System.Windows.Forms.Label label1;
    }
}

