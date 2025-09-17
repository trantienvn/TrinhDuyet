using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrinhDuyet
{
    public partial class MainWebForm : Form
    {
        private string Username = "";
        private bool isLoggedIn = false;
        private string currentUrlTxt = "";
        private string lastSearchKeyword = "";
        private bool isSearch = false;
        public MainWebForm(string startUrl = "about:blank")
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.Load += async (s, e) =>
            {
                await InitWeb();
                await NavigateToUrl(startUrl);
            };
            //mainWebView.KeyPreview = true;
            mainWebView.KeyDown += MainWebForm_KeyDown;
            // this.ControlBox = false;
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;

        }
        private string HideProtocol(string url)
        {
            string result = url;

            // Bước 1: Ẩn "https://" hoặc "http://"
            if (result.StartsWith("https://"))
            {
                result = result.Substring(8);
            }
            else if (result.StartsWith("http://"))
            {
                result = result.Substring(7);
            }

            // Bước 2: Ẩn "www." nếu có
            if (result.StartsWith("www."))
            {
                result = result.Substring(4);
            }
            result = result.TrimEnd('/');

            return result;
        }
        private void txtUrl_LostFocus(object sender, EventArgs e)
        {
            if (!isSearch && !string.IsNullOrEmpty(currentUrlTxt))
            {
                txtUrl.Text = HideProtocol(currentUrlTxt);
            }
            else if (isSearch)
            {
                txtUrl.Text = lastSearchKeyword;
            }
        }

        // Hiện full URL khi focus
        private void txtUrl_GotFocus(object sender, EventArgs e)
        {
            if (!isSearch)
            {
                txtUrl.Text = currentUrlTxt;
                txtUrl.SelectAll();
            }
            else
            {
                txtUrl.SelectAll();
            }
        }

        private void MainWebForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                e.Handled = true;
                var newForm = new MainWebForm("https://www.google.com"); // hoặc truyền URL bạn muốn
                newForm.Show();
            }
            if (e.Control && e.KeyCode == Keys.H) { 
                ShowHistory();
            }
            if (e.Control && e.KeyCode == Keys.E)
            {
                ToggleBookmark(mainWebView.Source.ToString());
            }
            if (e.Control && e.KeyCode == Keys.W)
            {
                e.Handled = true;
                this.Close(); // hoặc đóng tab hiện tại nếu bạn quản lý nhiều tab
            }
            if (e.Control && e.Shift && e.KeyCode == Keys.B)
            {
                e.Handled = true;
                ToggleBar(); // hàm bạn tự tạo để ẩn/hiện thanh bookmark
            }
            if (e.KeyCode == Keys.F11)
            {
                ToggleFullScreen();
            }

        }
        private void ToggleBar()
        {
            topPanel.Visible = !topPanel.Visible;
        }

        // ======= KHỞI TẠO WEBVIEW =======
        public async Task InitWeb()
        {
            if (mainWebView.CoreWebView2 != null) return;


            try
            {
                var env = await Program.GetSharedEnv();
                await mainWebView.EnsureCoreWebView2Async(env);
            }
            catch (Exception e)
            {
                await mainWebView.EnsureCoreWebView2Async();
            }
            txtUrl.GotFocus += txtUrl_GotFocus;
            txtUrl.LostFocus += txtUrl_LostFocus;
            txtUrl.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    _ = NavigateFromInput();
                    e.SuppressKeyPress = true;
                }
            };

            mainWebView.NavigationCompleted += (sender, args) =>
            {
                UpdateUIAfterNavigation();
            };
            mainWebView.SourceChanged += mainWebView_SourceChanged;
            mainWebView.CoreWebView2.ContainsFullScreenElementChanged += (sender, args) =>
            {
                isFullScreen = !mainWebView.CoreWebView2.ContainsFullScreenElement;
                ToggleFullScreen();
            };
            mainWebView.CoreWebView2.NewWindowRequested += mainWebView_NewWindowRequested;
            LoadUrlAutoComplete();
        }

        // ======= ĐIỀU HƯỚNG =======
        private async Task NavigateToUrl(string url)
        {
            try
            {
                await mainWebView.EnsureCoreWebView2Async();
                if (string.IsNullOrWhiteSpace(url)) return;
                if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
                {
                    url = "https://" + url;
                }
                mainWebView.Source = new Uri(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải trang: " + ex.Message);
            }
        }
        private async Task NavigateFromInput()
        {
            try
            {
                await mainWebView.EnsureCoreWebView2Async();
                string input = txtUrl.Text.Trim();
                if (string.IsNullOrEmpty(input)) return;

                bool isUrl = Uri.TryCreate(input, UriKind.Absolute, out Uri uriResult)
             && (uriResult.Scheme == Uri.UriSchemeHttp
                 || uriResult.Scheme == Uri.UriSchemeHttps
                 || uriResult.Scheme == Uri.UriSchemeFile
                 || uriResult.Scheme == "edge"
                 || uriResult.Scheme == "chrome");


                if (!isUrl)
                {
                    if (!input.Contains(".") || input.Contains(" "))
                    {
                        lastSearchKeyword = input;
                        isSearch = true;
                        await NavigateToUrl("https://www.google.com/search?q=" + Uri.EscapeDataString(input));
                    }
                    else
                    {
                        isSearch = false;
                        await NavigateToUrl(input);
                    }
                }
                else
                {
                    isSearch = false;
                    mainWebView.Source = uriResult;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải trang: " + ex.Message);
            }
        }
        
        // ====== Lắng nghe WebView ====
        private void mainWebView_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true;
            // Mở cửa sổ mới giống hệt form này, nhưng với URL mới
            var newBrowser = new MainWebForm(e.Uri);
            newBrowser.Show();
        }
        private void mainWebView_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            txtUrl.Text = mainWebView.Source?.AbsoluteUri;
            UpdateUIAfterNavigation();
        }


        // ======= BOOKMARKS =======
        string[] dautrang = [];
        private void ToggleBookmark(string url)
        {
            dautrang = UserStore.GetBookmarks(Username).Select(a=>a.Url).ToArray();

            if (dautrang.Contains(url))
            {
                UserStore.XoaDauTrang(Username, url);
                dautrang = dautrang.Where(a=>a!= url).ToArray();
                bookmarkIcon.Image = Properties.Resources.star;
            }
            else
            {
                UserStore.ThemDauTrang(Username, url, mainWebView.CoreWebView2.DocumentTitle);
                dautrang = [.. dautrang, url];
                bookmarkIcon.Image = Properties.Resources.star_fill;
            }
            
            
        }
        private void ShowBookmarks()
        {
            var bookmarks = UserStore.GetBookmarks(Username);

            Form bookmarkForm = new Form
            {
                Text = "Trang đã đánh dấu",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent
            };

            ListView listView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true
            };

            listView.Columns.Add("Tên hiển thị", 200);
            listView.Columns.Add("URL", 350);
            listView.Columns.Add("", 50);

            foreach (var bm in bookmarks)
            {
                var item = new ListViewItem(bm.TenHienThi)
                {
                    Tag = bm.Id // lưu Id để xóa trong DB
                };
                item.SubItems.Add(bm.Url);
                item.SubItems.Add("⋮");
                listView.Items.Add(item);
            }

            // Double click để mở trang
            listView.DoubleClick += (s, e) =>
            {
                if (listView.SelectedItems.Count > 0)
                {
                    _ = NavigateToUrl(listView.SelectedItems[0].SubItems[1].Text);
                    bookmarkForm.Close();
                }
            };

            // Context menu cho ⋮
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Sao chép URL", null, (s, e) =>
            {
                if (listView.SelectedItems.Count > 0)
                {
                    Clipboard.SetText(listView.SelectedItems[0].SubItems[1].Text);
                }
            });
            menu.Items.Add("Xóa bookmark", null, (s, e) =>
            {
                if (listView.SelectedItems.Count > 0)
                {
                    var selected = listView.SelectedItems[0];
                    int bmId = (int)selected.Tag;

                    // Xóa trong DB
                    UserStore.XoaDauTrang(Username,bmId);

                    // Xóa trong UI
                    listView.Items.Remove(selected);
                }
            });

            listView.MouseClick += (s, e) =>
            {
                var info = listView.HitTest(e.Location);
                if (info.Item != null && info.SubItem != null)
                {
                    int subItemIndex = info.Item.SubItems.IndexOf(info.SubItem);
                    if (subItemIndex == 2) // cột ⋮
                    {
                        listView.FocusedItem = info.Item;
                        menu.Show(listView, e.Location);
                    }
                }
            };

            Button btnClose = new Button { Text = "Đóng", Dock = DockStyle.Bottom, Size = new Size(200, 33) };
            btnClose.Click += (s, e) => bookmarkForm.Close();

            bookmarkForm.Controls.Add(listView);
            bookmarkForm.Controls.Add(btnClose);

            bookmarkForm.ShowDialog();
        }

        // ======= UI & HISTORY =======
        private void UpdateUIAfterNavigation()
        {
            string currentUrl = mainWebView.Source.ToString();
            this.Text = mainWebView.CoreWebView2.DocumentTitle;
            // txtUrl.Text = currentUrl;
            if (isSearch)
            {
                txtUrl.Text = lastSearchKeyword;
            }
            else
            {
                txtUrl.Text = HideProtocol(currentUrl);
            }
            currentUrlTxt = currentUrl;
            UserStore.ThemLichSu(Username, currentUrl);
            dautrang = UserStore.GetBookmarks(Username).Select(dt => dt.Url).ToArray();
            bookmarkIcon.Image = dautrang.Contains(currentUrl)
                ? Properties.Resources.star_fill
                : Properties.Resources.star;
            LoadUrlAutoComplete();
        }
        
        private void ShowHistory()
        {
            var lichSu = UserStore.GetLichSu(Username);
            string[] history = lichSu.Select(ls => ls.Url).Distinct().ToArray();

            Form historyForm = new Form
            {
                Text = "Lịch sử duyệt web",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent
            };

            // Dùng ListView thay cho ListBox
            ListView listView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true
            };

            listView.Columns.Add("URL", 500);
            listView.Columns.Add("", 50);

            foreach (var ls in lichSu)
            {
                var item = new ListViewItem(ls.Url)
                {
                    Tag = ls.Id // lưu Id để xóa trong DB
                };
                item.SubItems.Add("⋮");
                listView.Items.Add(item);
            }

            // Double click vào URL để mở
            listView.DoubleClick += (s, e) =>
            {
                if (listView.SelectedItems.Count > 0)
                {
                    _ = NavigateToUrl(listView.SelectedItems[0].Text);
                    historyForm.Close();
                }
            };

            // Tạo context menu cho nút ⋮
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Sao chép URL", null, (s, e) =>
            {
                if (listView.SelectedItems.Count > 0)
                {
                    Clipboard.SetText(listView.SelectedItems[0].Text);
                }
            });
            menu.Items.Add("Xóa", null, (s, e) =>
            {
                if (listView.SelectedItems.Count > 0)
                {
                    var selected = listView.SelectedItems[0];
                    int lichSuId = (int)selected.Tag;

                    // Xóa trong DB
                    UserStore.XoaLichSu(lichSuId);

                    // Xóa trong UI
                    listView.Items.Remove(selected);
                }
            });

            // Xử lý click vào cột ⋮
            listView.MouseClick += (s, e) =>
            {
                var info = listView.HitTest(e.Location);
                if (info.Item != null && info.SubItem != null)
                {
                    int subItemIndex = info.Item.SubItems.IndexOf(info.SubItem);
                    if (subItemIndex == 1) // cột ⋮
                    {
                        listView.FocusedItem = info.Item;
                        menu.Show(listView, e.Location);
                    }
                }
            };

            Button btnClose = new Button { Text = "Đóng", Dock = DockStyle.Bottom, Size = new Size(200, 33) };
            btnClose.Click += (s, e) => historyForm.Close();

            historyForm.Controls.Add(listView);
            historyForm.Controls.Add(btnClose);

            historyForm.ShowDialog();
        }




        // ======= FULL SCREEN =======
        // Biến lưu trạng thái cũ
        private FormBorderStyle oldBorderStyle;
        private FormWindowState oldWindowState;
        private Rectangle oldBounds;

        private bool isFullScreen = false;

        private void ToggleFullScreen()
        {
            if (!isFullScreen)
            {
                // Lưu trạng thái cũ
                oldBorderStyle = this.FormBorderStyle;
                oldWindowState = this.WindowState;
                oldBounds = this.Bounds;

                // Bật full screen
                topPanel.Visible = false;
                mainWebView.Dock = DockStyle.Fill;

                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
                this.Bounds = Screen.PrimaryScreen.Bounds;

                isFullScreen = true;
            }
            else
            {
                // Trả lại trạng thái cũ
                this.FormBorderStyle = oldBorderStyle;
                this.WindowState = oldWindowState;
                this.Bounds = oldBounds;

                topPanel.Visible = true;
                mainWebView.Dock = DockStyle.Fill;

                isFullScreen = false;
            }
        }


        // ======= FORM LOAD =======
        private async void MainWebForm_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            var cfg = ConfigManager.Load();
            getLogin();
            isLoggedIn = cfg.LoggedIn;
            userIcon.Image = isLoggedIn ? Properties.Resources.loggedin: Properties.Resources.user;
            
            await NavigateToUrl("https://google.com.vn");
        }
        private void LoadUrlAutoComplete()
        {
            var lichSu = UserStore.GetLichSu(Username);
            string[] historyUrls = lichSu.Select(ls => ls.Url).Distinct().ToArray();

            AutoCompleteStringCollection autoCompleteUrls = new AutoCompleteStringCollection();
            autoCompleteUrls.AddRange(historyUrls);

            txtUrl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtUrl.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtUrl.AutoCompleteCustomSource = autoCompleteUrls;
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
                UserStore.XoaTatCaLichSu(Username);

                MessageBox.Show("Đã xóa toàn bộ lịch sử duyệt web.", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadUrlAutoComplete();
        }


        // ======= ICONS EVENTS =======
        private async void homeIcon_Click(object sender, EventArgs e)
        {
            await NavigateToUrl("https://www.google.com.vn");
        }

        private void reloadBtn_Click(object sender, EventArgs e) => mainWebView.Reload();
        private void forwardBtn_Click(object sender, EventArgs e) { if (mainWebView.CanGoForward) mainWebView.GoForward(); }
        private void backBtn_Click(object sender, EventArgs e) { if (mainWebView.CanGoBack) mainWebView.GoBack(); }
        private void bookmarkIcon_Click(object sender, EventArgs e) => ToggleBookmark(mainWebView.Source.ToString());

        private void menuBtn_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            var backItem = new ToolStripMenuItem("Quay lại", null, (s, ev) => { if (mainWebView.CanGoBack) mainWebView.GoBack(); })
            { Enabled = mainWebView.CanGoBack };
            menu.Items.Add(backItem);

            var forwardItem = new ToolStripMenuItem("Tiến tới", null, (s, ev) => { if (mainWebView.CanGoForward) mainWebView.GoForward(); })
            { Enabled = mainWebView.CanGoForward };
            menu.Items.Add(forwardItem);

            menu.Items.Add("Tải lại", null, (s, ev) => mainWebView.Reload());
            menu.Items.Add(new ToolStripSeparator());

            menu.Items.Add("Chia sẻ liên kết", null, (s, ev) =>
            {
                Clipboard.SetText(mainWebView.Source.ToString());
                MessageBox.Show("Đã sao chép liên kết!", "Share", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

            menu.Items.Add("Lịch sử", null, (s, ev) => ShowHistory());
            menu.Items.Add("Dấu trang", null, (s, ev) => ShowBookmarks());
            menu.Items.Add("Xóa lịch sử", null, (s, ev) => ClearBrowsingHistory());
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("Trang chủ", null, (s, ev) => _ = NavigateToUrl("https://www.google.com"));

            menu.Show(menuBtn, new Point(0, menuBtn.Height));
        }

        private void userIcon_Click(object sender, EventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            if (!isLoggedIn)
            {

                menu.Items.Add("Đăng nhập", null, (s, ev) => Login());
            }

            else
            {
                menu.Items.Add($"Xin chào!, {Username}", null, (s,ev) => OpenInfoDialog());
                menu.Items.Add("Đăng xuất", null, (s, ev) => Logout());
            }
            menu.Show(userIcon, new Point(0, userIcon.Height));
        }
        private void Login()
        {
            DangNhap dn = new DangNhap();
            var result = dn.ShowDialog();
            if (result == DialogResult.OK)
            {
                userIcon.Image = Properties.Resources.loggedin;
                isLoggedIn = true;
                getLogin();
            }
        }
        private void Logout()
        {
            isLoggedIn = false;
            userIcon.Image = Properties.Resources.user;
            var cfg = ConfigManager.Load();
            cfg.Username = "userclient";
            cfg.LoggedIn = false;
            ConfigManager.Save(cfg);
            getLogin();
        }
        private void getLogin()
        {
            isLoggedIn = ConfigManager.Load().LoggedIn;
            Username = ConfigManager.Load().Username;

        }
        private void OpenInfoDialog() { 
            UserInfo userInfo = new UserInfo();
            userInfo.ShowDialog();
        }
    }
}
