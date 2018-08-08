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


namespace 监控系统1
{
    public partial class 新建按钮 : Form
    {
        public 新建按钮()
        {
            InitializeComponent();


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Point = this.textBox4.Text;
            string Ip = this.textBox1.Text;
            string Wbt = this.textBox2.Text;

            #region 数据库连接
            string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=QYH)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=QYH)));Persist Security Info=True;User ID=hanlin;Password=745752749;";
            OracleConnection conn = new OracleConnection(connString);
            conn.Open();
            #endregion

            if (Point != "" & Ip != "" & Wbt != "")
            {
                #region 查询语句提取数据
                OracleCommand cmd1 = conn.CreateCommand();
                cmd1.CommandText = "insert into CODING values(" + Point + "," + "'"+ Ip+"'" + ","+"'"+ Wbt +"'"+ ")";              
                #endregion
            }
            else
            {
                MessageBox.Show("请完整输入信息以便添加");
                Point = "";
                Ip = "";
                Wbt = "";
            }
            conn.Close();
        }
    }
}
