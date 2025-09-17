using System;
using System.IO;
using System.Windows.Forms;

namespace TrinhDuyet
{
    public partial class UserInfo : Form
    {
        public string username = "userclient";

        public UserInfo()
        {
            InitializeComponent();

            // Cài đặt form ở giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.White; // Màu nền trắng

            // Chỉ giữ nút đóng
            this.ControlBox = true;
            this.MaximizeBox = false; // Ẩn nút tối đa
            this.MinimizeBox = false; // Ẩn nút thu nhỏ
            GetUsername();
            label1.Text = $"Xin chào, {username}";
            this.Text = username;
        }

        private void GetUsername()
        {
            var cfg = ConfigManager.Load();
            username = cfg.Username;
        }
    }
}
