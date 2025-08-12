namespace TrinhDuyet
{
    partial class TrinhDuyet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrinhDuyet));
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            txtUrl = new TextBox();
            topPanel = new Panel();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            picicon = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picicon).BeginInit();
            SuspendLayout();
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Fill;
            webView21.Location = new Point(0, 50);
            webView21.Name = "webView21";
            webView21.Size = new Size(773, 352);
            webView21.TabIndex = 2;
            webView21.ZoomFactor = 1D;
            // 
            // txtUrl
            // 
            txtUrl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtUrl.Location = new Point(175, 12);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(550, 27);
            txtUrl.TabIndex = 0;
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.LightGray;
            topPanel.Controls.Add(pictureBox4);
            topPanel.Controls.Add(pictureBox3);
            topPanel.Controls.Add(pictureBox2);
            topPanel.Controls.Add(pictureBox1);
            topPanel.Controls.Add(picicon);
            topPanel.Controls.Add(txtUrl);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Padding = new Padding(10);
            topPanel.Size = new Size(773, 50);
            topPanel.TabIndex = 3;
            // 
            // pictureBox4
            // 
            pictureBox4.Anchor = AnchorStyles.Right;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(730, 9);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(29, 31);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 5;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(105, 9);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(29, 31);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(140, 9);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(29, 31);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(55, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(29, 31);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // picicon
            // 
            picicon.Image = (Image)resources.GetObject("picicon.Image");
            picicon.Location = new Point(13, 8);
            picicon.Name = "picicon";
            picicon.Size = new Size(34, 32);
            picicon.SizeMode = PictureBoxSizeMode.StretchImage;
            picicon.TabIndex = 1;
            picicon.TabStop = false;
            picicon.Click += picicon_Click;
            // 
            // TrinhDuyet
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(773, 402);
            Controls.Add(webView21);
            Controls.Add(topPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "TrinhDuyet";
            Text = "Trình Duyệt";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picicon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Panel topPanel;
        private PictureBox picicon;
        private PictureBox pictureBox1;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
    }
}
