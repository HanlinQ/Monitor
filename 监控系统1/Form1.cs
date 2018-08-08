using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VncSharp;
using Oracle.ManagedDataAccess.Client;
using System.Net.NetworkInformation;
using System.Net;
using System.Timers;
using DBConnection;
using System.Diagnostics;

namespace 监控系统1
{

    public partial class Form1 : Form
    {
        DB db = new DB();
        PING PI = new PING();     

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan; //控件显示不同颜色
            this.listBox1.Items.Add("不通设备信息：");    
        }

    

        public void Form1_Load(object sender, EventArgs e)
        {
            Creatbutton();
            ChangColor();
            var thread = new Thread(new ThreadStart(
               () =>
           {
                while (true)
            {
                
           
                   PI.Ping();
                  ChangColor();
                  AddListBox();

                   Thread.Sleep(1000);
            }

           }));
            thread.IsBackground = true;
            thread.Start();



        }

 

        #region 循环创建BUTTON

        public void  Creatbutton()
        {
            var ds2 = db.QueryT("select IP,STATUS,POINT from WBT_STATUS");        
            foreach (DataRow col in ds2.Rows)
            {               
                Button btn = new Button();
                btn.Text = col["IP"].ToString();
                btn.Name = col["POINT"].ToString();
                btn.Width = 85;
                btn.Height = 45;
                btn.Top = 3;
                btn.Left = 3;
                this.flowLayoutPanel1.Controls.Add(btn);              
            }  
        }

        #endregion


       #region 读取DB状态改变BUTTON颜色

        public void  ChangColor()
        {
            var ds2 = db.QueryT("select IP,STATUS,POINT from WBT_STATUS");
            foreach (DataRow col in ds2.Rows)
            {
                if (col["STATUS"].ToString() == "0")
                {
                    this.flowLayoutPanel1.Controls[col["POINT"].ToString()].BackColor = Color.DeepSkyBlue;
                    this.flowLayoutPanel1.Controls[col["POINT"].ToString()].ForeColor = Color.White;
                    this.flowLayoutPanel1.Controls[col["POINT"].ToString()].Click += delegate { Form2 form2 = new Form2(flowLayoutPanel1.Controls[col["POINT"].ToString()].Text); form2.Show(); }; //跳到VNC链接WBT 
                }
                else
                {
                    this.flowLayoutPanel1.Controls[col["POINT"].ToString()].BackColor = Color.Red;
                    this.flowLayoutPanel1.Controls[col["POINT"].ToString()].Click += delegate { MessageBox.Show("此WBT网络中断请检查\n\r" + "位置:" + this.flowLayoutPanel1.Controls[col["POINT"].ToString()].Name); };//如果失败向数据库插入一条语句   IP  连接状态   时间
                }
            }


            }

        #endregion


        #region ListBox添加数据

        public void AddListBox()
        {               
            var ds2 = db.QueryT("select IP,STATUS,POINT from WBT_STATUS");
            foreach (DataRow col in ds2.Rows)
            {
                if (col["STATUS"].ToString() == "1")
                {
                    this.listBox1.Items.Add("设备IP:" + col["IP"].ToString() +"                  连接失败");
                }             
             }
       }

        #endregion


        #region 历史记录

        private void 历史纪录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        #endregion


        #region 控件方法

        public void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



        }
        private void 更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void 更新WBTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }



        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.Controls.Clear();
            Creatbutton();
            
        } //更新按键

        private void 新建ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
           
           this.flowLayoutPanel1.Controls.Clear();
            Creatbutton();

        //    Thread t1 = new Thread(new ThreadStart(PI.Ping));
        //    t1.IsBackground = true;
        //    t1.Start();

        //    ChangColor();
        //    AddListBox(); 
                
        }
    }

    #region
   // int index = this.dataGridView1.Rows.Add();
   //         this.dataGridView1.Rows[index].Cells[0].Value = t1.ThreadState + "                         " + DateTime.Now.ToString();//PING状态 
   //         this.dataGridView1.Rows[index].Cells[2].Value = db.Conn() + "                         " + DateTime.Now.ToString(); //数据库链接状态   
    //        dataGridView1.InvalidateRow(0);
    #endregion

    }










