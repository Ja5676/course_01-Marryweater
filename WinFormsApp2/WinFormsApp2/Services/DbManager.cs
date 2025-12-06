using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp2.Models;

namespace WinFormsApp2.Services
{
    public static class DbManager
    {
        private readonly static string connectionString = "Server=DESKTOP-024LTB5\\MSSQLSERVER01;Database=LotFlowDB;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;";

        public static bool AddUser(string username, string email, string password)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var exists = db.ExecuteScalar<int>(
                    "SELECT COUNT(*) FROM Users WHERE Email = @Email OR Username = @Username",
                    new { Email = email, Username = username });

                if (exists > 0)
                {
                    MessageBox.Show("Користувач з таким логіном чи почтою вже існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                byte[] saltBytes = PasswordHasher.CreateSalt();
                byte[] hashBytes = PasswordHasher.HashPassword(password, saltBytes);

                string saltStr = Convert.ToBase64String(saltBytes);
                string hashStr = Convert.ToBase64String(hashBytes);

                var sql = @"INSERT INTO Users (Username, Email, DateRegistered, Salt, PasswordHash)
                            VALUES (@Username, @Email, @DateRegistered, @Salt, @PasswordHash)";

                db.Execute(sql, new
                {
                    Username = username,
                    Email = email,
                    DateRegistered = DateTime.Now,
                    Salt = saltStr,
                    PasswordHash = hashStr
                });

                MessageBox.Show("Реєстрація Успішна!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
        }

        public static User LoginUser(string username, string password)
        {
            using (var db = new SqlConnection(connectionString))
            {
                var user = db.QueryFirstOrDefault<User>("SELECT * FROM Users WHERE Username = @Username", new { Username = username });

                if (user == null) return null;

                byte[] saltBytes = Convert.FromBase64String(user.Salt);
                byte[] storedHashBytes = Convert.FromBase64String(user.PasswordHash);
                byte[] inputHashBytes = PasswordHasher.HashPassword(password, saltBytes);

                if (inputHashBytes.SequenceEqual(storedHashBytes))
                {
                    return user;
                }
            }
            return null;
        }
    }
}