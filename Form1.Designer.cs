namespace TrinhDuyet
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            txtUrl = new TextBox();
            topPanel = new Panel();
            picicon = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            topPanel.SuspendLayout();
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
            webView21.Size = new Size(1035, 541);
            webView21.TabIndex = 2;
            webView21.ZoomFactor = 1D;
            // 
            // txtUrl
            // 
            txtUrl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtUrl.Location = new Point(57, 12);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(966, 27);
            txtUrl.TabIndex = 0;
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.LightGray;
            topPanel.Controls.Add(picicon);
            topPanel.Controls.Add(txtUrl);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Padding = new Padding(10);
            topPanel.Size = new Size(1035, 50);
            topPanel.TabIndex = 3;
            // 
            // picicon
            // 
            picicon.Image = Properties.Resources.logo;
            picicon.Location = new Point(13, 5);
            picicon.Name = "picicon";
            picicon.Size = new Size(38, 38);
            picicon.SizeMode = PictureBoxSizeMode.StretchImage;
            picicon.TabIndex = 1;
            picicon.TabStop = false;
            picicon.Click += picicon_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1035, 591);
            Controls.Add(webView21);
            Controls.Add(topPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Trình Duyệt";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picicon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Panel topPanel;
        private PictureBox picicon;
    }
}
