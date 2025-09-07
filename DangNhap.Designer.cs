namespace TrinhDuyet
{
    partial class DangNhap
    {
        private System.ComponentModel.IContainer components = null;

        // Controls chung
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabLogin;
        private System.Windows.Forms.TabPage tabRegister;

        // Login controls
        private System.Windows.Forms.TextBox txtLoginUser;
        private System.Windows.Forms.TextBox txtLoginPass;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.CheckBox chkShowLogin;

        // Register controls
        private System.Windows.Forms.TextBox txtRegUser;
        private System.Windows.Forms.TextBox txtRegPass;
        private System.Windows.Forms.TextBox txtRegPass2;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.CheckBox chkShowReg;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DangNhap));
            tabControl = new TabControl();
            tabLogin = new TabPage();
            pictureBox1 = new PictureBox();
            chkShowLogin = new CheckBox();
            btnLogin = new Button();
            txtLoginPass = new TextBox();
            txtLoginUser = new TextBox();
            lblLU = new Label();
            lblLP = new Label();
            tabRegister = new TabPage();
            chkShowReg = new CheckBox();
            btnRegister = new Button();
            txtRegPass2 = new TextBox();
            txtRegPass = new TextBox();
            txtRegUser = new TextBox();
            lblRU = new Label();
            lblRP = new Label();
            lblRP2 = new Label();
            pictureBox2 = new PictureBox();
            tabControl.SuspendLayout();
            tabLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabLogin);
            tabControl.Controls.Add(tabRegister);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(717, 390);
            tabControl.TabIndex = 0;
            // 
            // tabLogin
            // 
            tabLogin.BackColor = Color.FromArgb(255, 192, 192);
            tabLogin.BackgroundImage = (Image)resources.GetObject("tabLogin.BackgroundImage");
            tabLogin.BackgroundImageLayout = ImageLayout.Stretch;
            tabLogin.Controls.Add(pictureBox1);
            tabLogin.Controls.Add(chkShowLogin);
            tabLogin.Controls.Add(btnLogin);
            tabLogin.Controls.Add(txtLoginPass);
            tabLogin.Controls.Add(txtLoginUser);
            tabLogin.Controls.Add(lblLU);
            tabLogin.Controls.Add(lblLP);
            tabLogin.Location = new Point(4, 29);
            tabLogin.Name = "tabLogin";
            tabLogin.Size = new Size(709, 357);
            tabLogin.TabIndex = 0;
            tabLogin.Text = "Đăng nhập";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(0, 0, 0, 0);
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(25, 72);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 129);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // chkShowLogin
            // 
            chkShowLogin.AutoSize = true;
            chkShowLogin.BackColor = Color.FromArgb(0, 0, 0, 0);
            chkShowLogin.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkShowLogin.Location = new Point(455, 129);
            chkShowLogin.Name = "chkShowLogin";
            chkShowLogin.Size = new Size(146, 27);
            chkShowLogin.TabIndex = 2;
            chkShowLogin.Text = "Hiện mật khẩu";
            chkShowLogin.UseVisualStyleBackColor = false;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.NavajoWhite;
            btnLogin.Location = new Point(249, 180);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(200, 33);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // txtLoginPass
            // 
            txtLoginPass.Location = new Point(249, 127);
            txtLoginPass.Name = "txtLoginPass";
            txtLoginPass.Size = new Size(200, 27);
            txtLoginPass.TabIndex = 1;
            txtLoginPass.UseSystemPasswordChar = true;
            // 
            // txtLoginUser
            // 
            txtLoginUser.Location = new Point(249, 87);
            txtLoginUser.Name = "txtLoginUser";
            txtLoginUser.Size = new Size(200, 27);
            txtLoginUser.TabIndex = 0;
            // 
            // lblLU
            // 
            lblLU.AutoSize = true;
            lblLU.BackColor = Color.FromArgb(0, 0, 0, 0);
            lblLU.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLU.Location = new Point(156, 91);
            lblLU.Name = "lblLU";
            lblLU.Size = new Size(87, 23);
            lblLU.TabIndex = 4;
            lblLU.Text = "Tài khoản:";
            // 
            // lblLP
            // 
            lblLP.AutoSize = true;
            lblLP.BackColor = Color.FromArgb(0, 0, 0, 0);
            lblLP.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLP.Location = new Point(155, 128);
            lblLP.Name = "lblLP";
            lblLP.Size = new Size(88, 23);
            lblLP.TabIndex = 5;
            lblLP.Text = "Mật khẩu:";
            // 
            // tabRegister
            // 
            tabRegister.BackColor = Color.FromArgb(0, 0, 0, 0);
            tabRegister.BackgroundImage = (Image)resources.GetObject("tabRegister.BackgroundImage");
            tabRegister.BackgroundImageLayout = ImageLayout.Stretch;
            tabRegister.Controls.Add(pictureBox2);
            tabRegister.Controls.Add(chkShowReg);
            tabRegister.Controls.Add(btnRegister);
            tabRegister.Controls.Add(txtRegPass2);
            tabRegister.Controls.Add(txtRegPass);
            tabRegister.Controls.Add(txtRegUser);
            tabRegister.Controls.Add(lblRU);
            tabRegister.Controls.Add(lblRP);
            tabRegister.Controls.Add(lblRP2);
            tabRegister.Location = new Point(4, 29);
            tabRegister.Name = "tabRegister";
            tabRegister.Size = new Size(709, 357);
            tabRegister.TabIndex = 1;
            tabRegister.Text = "Đăng ký";
            // 
            // chkShowReg
            // 
            chkShowReg.AutoSize = true;
            chkShowReg.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkShowReg.Location = new Point(460, 156);
            chkShowReg.Name = "chkShowReg";
            chkShowReg.Size = new Size(146, 27);
            chkShowReg.TabIndex = 3;
            chkShowReg.Text = "Hiện mật khẩu";
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.Moccasin;
            btnRegister.Location = new Point(242, 198);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(200, 33);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "Tạo tài khoản";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += BtnRegister_Click;
            // 
            // txtRegPass2
            // 
            txtRegPass2.Location = new Point(242, 153);
            txtRegPass2.Name = "txtRegPass2";
            txtRegPass2.Size = new Size(200, 27);
            txtRegPass2.TabIndex = 2;
            txtRegPass2.UseSystemPasswordChar = true;
            // 
            // txtRegPass
            // 
            txtRegPass.Location = new Point(242, 118);
            txtRegPass.Name = "txtRegPass";
            txtRegPass.Size = new Size(200, 27);
            txtRegPass.TabIndex = 1;
            txtRegPass.UseSystemPasswordChar = true;
            // 
            // txtRegUser
            // 
            txtRegUser.Location = new Point(242, 83);
            txtRegUser.Name = "txtRegUser";
            txtRegUser.Size = new Size(200, 27);
            txtRegUser.TabIndex = 0;
            // 
            // lblRU
            // 
            lblRU.AutoSize = true;
            lblRU.BackColor = Color.FromArgb(0, 0, 0, 0);
            lblRU.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRU.Location = new Point(147, 86);
            lblRU.Name = "lblRU";
            lblRU.Size = new Size(87, 23);
            lblRU.TabIndex = 5;
            lblRU.Text = "Tài khoản:";
            // 
            // lblRP
            // 
            lblRP.AutoSize = true;
            lblRP.BackColor = Color.FromArgb(0, 0, 0, 0);
            lblRP.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRP.Location = new Point(147, 122);
            lblRP.Name = "lblRP";
            lblRP.Size = new Size(88, 23);
            lblRP.TabIndex = 6;
            lblRP.Text = "Mật khẩu:";
            // 
            // lblRP2
            // 
            lblRP2.AutoSize = true;
            lblRP2.BackColor = Color.FromArgb(0, 0, 0, 0);
            lblRP2.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRP2.Location = new Point(147, 153);
            lblRP2.Name = "lblRP2";
            lblRP2.Size = new Size(78, 23);
            lblRP2.TabIndex = 7;
            lblRP2.Text = "Nhập lại:";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(0, 0, 0, 0);
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(16, 83);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(125, 129);
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // DangNhap
            // 
            ClientSize = new Size(717, 390);
            Controls.Add(tabControl);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "DangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập / Đăng ký";
            tabControl.ResumeLayout(false);
            tabLogin.ResumeLayout(false);
            tabLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabRegister.ResumeLayout(false);
            tabRegister.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);

        }
        private Label lblLU;
        private Label lblLP;
        private Label lblRU;
        private Label lblRP;
        private Label lblRP2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}