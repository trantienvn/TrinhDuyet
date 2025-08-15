using Microsoft.Web.WebView2.Core;

namespace TrinhDuyet
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        //[STAThread]
        //static void Main()
        //{
        //    // To customize application configuration such as set high DPI settings or default font,
        //    // see https://aka.ms/applicationconfiguration.
        //    ApplicationConfiguration.Initialize();
        //    Application.Run(new TrinhDuyet());
        //}
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var store = new UserStore("users.db");
            string startUrl = "https://www.google.com";
            if (args.Length > 0)
            {
                startUrl = args[0]; // Lấy tham số URL đầu tiên
            }

            Application.Run(new TrinhDuyet(startUrl));
        }

        public static CoreWebView2Environment SharedEnv;

        public static async Task<CoreWebView2Environment> GetSharedEnv()
        {
            if (SharedEnv == null)
            {
                string userDataFolder = Path.Combine(Application.StartupPath, "WebView2Data");
                SharedEnv = await CoreWebView2Environment.CreateAsync(null, userDataFolder);
            }
            return SharedEnv;
        }

    }
}