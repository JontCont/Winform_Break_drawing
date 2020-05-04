namespace GameMSN
{
    partial class fServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fServer));
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Listbox1 = new System.Windows.Forms.ListBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.start_btu = new System.Windows.Forms.Button();
            this.cls_btu = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(137, 116);
            this.TextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(161, 25);
            this.TextBox1.TabIndex = 67;
            this.TextBox1.Text = "127.0.0.1";
            // 
            // Listbox1
            // 
            this.Listbox1.FormattingEnabled = true;
            this.Listbox1.ItemHeight = 15;
            this.Listbox1.Location = new System.Drawing.Point(8, 26);
            this.Listbox1.Margin = new System.Windows.Forms.Padding(4);
            this.Listbox1.Name = "Listbox1";
            this.Listbox1.Size = new System.Drawing.Size(180, 154);
            this.Listbox1.TabIndex = 66;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(51, 155);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(72, 15);
            this.Label2.TabIndex = 64;
            this.Label2.Text = "Server Port";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(63, 120);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(61, 15);
            this.Label1.TabIndex = 63;
            this.Label1.Text = "Server IP";
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(137, 151);
            this.TextBox2.Margin = new System.Windows.Forms.Padding(4);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(161, 25);
            this.TextBox2.TabIndex = 62;
            this.TextBox2.Text = "2013";
            // 
            // start_btu
            // 
            this.start_btu.Location = new System.Drawing.Point(16, 39);
            this.start_btu.Margin = new System.Windows.Forms.Padding(4);
            this.start_btu.Name = "start_btu";
            this.start_btu.Size = new System.Drawing.Size(141, 42);
            this.start_btu.TabIndex = 61;
            this.start_btu.Text = "啟動";
            this.start_btu.UseVisualStyleBackColor = true;
            this.start_btu.Click += new System.EventHandler(this.Button1_Click);
            // 
            // cls_btu
            // 
            this.cls_btu.Location = new System.Drawing.Point(165, 39);
            this.cls_btu.Margin = new System.Windows.Forms.Padding(4);
            this.cls_btu.Name = "cls_btu";
            this.cls_btu.Size = new System.Drawing.Size(141, 42);
            this.cls_btu.TabIndex = 68;
            this.cls_btu.Text = "關閉";
            this.cls_btu.UseVisualStyleBackColor = true;
            this.cls_btu.Click += new System.EventHandler(this.Button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Listbox1);
            this.groupBox1.Location = new System.Drawing.Point(332, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(197, 199);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "線上使用者";
            // 
            // fServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 230);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cls_btu);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.start_btu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fServer";
            this.Text = "TCP Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FServer_FormClosing);
            this.Load += new System.EventHandler(this.FServer_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.ListBox Listbox1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.Button start_btu;
        private System.Windows.Forms.Button cls_btu;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
