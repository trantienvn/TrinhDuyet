using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace TrinhDuyet
{
    public partial class TrinhDuyet : Form
    {
        public TrinhDuyet()
        {
            InitializeComponent();
            InitWeb();

        }
        public async void InitWeb() {
            //khởi tạo webview

            txtUrl.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LoadWeb();
                    e.SuppressKeyPress = true; //ngăn phát âm thanh khi nhấn entter
                }
            };
            webView21.NavigationCompleted += async (sender, args) =>
            {
                this.Text = webView21.CoreWebView2.DocumentTitle; // đổi tiêu đề form
                txtUrl.Text = webView21.Source.ToString();    // hiển thị URL
                await LoadFaviconFromPageOrDefault(webView21.Source);
            };
            await webView21.EnsureCoreWebView2Async();
            webView21.CoreWebView2.ContainsFullScreenElementChanged += (sender, args) =>
            {
                if (webView21.CoreWebView2.ContainsFullScreenElement)
                {
                    // Bật full screen cho form
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    // Thoát full screen
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Normal;
                }
            };
        }
        private async Task LoadFaviconFromPageOrDefault(Uri pageUri)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = await client.GetStringAsync(pageUri);

                    string iconUrl = null;
                    var match = System.Text.RegularExpressions.Regex.Match(
                        html,
                        "<link[^>]+rel=[\"']?(?:shortcut )?icon[\"']?[^>]*href=[\"']?([^\"'>]+)",
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase
                    );

                    if (match.Success)
                    {
                        iconUrl = match.Groups[1].Value;
                        if (!iconUrl.StartsWith("http"))
                            iconUrl = new Uri(pageUri, iconUrl).ToString();
                    }
                    else
                    {
                        iconUrl = $"{pageUri.Scheme}://{pageUri.Host}/favicon.ico";
                    }

                    // Bỏ qua favicon svg vì GDI+ không hỗ trợ
                    if (iconUrl.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
                        throw new NotSupportedException("SVG not supported by GDI+");

                    // Tải dữ liệu icon
                    var response = await client.GetAsync(iconUrl);
                    if (!response.IsSuccessStatusCode ||
                        !response.Content.Headers.ContentType.MediaType.StartsWith("image"))
                    {
                        throw new Exception("Invalid favicon format");
                    }

                    var data = await response.Content.ReadAsByteArrayAsync();
                    using (var ms = new System.IO.MemoryStream(data))
                    {
                        using (var imgTemp = Image.FromStream(ms))
                        {
                            picicon.Image = new Bitmap(imgTemp); // clone để tránh lỗi dispose
                        }
                    }
                }
            }
            catch
            {
                picicon.Image = Properties.Resources.logo; // fallback
            }
        }



        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Đảm bảo WebView2 đã khởi tạo
                await webView21.EnsureCoreWebView2Async();

                string url = "https://google.com.vn";

                if (string.IsNullOrEmpty(url)) return;

                // Thêm https nếu thiếu
                if (!url.StartsWith("http"))
                {
                    url = "https://" + url;
                }

                webView21.Source = new Uri(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải trang: " + ex.Message);
            }
        }
        private async void LoadWeb()
        {
            try
            {
                // Đảm bảo WebView2 đã khởi tạo
                await webView21.EnsureCoreWebView2Async();

                string url = txtUrl.Text.Trim();

                if (string.IsNullOrEmpty(url)) return;

                // Thêm https nếu thiếu
                if (!url.StartsWith("http"))
                {
                    url = "https://" + url;
                }

                webView21.Source = new Uri(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải trang: " + ex.Message);
            }
        }

        private async void picicon_Click(object sender, EventArgs e)
        {
            try
            {
                // Đảm bảo WebView2 đã khởi tạo
                await webView21.EnsureCoreWebView2Async();

                string url = "https://google.com.vn";

                if (string.IsNullOrEmpty(url)) return;

                // Thêm https nếu thiếu
                if (!url.StartsWith("http"))
                {
                    url = "https://" + url;
                }

                webView21.Source = new Uri(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải trang: " + ex.Message);
            }
        }
    }
}
