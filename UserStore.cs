using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TrinhDuyet
{
    // DbContext quản lý dữ liệu trình duyệt
    internal class UserStore : DbContext
    {
        public DbSet<TaiKhoan> Users { get; set; }
        public DbSet<LichSu> LichSu { get; set; }
        public DbSet<DauTrang> DauTrang { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=TrinhDuyet;Trusted_Connection=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed 1 user mặc định
            modelBuilder.Entity<TaiKhoan>().HasData(
                new TaiKhoan
                {
                    Username = "userclient",
                    Password = "" // ⚠️ nên hash để an toàn
                }
            );
        }

        // Tạo user mới
        public static bool TaoUser(string tenDangNhap, string matKhau)
        {
            using var db = new UserStore();
            db.Database.EnsureCreated(); // tạo DB nếu chưa có

            bool existed = db.Users.Any(u => u.Username == tenDangNhap);
            if (existed)
            {
                return false;
            }

            var user = new TaiKhoan
            {
                Username = tenDangNhap,
                Password = matKhau, // ⚠️ gợi ý: hash password để an toàn hơn
            };

            db.Users.Add(user);
            db.SaveChanges();
            return true;
        }

        // Kiểm tra đăng nhập
        public static bool DangNhap(string tenDangNhap, string matKhau)
        {
            using var db = new UserStore();
            var user = db.Users.FirstOrDefault(u => u.Username == tenDangNhap && u.Password == matKhau);

            return user != null;
        }
        // =====================
        // 📌 Quản lý Lịch sử
        // =====================
        public static void ThemLichSu(string userId, string url)
        {
            using var db = new UserStore();
            db.Database.EnsureCreated();

            var lichSu = new LichSu
            {
                Url = url,
                TaiKhoanId = userId
            };
            db.LichSu.Add(lichSu);
            db.SaveChanges();
        }
        public static void XoaLichSu(int lichSuId)
        {
            using var db = new UserStore();
            var lichSu = db.LichSu.Find(lichSuId);
            if (lichSu != null)
            {
                db.LichSu.Remove(lichSu);
                db.SaveChanges();
            }
        }
        public static void XoaTatCaLichSu(string userId)
        {
            using var db = new UserStore();
            var lichSuList = db.LichSu.Where(ls => ls.TaiKhoanId == userId);

            if (lichSuList.Any())
            {
                db.LichSu.RemoveRange(lichSuList);
                db.SaveChanges();
                Console.WriteLine($"🗑️ Đã xóa toàn bộ lịch sử của user {userId}");
            }
            else
            {
                Console.WriteLine("⚠️ Không có lịch sử nào để xóa");
            }
        }


        public static List<LichSu> GetLichSu(string userId)
        {
            using var db = new UserStore();
            return db.LichSu
                     .Where(ls => ls.TaiKhoanId == userId)
                     .OrderByDescending(ls => ls.ThoiGian)
                     .ToList();
        }

        // =====================
        // 📌 Quản lý Bookmark
        // =====================
        public static bool ChuyenDoiBookmark(string userId, string url, string ten = "")
        {
            using var db = new UserStore();
            db.Database.EnsureCreated();

            // Kiểm tra đã có bookmark chưa
            var bm = db.DauTrang.FirstOrDefault(b => b.TaiKhoan.Username == userId && b.Url == url);

            if (bm == null)
            {
                // 👉 Chưa có -> thêm mới
                var bookmark = new DauTrang
                {
                    Url = url,
                    TenHienThi = string.IsNullOrWhiteSpace(ten) ? url : ten,
                    TaiKhoanId = userId
                };
                db.DauTrang.Add(bookmark);
                return true;
            }
            else
            {
                // 👉 Đã có -> xóa đi
                db.DauTrang.Remove(bm);
                return false;
            }

            db.SaveChanges();
        }
        public static void ThemDauTrang(string userId, string url, string TenHT = "")
        {
            using var db = new UserStore();
            db.Database.EnsureCreated();

            var dt = new DauTrang
            {
                TenHienThi = TenHT,
                Url = url,
                TaiKhoanId = userId
            };
            db.DauTrang.Add(dt);
            db.SaveChanges();
        }

        public static void XoaDauTrang(string userId, int id)
        {
            using var db = new UserStore();
            db.Database.EnsureCreated();

            // Kiểm tra đã có bookmark chưa
            var bm = db.DauTrang.FirstOrDefault(b => b.TaiKhoan.Username == userId && b.Id == id);
            {
                // 👉 Đã có -> xóa đi
                db.DauTrang.Remove(bm);
            }

            db.SaveChanges();
        }
        public static void XoaDauTrang(string userId, string url)
        {
            using var db = new UserStore();
            db.Database.EnsureCreated();

            // Kiểm tra đã có bookmark chưa
            var bm = db.DauTrang.FirstOrDefault(b => b.TaiKhoan.Username == userId && b.Url == url);
            {
                // 👉 Đã có -> xóa đi
                db.DauTrang.Remove(bm);
            }

            db.SaveChanges();
        }

        public static List<DauTrang> GetBookmarks(string userId)
        {
            using var db = new UserStore();
            return db.DauTrang
                     .Where(bm => bm.TaiKhoanId == userId)
                     .OrderBy(bm => bm.NgayTao)
                     .ToList();
        }

    }

    // ----------------- ENTITY -----------------
    public class TaiKhoan
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; } = string.Empty;

        // Quan hệ 1-nhiều
        public List<LichSu> LichSuTruyCap { get; set; } = new List<LichSu>();
        public List<DauTrang> DauTrangList { get; set; } = new List<DauTrang>();
    }

    public class LichSu
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public DateTime ThoiGian { get; set; } = DateTime.Now;

        // Liên kết với User
        public string TaiKhoanId { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
    }

    public class DauTrang
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string TenHienThi { get; set; } = string.Empty;
        public DateTime NgayTao { get; set; } = DateTime.Now;

        // Liên kết với User
        public string TaiKhoanId { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
    }
}
