using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace TrinhDuyet
{
    public partial class TrinhDuyet : Form
    {
        private List<string> historyList = new List<string>();
        private List<string> bookmarks = new List<string>();
        private string bookmarkFile = "bookmarks.txt";

        public TrinhDuyet(string startUrl = "about:blank")
        {
            InitializeComponent();
            this.Load += async (s, e) =>
            {
                await  InitWeb();
                await NavigateToUrl(startUrl);
            };
            //webView21.KeyPreview = true;
            webView21.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                e.Handled = true;
                var newForm = new TrinhDuyet("https://www.google.com"); // hoặc truyền URL bạn muốn
                newForm.Show();
            }
            if (e.KeyCode == Keys.F11)
            {
                ToggleFullScreen(this.FormBorderStyle != FormBorderStyle.None);
            }

        }

        // ======= KHỞI TẠO WEBVIEW =======
        public async Task InitWeb()
        {
            if (webView21.CoreWebView2 != null) return;

            
            try
            {
                var env = await Program.GetSharedEnv();
                await webView21.EnsureCoreWebView2Async(env);
            } catch(Exception e) {
                await webView21.EnsureCoreWebView2Async(); 
            }
            txtUrl.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    _ = NavigateFromInput();
                    e.SuppressKeyPress = true;
                }
            };

            webView21.NavigationCompleted += (sender, args) =>
            {
                UpdateUIAfterNavigation();
            };

            webView21.CoreWebView2.ContainsFullScreenElementChanged += (sender, args) =>
            {
                ToggleFullScreen(webView21.CoreWebView2.ContainsFullScreenElement);
            };
            webView21.CoreWebView2.NewWindowRequested += webView21_NewWindowRequested;
        }

        // ======= ĐIỀU HƯỚNG =======
        private async Task NavigateToUrl(string url)
        {
            try
            {
                await webView21.EnsureCoreWebView2Async();
                if (string.IsNullOrWhiteSpace(url)) return;
                if (!url.StartsWith("http")) url = "https://" + url;
                webView21.Source = new Uri(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải trang: " + ex.Message);
            }
        }
        private void webView21_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true;
            // Mở cửa sổ mới giống hệt form này, nhưng với URL mới
            var newBrowser = new TrinhDuyet(e.Uri);
            newBrowser.Show();
        }

        private async Task NavigateFromInput()
        {
            try
            {
                await webView21.EnsureCoreWebView2Async();
                string input = txtUrl.Text.Trim();
                if (string.IsNullOrEmpty(input)) return;

                bool isUrl = Uri.TryCreate(input, UriKind.Absolute, out Uri uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (!isUrl)
                {
                    if (!input.Contains(".") || input.Contains(" "))
                        await NavigateToUrl("https://www.google.com/search?q=" + Uri.EscapeDataString(input));
                    else
                        await NavigateToUrl(input);
                }
                else
                {
                    webView21.Source = uriResult;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải trang: " + ex.Message);
            }
        }

        // ======= BOOKMARKS =======
        private void LoadBookmarks()
        {
            if (File.Exists(bookmarkFile))
                bookmarks = new List<string>(File.ReadAllLines(bookmarkFile));
        }

        private void SaveBookmarks()
        {
            File.WriteAllLines(bookmarkFile, bookmarks);
        }

        private void ToggleBookmark(string url)
        {
            if (bookmarks.Contains(url))
            {
                bookmarks.Remove(url);
                pictureBox5.Image = Properties.Resources.star;
            }
            else
            {
                bookmarks.Insert(0,url);
                pictureBox5.Image = Properties.Resources.star_fill;
            }
            SaveBookmarks();
        }
        private void ShowBookmarks()
        {
            if (!File.Exists(bookmarkFile))
            {
                MessageBox.Show("Chưa có dấu trang!");
                return;
            }

            string[] bookmark = File.ReadAllLines(bookmarkFile);
            Form bookmarkForm = new Form
            {
                Text = "Trang đã được đánh dấu sao",
                Size = new Size(600, 400)
            };

            ListBox listBox = new ListBox { Dock = DockStyle.Fill };
            listBox.Items.AddRange(bookmark);
            listBox.DoubleClick += (s, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    _ = NavigateToUrl(listBox.SelectedItem.ToString());
                    bookmarkForm.Close();
                }
            };

            bookmarkForm.Controls.Add(listBox);
            bookmarkForm.ShowDialog();
        }

        // ======= UI & HISTORY =======
        private void UpdateUIAfterNavigation()
        {
            string currentUrl = webView21.Source.ToString();
            this.Text = webView21.CoreWebView2.DocumentTitle;
            txtUrl.Text = currentUrl;

            // Nếu URL đã có trong lịch sử thì xóa khỏi vị trí cũ
            historyList.Remove(currentUrl);

            // Thêm URL mới lên đầu danh sách
            historyList.Insert(0, currentUrl);

            // Ghi lại toàn bộ lịch sử vào file (URL mới nhất trên đầu)
            File.WriteAllLines("history.txt", historyList);


            pictureBox5.Image = bookmarks.Contains(currentUrl)
                ? Properties.Resources.star_fill
                : Properties.Resources.star;
        }

        private void ShowHistory()
        {
            if (!File.Exists("history.txt"))
            {
                MessageBox.Show("Chưa có lịch sử!");
                return;
            }

            string[] history = File.ReadAllLines("history.txt");
            Form historyForm = new Form
            {
                Text = "Lịch sử duyệt web",
                Size = new Size(600, 400)
            };

            ListBox listBox = new ListBox { Dock = DockStyle.Fill };
            listBox.Items.AddRange(history);
            listBox.DoubleClick += (s, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    _ = NavigateToUrl(listBox.SelectedItem.ToString());
                    historyForm.Close();
                }
            };

            historyForm.Controls.Add(listBox);
            historyForm.ShowDialog();
        }

        // ======= FULL SCREEN =======
        private void ToggleFullScreen(bool enable)
        {
            if (enable)
            {
                topPanel.Visible = false;

                // Cho webView chiếm toàn bộ form
                webView21.Dock = DockStyle.Fill;

                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                // Hiện lại topPanel
                topPanel.Visible = true;

                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
        }

        // ======= FORM LOAD =======
        private async void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            LoadBookmarks();
            await NavigateToUrl("https://google.com.vn");
        }
        private void ClearBrowsingHistory()
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa toàn bộ lịch sử duyệt web?",
                "Xác nhận xóa lịch sử",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                // Xóa danh sách trong bộ nhớ
                historyList.Clear();

                // Xóa file nếu tồn tại
                string filePath = "history.txt";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                MessageBox.Show("Đã xóa toàn bộ lịch sử duyệt web.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // ======= ICONS EVENTS =======
        private async void picicon_Click(object sender, EventArgs e)
        {
            await NavigateToUrl("https://www.google.com.vn");
        }

        private void pictureBox1_Click(object sender, EventArgs e) => webView21.Reload();
        private void pictureBox2_Click(object sender, EventArgs e) { if (webView21.CanGoForward) webView21.GoForward(); }
        private void pictureBox3_Click(object sender, EventArgs e) { if (webView21.CanGoBack) webView21.GoBack(); }
        private void pictureBox5_Click(object sender, EventArgs e) => ToggleBookmark(webView21.Source.ToString());

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            var backItem = new ToolStripMenuItem("Quay lại", null, (s, ev) => { if (webView21.CanGoBack) webView21.GoBack(); })
            { Enabled = webView21.CanGoBack };
            menu.Items.Add(backItem);

            var forwardItem = new ToolStripMenuItem("Tiến tới", null, (s, ev) => { if (webView21.CanGoForward) webView21.GoForward(); })
            { Enabled = webView21.CanGoForward };
            menu.Items.Add(forwardItem);

            menu.Items.Add("Tải lại", null, (s, ev) => webView21.Reload());
            menu.Items.Add(new ToolStripSeparator());

            menu.Items.Add("Chia sẻ liên kết", null, (s, ev) =>
            {
                Clipboard.SetText(webView21.Source.ToString());
                MessageBox.Show("Đã sao chép liên kết!", "Share", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

            menu.Items.Add("Lịch sử", null, (s, ev) => ShowHistory());
            menu.Items.Add("Dấu trang", null, (s, ev) => ShowBookmarks());
            menu.Items.Add("Xóa lịch sử", null, (s, ev) => ClearBrowsingHistory());
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("Trang chủ", null, (s, ev) => _ = NavigateToUrl("https://www.google.com"));

            menu.Show(pictureBox4, new Point(0, pictureBox4.Height));
        }
    }
}
