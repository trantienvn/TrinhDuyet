using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TrinhDuyet
{
    public class AppConfig
    {
        public string Username { get; set; } = "userclient";
        public bool LoggedIn { get; set; } = false;
        public int UserId { get; set; } = -1;
    }

    public static class ConfigManager
    {
        private static readonly string ConfigFile =
            Path.Combine(AppContext.BaseDirectory, "config.json");

        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        // Đọc config
        public static AppConfig Load()
        {
            if (!File.Exists(ConfigFile))
                return new AppConfig();

            try
            {
                string json = File.ReadAllText(ConfigFile, Encoding.UTF8);
                return JsonSerializer.Deserialize<AppConfig>(json, jsonOptions) ?? new AppConfig();
            }
            catch
            {
                return new AppConfig();
            }
        }

        // Lưu config
        public static void Save(AppConfig config)
        {
            string json = JsonSerializer.Serialize(config, jsonOptions);
            File.WriteAllText(ConfigFile, json, Encoding.UTF8);
        }
    }
}
