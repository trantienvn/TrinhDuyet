using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace TrinhDuyet
{
    public partial class MainWebFormTest : Form
    {
        // Import hàm WinAPI
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Hằng số cho SendMessage
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        private List<string> historyList = new List<string>();
        private List<string> bookmarks = new List<string>();
        private string[] loginInfo = new string[2];
        private string bookmarkFile = "bookmarks.txt";
        private bool isLoggedIn = false;
        private string currentUrlTxt = "";
        private string lastSearchKeyword = "";
        private bool isSearch = false;
        public MainWebFormTest(string startUrl = "about:blank")
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
            this.ControlBox = false;
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
                var newForm = new MainWebFormTest("https://www.google.com"); // hoặc truyền URL bạn muốn
                newForm.Show();
            }
            if (e.Control && e.KeyCode == Keys.H)
            {
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
        private void mainWebView_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true;
            // Mở cửa sổ mới giống hệt form này, nhưng với URL mới
            var newBrowser = new MainWebFormTest(e.Uri);
            newBrowser.Show();
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
                    else { 
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
                bookmarkIcon.Image = Properties.Resources.star;
            }
            else
            {
                bookmarks.Insert(0, url);
                bookmarkIcon.Image = Properties.Resources.star_fill;
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
            string currentUrl = mainWebView.Source.ToString();
            //this.Text = "";
            tittleTxt.Text = mainWebView.CoreWebView2.DocumentTitle;
            if (isSearch)
            {
                txtUrl.Text = lastSearchKeyword;
            }
            else
            {
                txtUrl.Text = HideProtocol(currentUrl);
            }
            currentUrlTxt = currentUrl;
                // Nếu URL đã có trong lịch sử thì xóa khỏi vị trí cũ
                historyList.Remove(currentUrl);

            // Thêm URL mới lên đầu danh sách
            historyList.Insert(0, currentUrl);

            // Ghi lại toàn bộ lịch sử vào file (URL mới nhất trên đầu)
            File.WriteAllLines("history.txt", historyList);


            bookmarkIcon.Image = bookmarks.Contains(currentUrl)
                ? Properties.Resources.star_fill
                : Properties.Resources.star;
            LoadUrlAutoComplete();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_CAPTION = 0x00C00000;
                CreateParams cp = base.CreateParams;
                cp.Style &= ~WS_CAPTION; // Xóa style caption (ẩn title bar)
                return cp;
            }
        }
        private void mainWebView_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            txtUrl.Text = mainWebView.Source?.AbsoluteUri;
            UpdateUIAfterNavigation();
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

            foreach (var url in history)
            {
                var item = new ListViewItem(url);
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
                    listView.Items.Remove(listView.SelectedItems[0]);
                    File.WriteAllLines("history.txt", listView.Items.Cast<ListViewItem>().Select(i => i.Text));
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
            LoadBookmarks();
            getLogin();
            string savedUser = loginInfo[0];
            string savedPass = loginInfo[1];

            if (!string.IsNullOrEmpty(savedUser) && !string.IsNullOrEmpty(savedPass))
            {
                var store = new UserStore("users.db");
                if (store.Login(savedUser, savedPass, out var err))
                {
                    userIcon.Image = Properties.Resources.loggedin;
                    isLoggedIn = true;
                }
            }
            await NavigateToUrl("https://google.com.vn");
        }
        private void LoadUrlAutoComplete()
        {
            if (!File.Exists("history.txt")) return;

            string[] historyUrls = File.ReadAllLines("history.txt");
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
                historyList.Clear();

                // Xóa file nếu tồn tại
                string filePath = "history.txt";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

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
                menu.Items.Add($"Xin chào!, {loginInfo[0]}", null, (s, ev) => OpenInfoDialog());
                menu.Items.Add("Đăng xuất", null, (s, ev) => Logout());
            }
            menu.Show(userIcon, new Point(0, userIcon.Height));
        }
        private void Login()
        {
            var store = new UserStore("users.db");
            DangNhap dn = new DangNhap(store);
            var result = dn.ShowDialog();
            if (result == DialogResult.OK)
            {
                userIcon.Image = Properties.Resources.loggedin;
                isLoggedIn = true;
                string filePath = "User.data";
                // Ví dụ: dn.Username và dn.Password là thông tin từ form đăng nhập
                File.WriteAllLines(filePath, new string[] { dn.Username, dn.Password });
                getLogin();
            }
        }
        private void Logout()
        {
            isLoggedIn = false;
            userIcon.Image = Properties.Resources.user;
            string filePath = "User.data";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            getLogin();
        }
        private void getLogin()
        {
            if (File.Exists("User.data"))
            {
                string[] Info = File.ReadAllLines("User.data");
                loginInfo = Info;
            }
            else
            {
                loginInfo[0] = null;
                loginInfo[1] = null;
            }

        }
        private void OpenInfoDialog()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.ShowDialog();
        }
        private Rectangle previousBounds; // lưu kích thước và vị trí cũ
        private bool isMaximized = false; // trạng thái maximize

        private void pannel_DoubleClick(object sender, EventArgs e)
        {
            ToggleMaximizeRestore();
        }

        private void maxmize_Click(object sender, EventArgs e)
        {
            ToggleMaximizeRestore();
        }

        private void pannel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == MouseButtons.Left) // chỉ kéo khi click 1 lần
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToggleMaximizeRestore()
        {
            if (!isMaximized)
            {
                // Lưu lại kích thước và vị trí hiện tại
                previousBounds = this.Bounds;

                // Lấy vùng làm việc (không bao gồm taskbar)
                Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;

                // Maximize nhưng vẫn hiển thị taskbar
                this.FormBorderStyle = FormBorderStyle.None; // nếu muốn borderless
                this.Bounds = workingArea;

                maxmize.Image = Properties.Resources.res; // đổi icon
                isMaximized = true;
            }
            else
            {
                // Khôi phục kích thước và vị trí cũ
                this.Bounds = previousBounds;
                this.FormBorderStyle = FormBorderStyle.Sizable; // trả lại border

                maxmize.Image = Properties.Resources.max; // đổi icon
                isMaximized = false;
            }
        }

        private void minisize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Btn_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            if (pb == close)
            {
                close.BackColor = Color.Red;  // hover close → đỏ
                close.Image = Properties.Resources.close1;
            }
            else if (pb == minisize || pb == maxmize)
            {
                pb.BackColor = Color.FromArgb(30, 128, 128, 128);
            }
        }

        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            pb.BackColor = Color.Transparent;
            if (pb == close)
            {
                close.Image = Properties.Resources.close;
            }
        }
    }
}
