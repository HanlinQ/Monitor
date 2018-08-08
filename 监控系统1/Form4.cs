using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBConnection;

namespace 监控系统1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();//实例化DB类

            //var ds = db.QueryT("select IP,POINT from WBT_STATUS "); //用DB类方法查找列数据

            String IP = textBox1.Text;
            String POINT = textBox2.Text;

            db.UpdataTable("Insert into WBT_STATUS (IP,POINT) VALUES ('" + IP + "','" + POINT + "')");
            //IP，状态，时间

        }
    }
}
