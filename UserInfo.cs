using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrinhDuyet
{
    public partial class UserInfo : Form
    {
        public string username = string.Empty;
        public UserInfo()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            // Không cho thay đổi kích thước
            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            InitializeComponent();
            GetUsername();
            label1.Text = $"Xin chào, {username}";
        }
        private void GetUsername()
        {
            string[] Info = File.ReadAllLines("User.data");
            if(Info.Length > 0)
                username = Info[0];
        }
    }
}
