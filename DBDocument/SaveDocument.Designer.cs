namespace DBDocument
{
    partial class SaveDocument
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_dataSource = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_PORT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_uid = new System.Windows.Forms.TextBox();
            this.txt_pwd = new System.Windows.Forms.TextBox();
            this.cmb_DBList = new System.Windows.Forms.ComboBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.brn_re = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_SaveHTML = new System.Windows.Forms.Button();
            this.btn_SaveWord = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lv_tables = new System.Windows.Forms.ListView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lv_TableInfo = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_SaveWordPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_SaveHtmlPath = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "主 机：";
            // 
            // txt_dataSource
            // 
            this.txt_dataSource.Location = new System.Drawing.Point(104, 38);
            this.txt_dataSource.Name = "txt_dataSource";
            this.txt_dataSource.Size = new System.Drawing.Size(214, 25);
            this.txt_dataSource.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(370, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口号：";
            // 
            // txt_PORT
            // 
            this.txt_PORT.Location = new System.Drawing.Point(443, 38);
            this.txt_PORT.Name = "txt_PORT";
            this.txt_PORT.Size = new System.Drawing.Size(100, 25);
            this.txt_PORT.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "用户名：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(370, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "密 码：";
            // 
            // txt_uid
            // 
            this.txt_uid.Location = new System.Drawing.Point(105, 91);
            this.txt_uid.Name = "txt_uid";
            this.txt_uid.Size = new System.Drawing.Size(214, 25);
            this.txt_uid.TabIndex = 6;
            // 
            // txt_pwd
            // 
            this.txt_pwd.Location = new System.Drawing.Point(443, 92);
            this.txt_pwd.Name = "txt_pwd";
            this.txt_pwd.PasswordChar = '*';
            this.txt_pwd.Size = new System.Drawing.Size(214, 25);
            this.txt_pwd.TabIndex = 7;
            // 
            // cmb_DBList
            // 
            this.cmb_DBList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DBList.FormattingEnabled = true;
            this.cmb_DBList.Location = new System.Drawing.Point(145, 47);
            this.cmb_DBList.Name = "cmb_DBList";
            this.cmb_DBList.Size = new System.Drawing.Size(266, 23);
            this.cmb_DBList.TabIndex = 8;
            this.cmb_DBList.SelectedIndexChanged += new System.EventHandler(this.cmb_DBList_SelectedIndexChanged);
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(770, 83);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(75, 38);
            this.btn_login.TabIndex = 9;
            this.btn_login.Text = "登 录";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.brn_re);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_login);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_dataSource);
            this.groupBox1.Controls.Add(this.txt_pwd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_uid);
            this.groupBox1.Controls.Add(this.txt_PORT);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1237, 148);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // brn_re
            // 
            this.brn_re.Location = new System.Drawing.Point(868, 83);
            this.brn_re.Name = "brn_re";
            this.brn_re.Size = new System.Drawing.Size(75, 38);
            this.brn_re.TabIndex = 10;
            this.brn_re.Text = "重置";
            this.brn_re.UseVisualStyleBackColor = true;
            this.brn_re.Click += new System.EventHandler(this.brn_re_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_SaveHtmlPath);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txt_SaveWordPath);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btn_SaveHTML);
            this.groupBox2.Controls.Add(this.btn_SaveWord);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmb_DBList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1237, 259);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据库列表";
            // 
            // btn_SaveHTML
            // 
            this.btn_SaveHTML.Location = new System.Drawing.Point(443, 149);
            this.btn_SaveHTML.Name = "btn_SaveHTML";
            this.btn_SaveHTML.Size = new System.Drawing.Size(133, 41);
            this.btn_SaveHTML.TabIndex = 11;
            this.btn_SaveHTML.Text = "导出HTML";
            this.btn_SaveHTML.UseVisualStyleBackColor = true;
            this.btn_SaveHTML.Click += new System.EventHandler(this.btn_SaveHTML_Click);
            // 
            // btn_SaveWord
            // 
            this.btn_SaveWord.Location = new System.Drawing.Point(443, 87);
            this.btn_SaveWord.Name = "btn_SaveWord";
            this.btn_SaveWord.Size = new System.Drawing.Size(133, 41);
            this.btn_SaveWord.TabIndex = 10;
            this.btn_SaveWord.Text = "导出word文档";
            this.btn_SaveWord.UseVisualStyleBackColor = true;
            this.btn_SaveWord.Click += new System.EventHandler(this.btn_SaveWord_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "数据库名称：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lv_tables);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(0, 407);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(411, 309);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "表列表";
            // 
            // lv_tables
            // 
            this.lv_tables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_tables.HideSelection = false;
            this.lv_tables.Location = new System.Drawing.Point(3, 21);
            this.lv_tables.Name = "lv_tables";
            this.lv_tables.Size = new System.Drawing.Size(405, 285);
            this.lv_tables.TabIndex = 0;
            this.lv_tables.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lv_TableInfo);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(411, 407);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(826, 309);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "表结构";
            // 
            // lv_TableInfo
            // 
            this.lv_TableInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_TableInfo.HideSelection = false;
            this.lv_TableInfo.Location = new System.Drawing.Point(3, 21);
            this.lv_TableInfo.Name = "lv_TableInfo";
            this.lv_TableInfo.Size = new System.Drawing.Size(820, 285);
            this.lv_TableInfo.TabIndex = 0;
            this.lv_TableInfo.UseCompatibleStateImageBehavior = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "word导出路径：";
            // 
            // txt_SaveWordPath
            // 
            this.txt_SaveWordPath.Location = new System.Drawing.Point(158, 101);
            this.txt_SaveWordPath.Name = "txt_SaveWordPath";
            this.txt_SaveWordPath.Size = new System.Drawing.Size(245, 25);
            this.txt_SaveWordPath.TabIndex = 13;
            this.txt_SaveWordPath.Click += new System.EventHandler(this.textBox1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "HTML导出路径：";
            // 
            // txt_SaveHtmlPath
            // 
            this.txt_SaveHtmlPath.Location = new System.Drawing.Point(157, 159);
            this.txt_SaveHtmlPath.Name = "txt_SaveHtmlPath";
            this.txt_SaveHtmlPath.Size = new System.Drawing.Size(245, 25);
            this.txt_SaveHtmlPath.TabIndex = 15;
            this.txt_SaveHtmlPath.Click += new System.EventHandler(this.txt_SaveHtmlPath_Click);
            // 
            // SaveDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 716);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveDocument";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生成数据库文档";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_dataSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_PORT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_uid;
        private System.Windows.Forms.TextBox txt_pwd;
        private System.Windows.Forms.ComboBox cmb_DBList;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button brn_re;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_SaveWord;
        private System.Windows.Forms.ListView lv_tables;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListView lv_TableInfo;
        private System.Windows.Forms.Button btn_SaveHTML;
        private System.Windows.Forms.TextBox txt_SaveWordPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_SaveHtmlPath;
    }
}

