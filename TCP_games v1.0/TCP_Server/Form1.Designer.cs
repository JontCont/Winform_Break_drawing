namespace TCP_Server
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Listbox1 = new System.Windows.Forms.ListBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(109, 77);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(122, 22);
            this.TextBox1.TabIndex = 67;
            this.TextBox1.Text = "127.0.0.1";
            // 
            // Listbox1
            // 
            this.Listbox1.FormattingEnabled = true;
            this.Listbox1.ItemHeight = 12;
            this.Listbox1.Location = new System.Drawing.Point(260, 34);
            this.Listbox1.Name = "Listbox1";
            this.Listbox1.Size = new System.Drawing.Size(114, 148);
            this.Listbox1.TabIndex = 66;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(279, 10);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(65, 12);
            this.Label3.TabIndex = 65;
            this.Label3.Text = "線上使用者";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(44, 108);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(57, 12);
            this.Label2.TabIndex = 64;
            this.Label2.Text = "Server Port";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(53, 80);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(48, 12);
            this.Label1.TabIndex = 63;
            this.Label1.Text = "Server IP";
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(109, 105);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(122, 22);
            this.TextBox2.TabIndex = 62;
            this.TextBox2.Text = "2013";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(13, 10);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(106, 34);
            this.Button1.TabIndex = 61;
            this.Button1.Text = "啟動";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(125, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 34);
            this.button2.TabIndex = 68;
            this.button2.Text = "關閉";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 198);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Listbox1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.Button1);
            this.Name = "Form1";
            this.Text = "TCP Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.ListBox Listbox1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Button button2;
    }
}

