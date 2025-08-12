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
        public async void InitWeb()
        {
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
                //await LoadFaviconFromPageOrDefault(webView21.Source);
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


                }
            }
            catch
            {
                picicon.Image = Properties.Resources.logo; // fallback
            }
        }



        private async void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized; // Phóng to toàn màn hình
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

                string input = txtUrl.Text.Trim(); // Đặt tên biến là input để dễ hiểu

                if (string.IsNullOrEmpty(input)) return;

                // Kiểm tra có phải URL hợp lệ hay không
                bool isUrl = Uri.TryCreate(input, UriKind.Absolute, out Uri uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                // Nếu không phải URL hợp lệ thì tìm kiếm Google
                if (!isUrl)
                {
                    // Nếu không có dấu chấm hoặc có khoảng trắng → tìm kiếm Google
                    if (!input.Contains(".") || input.Contains(" "))
                    {
                        string searchUrl = "https://www.google.com/search?q=" + Uri.EscapeDataString(input);
                        webView21.Source = new Uri(searchUrl);
                    }
                    else
                    {
                        // Nếu có dạng domain nhưng thiếu scheme (http/https)
                        webView21.Source = new Uri("https://" + input);
                    }
                }
                else
                {
                    // Nếu là URL hợp lệ thì mở trực tiếp
                    webView21.Source = uriResult;
                }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            webView21.Reload();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            // Quay lại
            var backItem = new ToolStripMenuItem("Quay lại", null, (s, ev) =>
            {
                if (webView21.CanGoBack) webView21.GoBack();
            });
            backItem.Enabled = webView21.CanGoBack;
            menu.Items.Add(backItem);

            // Tiến tới
            var forwardItem = new ToolStripMenuItem("Tiến tới", null, (s, ev) =>
            {
                if (webView21.CanGoForward) webView21.GoForward();
            });
            forwardItem.Enabled = webView21.CanGoForward;
            menu.Items.Add(forwardItem);

            // Tải lại
            menu.Items.Add("Tải lại", null, (s, ev) => webView21.Reload());

            menu.Items.Add(new ToolStripSeparator());

            // Share
            menu.Items.Add("Chia sẻ liên kết", null, (s, ev) =>
            {
                Clipboard.SetText(webView21.Source.ToString());
                MessageBox.Show("Đã sao chép liên kết để chia sẻ!", "Share", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

            // Lịch sử
            menu.Items.Add("Lịch sử", null, (s, ev) =>
            {
                webView21.CoreWebView2.Navigate("edge://history/");
            });

            // Dấu trang
            menu.Items.Add("Dấu trang", null, (s, ev) =>
            {
                // Ở đây bạn có thể lưu URL vào file hoặc DB
                string url = webView21.Source.ToString();
                File.AppendAllText("bookmarks.txt", url + Environment.NewLine);
                MessageBox.Show("Đã lưu vào dấu trang!", "Bookmark", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

            menu.Items.Add(new ToolStripSeparator());

            // Trang chủ
            menu.Items.Add("Trang chủ", null, (s, ev) =>
            {
                webView21.CoreWebView2.Navigate("https://www.google.com");
            });

            // Hiển thị menu dưới nút bấm
            menu.Show(pictureBox4, new Point(0, pictureBox4.Height));
        }

        private void pictureBox2_Click(object sender, EventArgs e) // Forward
        {
            if (webView21 != null && webView21.CanGoForward)
            {
                webView21.GoForward();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e) // Back
        {
            if (webView21 != null && webView21.CanGoBack)
            {
                webView21.GoBack();
            }
        }

        private void webView21_Click(object sender, EventArgs e)
        {

        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void topPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
