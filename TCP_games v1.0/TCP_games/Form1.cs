using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Net;//匯入網路通訊協定相關函數
using System.Net.Sockets;//匯入網路插座功能函數
using System.Threading;//匯入多執行緒功能函數
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.PowerPacks;

namespace TCP_games
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        #region 公用變數
        readonly string[] card ={
            "牛奶","咖啡","黑咖啡","茶","紅茶","綠茶","冰紅茶","青草茶","烏龍茶","擂茶","珍珠奶茶"
            ,"檸檬汁","甘蔗汁","酸梅汁","楊桃汁","椰子","西瓜","蓮霧","香蕉","葡萄","木瓜","鳳梨"
            ,"水梨","榴槤","草莓","蘋果","奇異果","荔枝","龍眼","火龍果","橘子","哈密瓜","櫻桃","芭樂"
            ,"水蜜桃","檸檬","芒果","香瓜","李子","文旦","包心菜","紫色包心菜","蔥","芹菜","紅蘿蔔"
            ,"辣椒","黃瓜","蒜頭","小紅蘿蔔","菠菜","空心菜","白木耳","玉米粒","豆芽","蘆筍","山芋"
            ,"花椰菜","大白菜","薑","大蔥","萵苣","蘑菇","豌豆","馬鈴薯","冬瓜","芋頭","橘子","洋蔥"
            ,"芥菜","橄欖","金針菇","四季豆","甜菜","茄子","結球菜心","荸薺","白花菜","地瓜","番茄"};
        Socket T;//通訊物件
        Thread Th;//網路監聽執行緒
        string User, Ans="", DMaster;
        string[] player;
        public int user_scro = 0;
        int p_num = 0,Count;//判斷Draw
        #endregion
        //--------------------物件-------------------------//
        public Form1()
        {
            InitializeComponent();
            _lastCustomColors = new int[]  //color plate
            {
                ColorToInt(Color.Red),
                ColorToInt(Color.Blue),
                ColorToInt(Color.Gray)
            };
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            C = new ShapeContainer();//建立畫布(本機繪圖用)
            D = new ShapeContainer();//建立畫布(本機繪圖用)
            gm_btu.Enabled  = false;
            oth_btu.Enabled = false;
            Draw_panel.Controls.Add(C);
            button1.Enabled = false;
            for (int i = 0; i < ZZ.Length; i++) ZZ[i] = "0";
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Send("9" + User); //傳送自己的離線訊息給伺服器
                if(T !=null) T.Close(); //關閉網路通訊器  
            }
            catch { }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false; //忽略跨執行緒操作的錯誤
            User = textBox3.Text;  //使用者名稱
            string IP = textBox1.Text;//伺服器IP   
            int Port = int.Parse(textBox2.Text);  //伺服器Port
            try
            {
                if (button1.Text.Equals("Sign in"))
                {
                    IPEndPoint EP = new IPEndPoint(IPAddress.Parse(IP), Port);//建立伺服器端點資訊
                    T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    T.Connect(EP); //連上Server的EP端點(類似撥號連線)
                    Th = new Thread(Listen)
                    {
                        IsBackground = true //設定為背景執行緒
                    }; //建立監聽執行緒
                    Th.Start(); //開始監聽
                    TextBox5.AppendText("(系統) : 已連線伺服器！ \r\n");
                    Send("L" + User);
                    button1.Text = "Sign out";
                }
                else if (button1.Text.Equals("Sign out"))
                {
                    Send("9" + User);
                    T.Close(); //關閉網路通訊器
                    metroButton3.Show();
                    timer1.Stop();
                    metroProgressBar1.Value = 100;
                    button1.Text = "Sign in";
                    gm_btu.Enabled = false;
                    oth_btu.Enabled = false;
                }
            }
            catch
            {
                  metroButton3.Show();
                  timer1.Stop();
                  metroProgressBar1.Value = 100;
                  button1.Text = "Sign in";
                  gm_btu.Enabled = false;
                  oth_btu.Enabled = false;
                  TextBox5.AppendText("(系統) : " + User + " 無法連上伺服器！ \r\n");
            }
        }
        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button1_Click(this, new EventArgs());
                e.SuppressKeyPress = true;
            }    
        }
        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "") button1.Enabled = true;
        }
        //聊天室
        private void MetroButton1_Click(object sender, EventArgs e)
        {
            if (TextBox6.Text == "") return;
            if (listBox1.SelectedIndex < 0) 
            {
                if (Ans.Equals(TextBox6.Text)) Send("D" + " 系統 : 禁止傳解答!!");
                else Send("D" + User + "：" + TextBox6.Text);
            }
            else
            {
                Send("3" + "來自" + User + ": " + TextBox6.Text + "|" + listBox1.SelectedItem);
                TextBox5.Text += "私密" + listBox1.SelectedItem + "： " + TextBox6.Text + "\r\n";
            }
            TextBox6.Text = ""; //清除發言框
        }
        private void TextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MetroButton1_Click(this,new EventArgs());
                e.SuppressKeyPress = true; //SuppressKeyPress和Hanld都可以设置
            }
        }
        //答案區
        private void MetroButton2_Click(object sender, EventArgs e)
        {
            if (TextBox8.Text == "") return;
            if (listBox1.SelectedIndex < 0)
            {
                if (Ans.Equals(TextBox8.Text))
                {
                    Send("M" + User + ": Hit");
                    user_scro += this.metroProgressBar1.Value / 3;
                    Send("S" + User + ":" + user_scro);
                }
                else Send("M" + User + ":" + TextBox8.Text);
            }
            TextBox8.Text = "";
        }
        private void TextBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MetroButton2_Click(this,new EventArgs());
                e.SuppressKeyPress = true; //SuppressKeyPress和Hanld都可以设置
            }
        }

        //------------------Sub------------------------//
        private void Send(string Str)
        {
            try {
                byte[] B = Encoding.Default.GetBytes(Str); //翻譯文字成Byte陣列
                if(T !=null) T.Send(B, 0, B.Length, SocketFlags.None); //傳送訊息給伺服器
            }
            catch { }
        }
        private void Listen()
        {
            EndPoint ServerEP = (EndPoint)T.RemoteEndPoint; 
            byte[] B = new byte[1023]; 
            int inLen = 0; //接收的位元組數目
            while (true)
            {
                try
                {
                    inLen = T.ReceiveFrom(B, ref ServerEP);//收聽資訊並取得位元組數
                }
                catch (Exception)
                {
                    T.Close();
                    listBox1.Items.Clear();//清除線上名單
                    MessageBox.Show("伺服器斷線了！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);//顯示斷線
                    TextBox5.AppendText("(系統) : 伺服器已斷線了！ \r\n");
                    button1.Enabled = true;//連線按鍵恢復可用
                    Th.Abort();//刪除執行緒
                }

                string Msg = Encoding.Default.GetString(B, 0, inLen); //解讀完整訊息
                string St = Msg.Substring(0, 1); //取出命令碼 (第一個字)
                string Str = Msg.Substring(1); //取出命令碼之後的訊息   
                switch (St)//依命令碼執行功能
                {
                    case "L"://接收線上名單
                        listBox1.Items.Clear(); //清除名單
                        string[] M = Str.Split(','); //拆解名單成陣列
                        player = M;
                        for (int i = 0; i < M.Length; i++) listBox1.Items.Add(M[i]); //逐一加入名單
                        break;
                    case "S":
                        Count--;
                        TextBox9.AppendText(Str + "\r\n");
                        TextBox9.AppendText(DMaster+": +10" + "\r\n");
                        if (DMaster == User) user_scro += 10;
                        break;
                    case "D":TextBox5.AppendText("(公開)" + Str + "\r\n"); break;//聊天室
                    case "3": TextBox5.AppendText("(私密)" + Str + "\r\n"); break;//私密訊息
                    case "M": TextBox7.AppendText(Str + "\r\n"); break;
                    case "P": PointShape(Str); break;
                    case "A": Ans = Str; break;
                    case "C": D.Shapes.Clear(); break;
                    case "G": Game(); break;
                    case "R": metroButton3.Show(); break;
                }
            }
        }
        private int RandomNum()
        {
            int temp = 0;
            Random crandom = new Random();
            for (int i = 0; i < card.Length; i++)
            {
                temp = crandom.Next(0, card.Length);
            }
            return temp;
        }
        private void Game()
        {
            if (this.metroProgressBar1.Value == 0)
            {
                timer1.Stop();
                if (player[p_num] != User) oth_btu.Enabled = true;
                else gm_btu.Enabled = true;
                metroProgressBar1.Value = 100;
                C.Shapes.Clear();
                Draw_panel.Controls.Clear();
            }
            if (user_scro >= 100)
            {
                timer1.Stop();
                string[] win_play = TextBox9.Text.Split(':');
                Send("M" + "(系統)" + win_play[0] + ":(WINER) Done");
                Send("R");
                metroProgressBar1.Value = 100;
            }

        }

        //----VB power pack----//
        # region 繪圖相關變數宣告
        ShapeContainer C,D;
        Point stP;//繪圖起點
        string p;//筆畫座標字串
        readonly string[] ZZ = new string[3]; // for color plate RGB bands
        private int[] _lastCustomColors = new int[16]; //color plate
        int LineWidth = 1;//線粗細
        #endregion
        //繪圖動作
        private void MetroPanel2_MouseUp(object sender, MouseEventArgs e)
        {
             p = LineWidth+"-"+ ZZ[0] + "*" + ZZ[1] + "*" + ZZ[2] + "_" + p;
             Send("P" + p); 
        }
        private void MetroPanel2_MouseDown(object sender, MouseEventArgs e)
        {
            stP = e.Location;//起點
            p = stP.X.ToString() + "," + stP.Y.ToString();//起點座標紀錄
        }
        private void MetroPanel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _ = new LineShape
                {
                    StartPoint = stP,//線段起點
                    EndPoint = e.Location,//線段終點
                    BorderWidth = LineWidth,
                    BorderColor = pictureBox1.BackColor,//設定畫筆顏色
                    Parent = C  //線段加入畫布C
                };//建立線段物件

                stP = e.Location;//終點變起點
                p += "/" + stP.X.ToString() + "," + stP.Y.ToString();//持續紀錄座標
            }
        }
        private void PointShape(string  str) 
        {
            string[] W = str.Split('-');
            string[] Z = W[1].Split('_');
            string[] Z1 =Z[0].Split('*'); //切割顏色RGB   
            string[] Q = Z[1].Split('/');//切割座標點資訊
            LineWidth = Int32.Parse(W[0]);
            Point[] R = new Point[Q.Length];//宣告座標點陣列
            try 
            {
                try 
                {
                    for (int i = 0; i < Q.Length; i++)
                    {
                        string[] K = Q[i].Split(',');//切割X與Y座標
                        if(K[1] =="") K = Q[0].Split(',');
                        R[i].X = int.Parse(K[0]);//定義第i點X座標
                        R[i].Y = int.Parse(K[1]);//定義第i點Y座標
                    }
                } catch (IndexOutOfRangeException) {
                    for (int i = 0; i < 1; i++)
                    {
                        string[] K = Q[0].Split(',');//切割X與Y座標
                        R[Q.Length-1].X = int.Parse(K[0]);//定義第i點X座標
                        R[Q.Length-1].Y = int.Parse(K[1]);//定義第i點Y座標
                    }
                }

                for (int i = 0; i < Q.Length - 1; i++)
                {
                    LineShape L = new LineShape
                    {
                        StartPoint = R[i],//線段起點
                        EndPoint = R[i + 1],//線段終點
                        BorderWidth = LineWidth
                };//建立線段物件
                    int r = Convert.ToInt32(Z1[0]);
                    int g = Convert.ToInt32(Z1[1]);
                    int b = Convert.ToInt32(Z1[2]);
                    L.BorderColor = Color.FromArgb(r, g, b);
                    L.Parent = D;//線段L加入畫布D(遠端使用者繪圖)
                    
                }
            } catch (InvalidOperationException){ }
        }
        private void Color_btu_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlgColor = new ColorDialog())
            {
                dlgColor.FullOpen = true;
                dlgColor.CustomColors = _lastCustomColors;
                if (dlgColor.ShowDialog() == DialogResult.OK)
                {
                    _lastCustomColors = dlgColor.CustomColors;
                    pictureBox1.BackColor = dlgColor.Color; //讀取色盤顏色給畫筆
                    ZZ[0] = pictureBox1.BackColor.R.ToString();
                    ZZ[1] = pictureBox1.BackColor.G.ToString();
                    ZZ[2] = pictureBox1.BackColor.B.ToString();
                }
            }
        }
        private static int ColorToInt(Color color)  //color plate
        {
            return (color.R) | (color.G << 8) | (color.G << 16);
        }
        private void MetroButton3_Click(object sender, EventArgs e)
        {
            user_scro = 0;
            TextBox9.Text = "";
            if (listBox1.Items.Count > 1) 
            {
                metroButton3.Hide();
                gm_btu.Enabled = true;
                oth_btu.Enabled = true;
                if (player[p_num] != User) gm_btu.Enabled = false;
                else oth_btu.Enabled = false;
            }
            else TextBox5.AppendText("(系統) : 必須要兩個人以上的玩家 。 \r\n");
        }
            
        //清除圖
        private void Button2_Click(object sender, EventArgs e)
        {
            C.Shapes.Clear();
            Send("C");
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:  // 線寬=1
                    LineWidth = 1;
                    break;
                case 2:  // 線寬=2
                    LineWidth = 2;
                    break;
                case 3:  // 線寬=3
                    LineWidth = 3;
                    break;
                case 4:  // 線寬=4
                    LineWidth = 4;
                    break;
                case 5:  // 線寬=5
                    LineWidth = 5;
                    break;
                case 6:  // 線寬=6
                    LineWidth = 6;
                    break;
                case 7:  // 線寬=7
                    LineWidth = 7;
                    break;
                case 8:  // 線寬=8
                    LineWidth = 8;
                    break;
                case 9:  // 線寬=9
                    LineWidth = 9;
                    break;
                default: // 線寬=1
                    LineWidth = 1;
                    break;
            }
        }

        //載入時間
        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            if (metroProgressBar1.Value > 0) {
                metroProgressBar1.Value--;
                if (Count == 1)
                {
                    Game();
                    Count = listBox1.Items.Count;
                    timer1.Stop();
                    if (player[p_num] != User) oth_btu.Enabled = true;
                    else gm_btu.Enabled = true;
                    metroProgressBar1.Value = 100;
                    C.Shapes.Clear();
                    Draw_panel.Controls.Clear();
                }
            }
            else Game();
        }
        private void Gm_btu_Click(object sender, EventArgs e)
        {
            Count = listBox1.Items.Count;
            Send("G" + player[p_num]);
            DMaster = player[p_num];
            p_num++;
            if (p_num > listBox1.Items.Count - 1) p_num = 0;
            gm_btu.Enabled = false;
            TextBox8.Enabled = false;

            timer1.Start();
            metroLabel3.Text = card[RandomNum()];
            Send("A" + metroLabel3.Text); //傳送題目
            Draw_panel.Enabled = true;
            Draw_panel.Controls.Clear();
            Draw_panel.Controls.Add(C);
        }
        private void Oth_btu_Click(object sender, EventArgs e)
        {
            Count = listBox1.Items.Count;
            DMaster = player[p_num];
            for (int i = 0; i < listBox1.Items.Count - 1; i++) Send("G" + player[i]); 
            p_num++;
            if (p_num > listBox1.Items.Count - 1) p_num = 0;
            oth_btu.Enabled = false;
            TextBox8.Enabled = true;
            
            timer1.Start();
            D.Shapes.Clear();
            Draw_panel.Controls.Clear();
            Draw_panel.Controls.Add(D);
            metroLabel3.Text = "";
            Draw_panel.Enabled = false;
        }

    }
}
