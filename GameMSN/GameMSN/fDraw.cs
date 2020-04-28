using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;//匯入網路通訊協定相關函數
using System.Net.Sockets;//匯入網路插座功能函數
using System.Threading;//匯入多執行緒功能函數
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.PowerPacks;
using Microsoft.VisualBasic.ApplicationServices;

namespace GameMSN
{

    public partial class fDraw : Form
    {
        public fDraw()
        {
            InitializeComponent();
        }
        #region 繼承Net資料
        private string _Dip;
        private string _Dport;
        private string _Dname;
        public string Netip { get { return _Dip; } set { _Dip = value; } }
        public string Netport { get { return _Dport; } set { _Dport = value; } }
        public string Netname { get { return _Dname; } set { _Dname = value; } }
        Socket T;
        Thread Th;
        #endregion
        #region 繪畫功能
        private ShapeContainer C, D; 
        Point stP;
        string p;
        private int LineWidth = 1;
        private string Shape = "L";
        readonly string[] ZZ = new string[3]; // for color plate RGB bands
        private int[] _lastCustomColors = new int[16]; //color plate

        private static int ColorToInt(Color color)  //color plate
        {
            return (color.R) | (color.G << 8) | (color.G << 16);
        }
        private void CreateShapes()
        {
            C = new ShapeContainer();//建立畫布(本機繪圖用)
            D = new ShapeContainer();//建立畫布(本機繪圖用)
            Draw_panel1.Controls.Add(C);
            Draw_panel1.Controls.Add(D);
            for (int i = 0; i < ZZ.Length; i++) ZZ[i] = "0";
        }

