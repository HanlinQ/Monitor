using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DBConnection
{
    class DB
    {
        public static string connString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=QYH)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=QYH)));Persist Security Info=True;User ID=hanlin;Password=745752749;";
        OracleConnection conn = new OracleConnection(connString);

        public void ConnOpen()
        {

            if (this.conn.State != ConnectionState.Open)
                this.conn.Open();
        } //打开

        public void ConnClose()
        {
            if (this.conn.State == ConnectionState.Open)
                this.conn.Close();
        } //关闭

        public string Conn()
        {
            conn.Open();
            if (this.conn.State == ConnectionState.Open)
            {
                return "链接正常";
            }
            else
            {
                return "链接失败";
            }
          
        } //DatagridView 第3列

        public void UpdataTable(string Command)//插入数据
        {
            ConnOpen();
            var commandText = Command; 
            using (OracleConnection connection = new OracleConnection(connString))
            {
                using (OracleCommand command = new OracleCommand(commandText, connection))
                {
                  
                    command.Connection.Open();
                   
                    int result = command.ExecuteNonQuery();

                    command.Connection.Close();
                }
            }
         

        }
     
        public DataTable QueryT(string SQLString)
        {
            using (OracleConnection conn = new OracleConnection(connString))
            {
                DataTable ds = new DataTable();
                try
                {
                    conn.Open();
                    OracleDataAdapter command = new OracleDataAdapter(SQLString, conn);
                    command.Fill(ds);
                }
                catch (OracleException e)
                {
                    throw new Exception(e.Message);
                }
                return ds;
            }
        }//带参数提取表数据

        public int TBCount()
        {
            var count = 0;
            var ds = QueryT("select count(*) as count from CODING ");
            foreach (DataRow col in ds.Rows)
            {
                var countStr = col["count"].ToString();
                count = int.Parse(countStr);
            }
            return count;

        }//行数

    }
}
