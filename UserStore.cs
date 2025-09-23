using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TrinhDuyet
{
    public class UserStore
    {
        private readonly string _path;
        private readonly Dictionary<string, (byte[] Salt, byte[] Hash, int Iter)> _users;

        public UserStore(string path)
        {
            _path = path;
            _users = new Dictionary<string, (byte[], byte[], int)>(StringComparer.OrdinalIgnoreCase);
            Load();
        }

        public bool Register(string username, string password, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                error = "Tên đăng nhập và mật khẩu không được để trống.";
                return false;
            }
            if (_users.ContainsKey(username))
            {
                error = "Tên đăng nhập đã tồn tại.";
                return false;
            }

            var salt = RandomBytes(16);
            int iter = 100_000;
            var hash = Pbkdf2(password, salt, iter, 32);
            _users[username] = (salt, hash, iter);
            Save();
            return true;
        }

        public bool Login(string username, string password, out string error)
        {
            error = "";
            if (!_users.TryGetValue(username, out var rec))
            {
                error = "Sai tài khoản hoặc mật khẩu.";
                return false;
            }
            var test = Pbkdf2(password, rec.Salt, rec.Iter, rec.Hash.Length);
            if (!CryptographicOperations.FixedTimeEquals(test, rec.Hash))
            {
                error = "Sai tài khoản hoặc mật khẩu.";
                return false;
            }
            return true;
        }

        private void Load()
        {
            if (!File.Exists(_path)) return;
            foreach (var line in File.ReadAllLines(_path))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(':');
                if (parts.Length < 4) continue;
                var user = parts[0];
                var salt = Convert.FromBase64String(parts[1]);
                var hash = Convert.FromBase64String(parts[2]);
                int iter = int.TryParse(parts[3], out var it) ? it : 100_000;
                _users[user] = (salt, hash, iter);
            }
        }

        private void Save()
        {
            var lines = _users.Select(kv =>
                $"{kv.Key}:{Convert.ToBase64String(kv.Value.Salt)}:{Convert.ToBase64String(kv.Value.Hash)}:{kv.Value.Iter}");
            File.WriteAllLines(_path, lines, Encoding.UTF8);
        }

        private static byte[] RandomBytes(int len)
        {
            var b = new byte[len];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(b);
            return b;
        }

        private static byte[] Pbkdf2(string pwd, byte[] salt, int iterations, int len)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(pwd, salt, iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(len);
            }
        }
    }
}