        private void Draw_panel1_MouseUp(object sender, MouseEventArgs e)
        {
            string[] tmpe1 = p.Split('/');
            Point[] R = new Point[tmpe1.Length];
            for (int i = 0; i < tmpe1.Length; i++)
            {
                string[] K = tmpe1[i].Split(',');//切割X與Y座標
                if (K[1] == "") K = tmpe1[0].Split(',');
                R[i].X = int.Parse(K[0]);//定義第i點X座標
                R[i].Y = int.Parse(K[1]);//定義第i點Y座標
            }
            switch (Shape)
            {
                case "R":
                    RectangleShape Rect = new RectangleShape
                    {
                        Left = R[0].X,//線段起點
                        Top = R[0].Y,
                        Width = R[tmpe1.Length - 1].X - R[0].X,//線段終點
                        Height = R[tmpe1.Length - 1].Y - R[0].Y,
                        BorderWidth = LineWidth,
                        BorderColor = pictureBox1.BackColor//設定畫筆顏色
                    };//建立線段物件
                    Rect.Parent = C;
                    break;
                case "O":
                    OvalShape Oval = new OvalShape
                    {
                        Left = R[0].X,//線段起點
                        Top = R[0].Y,
                        Width = R[tmpe1.Length - 1].X - R[0].X,//線段終點
                        Height = R[tmpe1.Length - 1].Y - R[0].Y,
                        BorderWidth = LineWidth,
                        BorderColor = pictureBox1.BackColor//設定畫筆顏色
                    };//建立線段物件
                    Oval.Parent = C;
                    break;
            }
            p = Shape + "|" + LineWidth + "-" + ZZ[0] + "*" + ZZ[1] + "*" + ZZ[2] + "_" + p;
            Send("P" + p);
        }
        private void Draw_panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if(Shape.Equals("L"))
                {
                    LineShape L = new LineShape
                    {
                        StartPoint = stP,//線段起點
                        EndPoint = e.Location,//線段終點
                        BorderWidth = LineWidth,
                        BorderColor = pictureBox1.BackColor,//設定畫筆顏色  
                    };//建立線段物件
                    L.Parent = C;
                }
                stP = e.Location;
                p += "/" + stP.X.ToString() + "," + stP.Y.ToString();//持續紀錄座標
            }
        }
        private void Draw_panel1_MouseDown(object sender, MouseEventArgs e)
        {
            stP = e.Location;//起點
            p = stP.X.ToString() + "," + stP.Y.ToString();//起點座標紀錄
        }//起點座標紀錄
        private void PointShape(string str) 
        {
            string[] S = str.Split('|');    //形狀 
            string[] W = S[1].Split('-');    //LineWidth 
            string[] Z = W[1].Split('_');   // RGB p
            string[] Z1 = Z[0].Split('*'); //切割顏色RGB   
            string[] Q = Z[1].Split('/');   //切割座標點資訊
            LineWidth = Int32.Parse(W[0]);
            Point[] R = new Point[Q.Length];//宣告座標點陣列
            try
            {
                try
                {
                    for (int i = 0; i < Q.Length; i++)
                    {
                        string[] K = Q[i].Split(',');//切割X與Y座標
                        if (K[1] == "") K = Q[0].Split(',');
                        R[i].X = int.Parse(K[0]);//定義第i點X座標
                        R[i].Y = int.Parse(K[1]);//定義第i點Y座標
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        string[] K = Q[0].Split(',');//切割X與Y座標
                        R[Q.Length - 1].X = int.Parse(K[0]);//定義第i點X座標
                        R[Q.Length - 1].Y = int.Parse(K[1]);//定義第i點Y座標
                    }
                }
                int r = Convert.ToInt32(Z1[0]);
                int g = Convert.ToInt32(Z1[1]);
                int b = Convert.ToInt32(Z1[2]);
                switch (S[0])
                {
                    case "R":
                        _ = new RectangleShape()
                        {
                            Left = R[0].X,
                            Top = R[0].Y,
                            Width = R[Q.Length - 1].X - R[0].X,
                            Height = R[Q.Length - 1].Y - R[0].Y,
                            BorderWidth = LineWidth,
                            BorderColor = Color.FromArgb(r, g, b),
                            Parent = D
                        };
                        break;
                    case "O":
                        _ = new OvalShape()
                        {
                            Left = R[0].X,
                            Top = R[0].Y,
                            Width = R[Q.Length - 1].X - R[0].X,
                            Height = R[Q.Length - 1].Y - R[0].Y,
                            BorderWidth = LineWidth,
                            BorderColor = Color.FromArgb(r, g, b),
                            Parent = D
                        };
                        break;
                    case "L":
                        for (int i = 0; i < Q.Length - 1; i++)
                        {
                            _ = new LineShape
                            {
                                StartPoint = R[i],//線段起點
                                EndPoint = R[i + 1],//線段終點
                                BorderWidth = LineWidth,
                                BorderColor = Color.FromArgb(r, g, b),
                                Parent = D
                            };//建立線段物件
                        }
                        break;
                }
            }
            catch (InvalidOperationException) { }
        }
        private void Paletter_btu_MouseHover(object sender, EventArgs e)
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(Paletter_btu, "調色盤");
        }//調色盤
        private void Paletter_btu_Click(object sender, EventArgs e)
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
        }//rgb
        private void Cls_btu_Click(object sender, EventArgs e)
        {
            Send("C");
        } //cls
        private void Cls_btu_MouseHover(object sender, EventArgs e)
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(Cls_btu, "清除");
        } //cls
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0) LineWidth = comboBox1.SelectedIndex;
            else LineWidth = 1;
            switch (comboBox1.SelectedIndex+1) 
            {
                case 2: LineWidth = 5; break;
                case 3: LineWidth = 7; break;
                case 4: LineWidth = 10; break;
                case 5: LineWidth = 12; break;
                default: LineWidth = 1; break;
            }
        } //線粗細
        private void Round_btu_Click(object sender, EventArgs e)
        {
            Shape = "O";
        }
        private void Square_btu_Click(object sender, EventArgs e)
        {
            Shape = "R";
        }
        private void Line_btu_Click(object sender, EventArgs e)
        {
            Shape = "L";
        }
        private void Draw_panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion
        #region 聊天室chat
        //聊天室chat 
        private void Chat_btu_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "") return;
            if (listBox1.SelectedIndex < 0)
            {
                //if (Ans.Equals(TextBox6.Text)) Send("D" + " 系統 : 禁止傳解答!!");
                //else 
                Send("D" + Netname + "：" + textBox3.Text);
            }
            else
            {
                Send("3" + "來自" + Netname + ": " + textBox3.Text + "|" + listBox1.SelectedItem);
                textBox1.Text += "私密" + listBox1.SelectedItem + "： " + textBox3.Text + "\r\n";
            }
            textBox3.Text = ""; //清除發言框
        }
        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Chat_btu_Click(this, new EventArgs());
                e.SuppressKeyPress = true; //SuppressKeyPress和Hanld都可以设置
            }
        }
        //答案區
        private void Guess_btu_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "") return;
            if (listBox1.SelectedIndex < 0)
            {
                if (Ans.Equals(textBox4.Text))
                {
                    Send("M" + Netname + ": Hit");
                    user_scro += this.progressBar1.Value / 3;
                    Send("S" + Netname + ":" + user_scro +"分");
                }
                else   Send("M" + Netname + ":" + textBox4.Text);
            }
            textBox4.Text = "";
        }
        private void TextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                Guess_btu_Click(this,new EventArgs());
                e.SuppressKeyPress = true;
            }
        }
        #endregion

        private void FDraw_Load(object sender, EventArgs e)
        {
            Connect(Netip, Netport, Netname);
            CreateShapes();
            _lastCustomColors = new int[]  //color plate
            {   ColorToInt(Color.Red),
                ColorToInt(Color.Blue),
                ColorToInt(Color.Gray)
            };
            label4.Text = Netname;
        }
        private void Exit_btu_Click(object sender, EventArgs e)
        {
            try
            {
                fLogin login = new fLogin()
                {
                    Netip = Netip,
                    Netport = Netport,
                    Netname = Netname
                };
                login.Show();
                Send("9" + Netname); //傳送自己的離線訊息給伺服器
                if (T != null) T.Close(); //關閉網路通訊器  
                this.Close();
            }
            catch { }
        }
        private void Connect(string IP, string Port, string Name)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false; //忽略跨執行緒操作的錯誤
                IPEndPoint EP = new IPEndPoint(IPAddress.Parse(IP), int.Parse(Port));//建立伺服器端點資訊
                T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                T.Connect(EP);
                Th = new Thread(Listen) { IsBackground = true };
                Send("L" + Name);
                Th.Start();
                textBox1.AppendText("(系統) : 已連線伺服器！ \r\n");
            }
            catch 
            {
                fLogin login = new fLogin()
                {
                    Netip = Netip,
                    Netport = Netport,
                    Netname = Netname
                };
                login.Show();
                textBox1.AppendText("(系統) : " + Name + " 無法連上伺服器！ \r\n");
                this.Close();
            }
        }
        private void Send(string Str)
        {
            try
            {
                byte[] B = Encoding.Default.GetBytes(Str); //翻譯文字成Byte陣列
                if (T != null) T.Send(B, 0, B.Length, SocketFlags.None); //傳送訊息給伺服器
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
                catch(Exception)
                {
                    T.Close();
                    listBox1.Items.Clear();//清除線上名單
                    MessageBox.Show("伺服器斷線了！", "注意", MessageBoxButtons.OK, MessageBoxIcon.Information);//顯示斷線
                    Th.Abort();//刪除執行緒
                }
                Recetor(B, inLen);
            }
        }
        private void Recetor(byte[] B,int inLen) 
        {
            string Msg = Encoding.Default.GetString(B, 0, inLen); //解讀完整訊息
            string St = Msg.Substring(0, 1); //取出命令碼 (第一個字)
            string Str = Msg.Substring(1); //取出命令碼之後的訊息   
            switch (St)//依命令碼執行功能
            {
                case "L"://接收線上名單
                    listBox1.Items.Clear(); Player.Clear();//清除名單
                    string[] M = Str.Split(','); //拆解名單成陣列
                    for (int i = 0; i < M.Length; i++) 
                    {
                        Player.Add(M[i]);
                        listBox1.Items.Add(M[i]+":  "+user_scro+"分"); 
                    } //逐一加入名單
                    break;  
                case "9": //接收離開玩家
                    textBox1.AppendText("(系統) :"+Str+" 已離開伺服器！ \r\n");
                    Player.Remove(Str);
                    listBox1.Items.Remove(Str);
                    break;
                case "3": textBox1.AppendText("(私密)" + Str + "\r\n"); break;//私密訊息
                case "C":  C.Shapes.Clear();D.Shapes.Clear(); break;
                case "P": PointShape(Str); break;
                case "A": Ans = Str; break;
                case "G": 
                    Game(); 
                    textBox1.AppendText("(系統) :"+Str+" Draw"); 
                    break;
                case "D": textBox1.AppendText("(公開)" + Str + "\r\n"); break;//聊天室
                case "M": textBox2.AppendText(Str + "\r\n"); break;
                case "R": Start_btu.Show(); break;
                case "S":
                    Count++;
                    //textBox3.AppendText(Str + "\r\n");
                    listBox1.Items.Remove(Str);
                    listBox1.Items.Add(Str);
                    //textBox3.AppendText(Player[p_num]+ ": +10" + "\r\n");
                    if (p_num > Player.Count - 1) p_num = 0;
                    if (Player[p_num] == Netname) user_scro += 10;
                    break;
            }
        } //接收ServerC回傳

        private void Game()
        {
            if (this.progressBar1.Value == 0)
            {
                timer1.Stop();
                if (Player[p_num] != Netname) Oth_btu.Enabled = true;
                else Gm_btu.Enabled = true;
                C.Shapes.Clear();
                Draw_panel1.Controls.Clear();
                Count = 0;
            }
            if (user_scro >= 100)
            {
                timer1.Stop();
                string[] win_play = textBox3.Text.Split(':');
                Send("R");
                Send("M" + "(系統)" + win_play[0] + ":(WINER) Done");
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


        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            if (progressBar1.Value > 0)
            {
                progressBar1.Value--;
                if (Player.Count-1 == Count)
                {
                    Game();
                    progressBar1.Value = 100;
                    timer1.Stop();
                    if (Player[p_num] == Netname) Gm_btu.Enabled = true;
                    else Oth_btu.Enabled = true;
                    C.Shapes.Clear();
                    Draw_panel1.Controls.Clear();
                    Count = 0;
                }
            }
            else Game();
        }
        int p_num = 0,Count=0, user_scro=0;
        string Ans;//答案
        List<string> Player= new List<string>();

        private void Gm_btu_Click(object sender, EventArgs e)
        {
            p_num++;
            if (p_num > Player.Count - 1) p_num = 0;
            Gm_btu.Enabled = false;
            textBox4.Enabled = false;

            timer1.Start();
            label5.Text = card[RandomNum()];
            Send("A" + label5.Text); //傳送題目
            Draw_panel1.Enabled = true;
            Draw_panel1.Controls.Clear();
            Draw_panel1.Controls.Add(C);
        }


        private void Oth_btu_Click(object sender, EventArgs e)
        {
            p_num++;
            if (p_num > Player.Count - 1) p_num = 0;
            Oth_btu.Enabled = false;
            textBox4.Enabled = true;

            timer1.Start();
            D.Shapes.Clear();
            Draw_panel1.Controls.Clear();
            Draw_panel1.Controls.Add(D);
            Draw_panel1.Enabled = false;
            label5.Text = "";
        }

        private void Start_btu_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 1)
            {
                Start_btu.Hide();
                Gm_btu.Enabled = Oth_btu.Enabled = false;
                if (Player[p_num] == Netname) Gm_btu.Enabled = true;
                else Oth_btu.Enabled = true;
            }
            else textBox1.AppendText("(系統) : 必須要兩個人以上的玩家 。 \r\n");
        }

        readonly string[] card ={
            "牛奶","咖啡","黑咖啡","茶","紅茶","綠茶","冰紅茶","青草茶","烏龍茶","擂茶","珍珠奶茶"
            ,"檸檬汁","甘蔗汁","酸梅汁","楊桃汁","椰子","西瓜","蓮霧","香蕉","葡萄","木瓜","鳳梨"
            ,"水梨","榴槤","草莓","蘋果","奇異果","荔枝","龍眼","火龍果","橘子","哈密瓜","櫻桃","芭樂"
            ,"水蜜桃","檸檬","芒果","香瓜","李子","文旦","包心菜","紫色包心菜","蔥","芹菜","紅蘿蔔"
            ,"辣椒","黃瓜","蒜頭","小紅蘿蔔","菠菜","空心菜","白木耳","玉米粒","豆芽","蘆筍","山芋"
            ,"花椰菜","大白菜","薑","大蔥","萵苣","蘑菇","豌豆","馬鈴薯","冬瓜","芋頭","橘子","洋蔥"
            ,"芥菜","橄欖","金針菇","四季豆","甜菜","茄子","結球菜心","荸薺","白花菜","地瓜","番茄"};

    }

}
