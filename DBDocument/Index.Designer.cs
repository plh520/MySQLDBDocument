namespace DBDocument
{
    partial class Index
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_SQLServer = new System.Windows.Forms.Button();
            this.btn_MySql = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_SQLServer
            // 
            this.btn_SQLServer.Location = new System.Drawing.Point(92, 51);
            this.btn_SQLServer.Name = "btn_SQLServer";
            this.btn_SQLServer.Size = new System.Drawing.Size(131, 23);
            this.btn_SQLServer.TabIndex = 0;
            this.btn_SQLServer.Text = "SQL  Server";
            this.btn_SQLServer.UseVisualStyleBackColor = true;
            this.btn_SQLServer.Click += new System.EventHandler(this.btn_SQLServer_Click);
            // 
            // btn_MySql
            // 
            this.btn_MySql.Location = new System.Drawing.Point(92, 103);
            this.btn_MySql.Name = "btn_MySql";
            this.btn_MySql.Size = new System.Drawing.Size(131, 23);
            this.btn_MySql.TabIndex = 1;
            this.btn_MySql.Text = "MySql";
            this.btn_MySql.UseVisualStyleBackColor = true;
            this.btn_MySql.Click += new System.EventHandler(this.btn_MySql_Click);
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 197);
            this.Controls.Add(this.btn_MySql);
            this.Controls.Add(this.btn_SQLServer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Index";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Index";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_SQLServer;
        private System.Windows.Forms.Button btn_MySql;
    }
}