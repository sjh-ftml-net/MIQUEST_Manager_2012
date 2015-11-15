using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilSpace;

namespace WindowsFormsApplication1
{
    public partial class HomeForm : Form
    {

        private Utils cUtils = new Utils();

        public HomeForm()
        {
            InitializeComponent();
        }

        private void if_bt_generate_Click(object sender, EventArgs e)
        {
            string lic = if_dtp_code.ToString() + ";" + if_tb_name;

            if_tb_lic.Text = cUtils.EncryptStringAES(lic, "TOPSECRET");

        }
    }
}
