namespace TrinhDuyet
{
    partial class MainWebForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWebForm));
            mainWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            txtUrl = new TextBox();
            topPanel = new Panel();
            userIcon = new PictureBox();
            bookmarkIcon = new PictureBox();
            menuBtn = new PictureBox();
            backBtn = new PictureBox();
            forwardBtn = new PictureBox();
            reloadBtn = new PictureBox();
            homeIcon = new PictureBox();
            toolTip = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)mainWebView).BeginInit();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)userIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bookmarkIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)menuBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)backBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)forwardBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)reloadBtn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)homeIcon).BeginInit();
            SuspendLayout();
            // 
            // mainWebView
            // 
            mainWebView.AllowExternalDrop = true;
            mainWebView.CreationProperties = null;
            mainWebView.DefaultBackgroundColor = Color.White;
            mainWebView.Dock = DockStyle.Fill;
            mainWebView.Location = new Point(0, 42);
            mainWebView.Margin = new Padding(3, 2, 3, 2);
            mainWebView.Name = "mainWebView";
            mainWebView.Size = new Size(1225, 573);
            mainWebView.TabIndex = 2;
            mainWebView.ZoomFactor = 1D;
            // 
            // txtUrl
            // 
            txtUrl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtUrl.Font = new Font("Segoe UI", 12F);
            txtUrl.Location = new Point(138, 9);
            txtUrl.Margin = new Padding(3, 2, 3, 2);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(992, 29);
            txtUrl.TabIndex = 0;
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.MistyRose;
            topPanel.Controls.Add(userIcon);
            topPanel.Controls.Add(bookmarkIcon);
            topPanel.Controls.Add(menuBtn);
            topPanel.Controls.Add(backBtn);
            topPanel.Controls.Add(forwardBtn);
            topPanel.Controls.Add(reloadBtn);
            topPanel.Controls.Add(homeIcon);
            topPanel.Controls.Add(txtUrl);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Margin = new Padding(3, 2, 3, 2);
            topPanel.Name = "topPanel";
            topPanel.Padding = new Padding(9, 8, 9, 8);
            topPanel.Size = new Size(1225, 42);
            topPanel.TabIndex = 3;
            // 
            // userIcon
            // 
            userIcon.Anchor = AnchorStyles.Right;
            userIcon.Image = Properties.Resources.user;
            userIcon.Location = new Point(1166, 12);
            userIcon.Margin = new Padding(3, 2, 3, 2);
            userIcon.Name = "userIcon";
            userIcon.Size = new Size(24, 24);
            userIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            userIcon.TabIndex = 7;
            userIcon.TabStop = false;
            toolTip.SetToolTip(userIcon, "Đăng nhập");
            userIcon.Click += userIcon_Click;
            // 
            // bookmarkIcon
            // 
            bookmarkIcon.Anchor = AnchorStyles.Right;
            bookmarkIcon.Image = Properties.Resources.star;
            bookmarkIcon.Location = new Point(1138, 12);
            bookmarkIcon.Margin = new Padding(3, 2, 3, 2);
            bookmarkIcon.Name = "bookmarkIcon";
            bookmarkIcon.Size = new Size(24, 24);
            bookmarkIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            bookmarkIcon.TabIndex = 6;
            bookmarkIcon.TabStop = false;
            toolTip.SetToolTip(bookmarkIcon, "Đánh dấu");
            bookmarkIcon.Click += bookmarkIcon_Click;
            // 
            // menuBtn
            // 
            menuBtn.Anchor = AnchorStyles.Right;
            menuBtn.Image = (Image)resources.GetObject("menuBtn.Image");
            menuBtn.Location = new Point(1193, 12);
            menuBtn.Margin = new Padding(3, 2, 3, 2);
            menuBtn.Name = "menuBtn";
            menuBtn.Size = new Size(24, 24);
            menuBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            menuBtn.TabIndex = 5;
            menuBtn.TabStop = false;
            toolTip.SetToolTip(menuBtn, "Menu");
            menuBtn.Click += menuBtn_Click;
            // 
            // backBtn
            // 
            backBtn.Anchor = AnchorStyles.Left;
            backBtn.Image = (Image)resources.GetObject("backBtn.Image");
            backBtn.Location = new Point(10, 12);
            backBtn.Margin = new Padding(3, 2, 3, 2);
            backBtn.Name = "backBtn";
            backBtn.Size = new Size(24, 24);
            backBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            backBtn.TabIndex = 4;
            backBtn.TabStop = false;
            toolTip.SetToolTip(backBtn, "Quay lại");
            backBtn.Click += backBtn_Click;
            // 
            // forwardBtn
            // 
            forwardBtn.Anchor = AnchorStyles.Left;
            forwardBtn.Image = (Image)resources.GetObject("forwardBtn.Image");
            forwardBtn.Location = new Point(37, 12);
            forwardBtn.Margin = new Padding(3, 2, 3, 2);
            forwardBtn.Name = "forwardBtn";
            forwardBtn.Size = new Size(24, 24);
            forwardBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            forwardBtn.TabIndex = 3;
            forwardBtn.TabStop = false;
            toolTip.SetToolTip(forwardBtn, "Tiến tới");
            forwardBtn.Click += forwardBtn_Click;
            // 
            // reloadBtn
            // 
            reloadBtn.Anchor = AnchorStyles.Left;
            reloadBtn.Image = (Image)resources.GetObject("reloadBtn.Image");
            reloadBtn.Location = new Point(72, 12);
            reloadBtn.Margin = new Padding(3, 2, 3, 2);
            reloadBtn.Name = "reloadBtn";
            reloadBtn.Size = new Size(24, 24);
            reloadBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            reloadBtn.TabIndex = 2;
            reloadBtn.TabStop = false;
            toolTip.SetToolTip(reloadBtn, "Tải lại");
            reloadBtn.Click += reloadBtn_Click;
            // 
            // homeIcon
            // 
            homeIcon.Anchor = AnchorStyles.Left;
            homeIcon.Image = (Image)resources.GetObject("homeIcon.Image");
            homeIcon.Location = new Point(102, 12);
            homeIcon.Margin = new Padding(3, 2, 3, 2);
            homeIcon.Name = "homeIcon";
            homeIcon.Size = new Size(24, 24);
            homeIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            homeIcon.TabIndex = 1;
            homeIcon.TabStop = false;
            toolTip.SetToolTip(homeIcon, "Trang chủ");
            homeIcon.Click += homeIcon_Click;
            // 
            // toolTip
            // 
            toolTip.BackColor = Color.LightSalmon;
            toolTip.ForeColor = SystemColors.GradientInactiveCaption;
            // 
            // MainWebForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1225, 615);
            Controls.Add(mainWebView);
            Controls.Add(topPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainWebForm";
            Text = "Trình Duyệt";
            Load += MainWebForm_Load;
            ((System.ComponentModel.ISupportInitialize)mainWebView).EndInit();
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)userIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)bookmarkIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)menuBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)backBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)forwardBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)reloadBtn).EndInit();
            ((System.ComponentModel.ISupportInitialize)homeIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 mainWebView;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Panel topPanel;
        private PictureBox homeIcon;
        private PictureBox reloadBtn;
        private PictureBox menuBtn;
        private PictureBox backBtn;
        private PictureBox forwardBtn;
        private PictureBox bookmarkIcon;
        private PictureBox userIcon;
        private ToolTip toolTip;
    }
}
