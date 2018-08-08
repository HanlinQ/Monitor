using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VncSharp;
using Oracle.ManagedDataAccess.Client;

namespace 监控系统1
{
    public partial class Form2 : Form
    {
        private static string GetPassword()
        {
            return "#CB1W#V";
        } 

        public Form2(string host)
        {

            InitializeComponent();
            rd.GetPassword = new AuthenticateDelegate(GetPassword);
            rd.Connect(host);
          
        } 

        
   

    }
}
