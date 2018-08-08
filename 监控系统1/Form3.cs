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
    
    public partial class Form3 : Form
    {
        DB db = new DB();
        public Form3()
        {
            InitializeComponent();
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan; //控件显示不同颜色
            ComboItem();
        }


        #region 循环遍历表中时间项

        public void ComboItem()
        {
            var ds= db.QueryT("SELECT DISTINCT to_char(TIME,'yyyy-mm-dd hh24') AS TIME FROM LOG ORDER BY TIME DESC ");
            foreach (DataRow col in ds.Rows)
            {              
                this.comboBox1.Items.Add(col["TIME"].ToString()); 
            }
        }

        #endregion


        #region 点击时间在表中寻找对应IP及状态并显示在DataGridView

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ds = db.QueryT("SELECT IP,STATUS FROM LOG WHERE TIME like TO_DATE('" + comboBox1.SelectedItem.ToString() + "','yyyy-mm-dd hh24') ORDER BY IP ASC");
            dataGridView1.DataSource = ds;
        }

        #endregion



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }
    }
}
