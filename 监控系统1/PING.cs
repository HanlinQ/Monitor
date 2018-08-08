using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using DBConnection;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Drawing;

namespace 监控系统1
{
    class PING
    {

        #region IP监控
      
        public void Ping()//PING
        {
            DB db = new DB();//实例化DB类
            Ping ping = new Ping();//实例化PING的类


            var ds = db.QueryT("select IP,POINT from WBT_STATUS "); //用DB类方法查找列数据
            String status = "";
            foreach (DataRow col in ds.Rows) //循环遍历每行数据
            {             
                PingReply reply = ping.Send(IPAddress.Parse(col["IP"].ToString()));

                if (reply.Status == IPStatus.Success ) //成功 对应IP写入STATUS列值=0
                {
                    db.UpdataTable("UPDATE WBT_STATUS SET STATUS = 0 WHERE IP ='"+col["IP"].ToString()+"'");
                    status = "Open";
                }
                else  //失败则写入STATUS列=1
                {
                    db.UpdataTable("UPDATE WBT_STATUS SET STATUS = 1 WHERE IP ='"+col["IP"].ToString()+"'");
                    status = "Close";
                    
                }
                db.UpdataTable("Insert into LOG (IP,STATUS,TIME) VALUES ('" + col["IP"].ToString() + "','" + status + "',SYSDATE)");
            }
            
            db.ConnClose();
        }

        #endregion

    }
}
