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
    public partial class DangNhap : Form
    {
        private readonly UserStore _store;
        public string Username { get; private set; }
        public string Password { get; private set; }
        public DangNhap(UserStore store)
        {
            _store = store;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Không cho thay đổi kích thước
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = true;
            this.MaximizeBox = false; // Ẩn nút tối đa
            this.MinimizeBox = false; // Ẩn nút thu nhỏ
            InitializeComponent();
            this.chkShowLogin.CheckedChanged += (s, e) =>
            {
                this.txtLoginPass.UseSystemPasswordChar = !this.chkShowLogin.Checked;
            };
            this.chkShowReg.CheckedChanged += (s, e) =>
            {
                this.txtRegPass.UseSystemPasswordChar = !this.chkShowReg.Checked;
                this.txtRegPass2.UseSystemPasswordChar = !this.chkShowReg.Checked;
            };
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (_store.Login(txtLoginUser.Text.Trim(), txtLoginPass.Text, out var err))
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // báo thành công
                Username = txtLoginUser.Text;
                Password = txtLoginPass.Text;
                this.Close();
                // TODO: Mở form chính
            }
            else
            {
                MessageBox.Show(err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (txtRegPass.Text != txtRegPass2.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_store.Register(txtRegUser.Text.Trim(), txtRegPass.Text, out var err))
            {
                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // báo thành công
                Username = txtRegUser.Text;
                Password = txtRegPass.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show(err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
