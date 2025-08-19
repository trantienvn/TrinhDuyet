namespace TrinhDuyet
{
    partial class MainWebFormTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWebFormTest));
            mainWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            txtUrl = new TextBox();
            topPanel = new Panel();
            tittleTxt = new Label();
            appicon = new PictureBox();
            userIcon = new PictureBox();
            bookmarkIcon = new PictureBox();
            minisize = new PictureBox();
            maxmize = new PictureBox();
            close = new PictureBox();
            menuBtn = new PictureBox();
            backBtn = new PictureBox();
            forwardBtn = new PictureBox();
            reloadBtn = new PictureBox();
            homeIcon = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)mainWebView).BeginInit();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)appicon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)userIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bookmarkIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minisize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxmize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)close).BeginInit();
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
            mainWebView.Location = new Point(0, 85);
            mainWebView.Name = "mainWebView";
            mainWebView.Size = new Size(1135, 474);
            mainWebView.TabIndex = 2;
            mainWebView.ZoomFactor = 1D;
            // 
            // txtUrl
            // 
            txtUrl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtUrl.Location = new Point(158, 48);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(868, 27);
            txtUrl.TabIndex = 0;
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.MistyRose;
            topPanel.Controls.Add(tittleTxt);
            topPanel.Controls.Add(appicon);
            topPanel.Controls.Add(userIcon);
            topPanel.Controls.Add(bookmarkIcon);
            topPanel.Controls.Add(minisize);
            topPanel.Controls.Add(maxmize);
            topPanel.Controls.Add(close);
            topPanel.Controls.Add(menuBtn);
            topPanel.Controls.Add(backBtn);
            topPanel.Controls.Add(forwardBtn);
            topPanel.Controls.Add(reloadBtn);
            topPanel.Controls.Add(homeIcon);
            topPanel.Controls.Add(txtUrl);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Padding = new Padding(10);
            topPanel.Size = new Size(1135, 85);
            topPanel.TabIndex = 3;
            topPanel.DoubleClick += pannel_DoubleClick;
            topPanel.MouseDown += pannel_MouseDown;
            // 
            // tittleTxt
            // 
            tittleTxt.AutoSize = true;
            tittleTxt.Location = new Point(44, 16);
            tittleTxt.Name = "tittleTxt";
            tittleTxt.Size = new Size(84, 20);
            tittleTxt.TabIndex = 9;
            tittleTxt.Text = "Trình Duyệt";
            // 
            // appicon
            // 
            appicon.Image = (Image)resources.GetObject("appicon.Image");
            appicon.Location = new Point(13, 11);
            appicon.Name = "appicon";
            appicon.Size = new Size(25, 25);
            appicon.SizeMode = PictureBoxSizeMode.StretchImage;
            appicon.TabIndex = 8;
            appicon.TabStop = false;
            // 
            // userIcon
            // 
            userIcon.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            userIcon.Image = Properties.Resources.user;
            userIcon.Location = new Point(1067, 48);
            userIcon.Name = "userIcon";
            userIcon.Size = new Size(25, 25);
            userIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            userIcon.TabIndex = 7;
            userIcon.TabStop = false;
            userIcon.Click += userIcon_Click;
            // 
            // bookmarkIcon
            // 
            bookmarkIcon.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bookmarkIcon.Image = Properties.Resources.star;
            bookmarkIcon.Location = new Point(1036, 48);
            bookmarkIcon.Name = "bookmarkIcon";
            bookmarkIcon.Size = new Size(25, 25);
            bookmarkIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            bookmarkIcon.TabIndex = 6;
            bookmarkIcon.TabStop = false;
            bookmarkIcon.Click += bookmarkIcon_Click;
            // 
            // minisize
            // 
            minisize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minisize.Image = Properties.Resources.mini;
            minisize.Location = new Point(1001, 2);
            minisize.Name = "minisize";
            minisize.Size = new Size(32, 32);
            minisize.SizeMode = PictureBoxSizeMode.StretchImage;
            minisize.TabIndex = 5;
            minisize.TabStop = false;
            minisize.Click += minisize_Click;
            minisize.MouseEnter += Btn_MouseEnter;
            minisize.MouseLeave += Btn_MouseLeave;
            // 
            // maxmize
            // 
            maxmize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            maxmize.Image = Properties.Resources.max;
            maxmize.Location = new Point(1043, 2);
            maxmize.Name = "maxmize";
            maxmize.Size = new Size(36, 32);
            maxmize.SizeMode = PictureBoxSizeMode.StretchImage;
            maxmize.TabIndex = 5;
            maxmize.TabStop = false;
            maxmize.Click += maxmize_Click;
            maxmize.MouseEnter += Btn_MouseEnter;
            maxmize.MouseLeave += Btn_MouseLeave;
            // 
            // close
            // 
            close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            close.Image = Properties.Resources.close;
            close.Location = new Point(1090, 2);
            close.Name = "close";
            close.Size = new Size(32, 32);
            close.SizeMode = PictureBoxSizeMode.StretchImage;
            close.TabIndex = 5;
            close.TabStop = false;
            close.Click += close_Click;
            close.MouseEnter += Btn_MouseEnter;
            close.MouseLeave += Btn_MouseLeave;
            // 
            // menuBtn
            // 
            menuBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            menuBtn.Image = (Image)resources.GetObject("menuBtn.Image");
            menuBtn.Location = new Point(1098, 48);
            menuBtn.Name = "menuBtn";
            menuBtn.Size = new Size(25, 25);
            menuBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            menuBtn.TabIndex = 5;
            menuBtn.TabStop = false;
            menuBtn.Click += menuBtn_Click;
            // 
            // backBtn
            // 
            backBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            backBtn.Image = (Image)resources.GetObject("backBtn.Image");
            backBtn.Location = new Point(12, 48);
            backBtn.Name = "backBtn";
            backBtn.Size = new Size(25, 25);
            backBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            backBtn.TabIndex = 4;
            backBtn.TabStop = false;
            backBtn.Click += backBtn_Click;
            // 
            // forwardBtn
            // 
            forwardBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            forwardBtn.Image = (Image)resources.GetObject("forwardBtn.Image");
            forwardBtn.Location = new Point(42, 48);
            forwardBtn.Name = "forwardBtn";
            forwardBtn.Size = new Size(25, 25);
            forwardBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            forwardBtn.TabIndex = 3;
            forwardBtn.TabStop = false;
            forwardBtn.Click += forwardBtn_Click;
            // 
            // reloadBtn
            // 
            reloadBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            reloadBtn.Image = (Image)resources.GetObject("reloadBtn.Image");
            reloadBtn.Location = new Point(82, 48);
            reloadBtn.Name = "reloadBtn";
            reloadBtn.Size = new Size(25, 25);
            reloadBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            reloadBtn.TabIndex = 2;
            reloadBtn.TabStop = false;
            reloadBtn.Click += reloadBtn_Click;
            // 
            // homeIcon
            // 
            homeIcon.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            homeIcon.Image = (Image)resources.GetObject("homeIcon.Image");
            homeIcon.Location = new Point(117, 48);
            homeIcon.Name = "homeIcon";
            homeIcon.Size = new Size(25, 25);
            homeIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            homeIcon.TabIndex = 1;
            homeIcon.TabStop = false;
            homeIcon.Click += homeIcon_Click;
            // 
            // MainWebFormTest
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1135, 559);
            Controls.Add(mainWebView);
            Controls.Add(topPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainWebFormTest";
            Text = "Trình Duyệt";
            Load += MainWebForm_Load;
            ((System.ComponentModel.ISupportInitialize)mainWebView).EndInit();
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)appicon).EndInit();
            ((System.ComponentModel.ISupportInitialize)userIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)bookmarkIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)minisize).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxmize).EndInit();
            ((System.ComponentModel.ISupportInitialize)close).EndInit();
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
        private PictureBox minisize;
        private PictureBox maxmize;
        private PictureBox close;
        private PictureBox appicon;
        private Label tittleTxt;
    }
}
