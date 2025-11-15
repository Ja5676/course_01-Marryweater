using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp2.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WinFormsApp2.Services
{
    public static class DbManager
    {
        private readonly static string connectionString = "Server=DESKTOP-024LTB5\\MSSQLSERVER01;Database=LotFlowDB;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;";
        public static IEnumerable<User> GetUsers()
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    var users = db.Query<User>("SELECT * FROM Users");
                    return users;
                }
            }
            catch
            {
                return new List<User>();
            }
        }
        public static IEnumerable<Category> GetCategories()
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    var cetegories = db.Query<Category>("SELECT * FROM Categories");
                    return cetegories;
                }
            }
            catch
            {
                return new List<Category>();
            }
        }
        public static int GetCategoryId(string categoryName)
        {
            using (var db = new SqlConnection(connectionString))
            {
                int categoryId = db.QueryFirstOrDefault<int>(@"SELECT Id FROM Categories WHERE Name = @categoryName", new { categoryName});
                return categoryId;
            }
        }
        public static void AddUser(User user)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    var users = GetUsers().ToList();

                    if (users.Exists(u => u.Username.Equals(user.Username) || u.Email.Equals(user.Email)))
                    {
                        MessageBox.Show("A user with this username or email address already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string initial = user.Username.Substring(0, 1).ToUpper();

                    Color bgColor = Color.FromArgb(0, 113, 251);
                    Color fgColor = Color.White;

                    Bitmap bitmap = new Bitmap(100, 100);

                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(bgColor);
                        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                        float fontSize = 100 * 0.45f;
                        Font font = new Font("Segoe UI", fontSize, FontStyle.Bold);

                        using (SolidBrush textBrush = new SolidBrush(fgColor))
                        using (StringFormat format = new StringFormat())
                        {
                            format.Alignment = StringAlignment.Center;
                            format.LineAlignment = StringAlignment.Center;

                            RectangleF rect = new RectangleF(0, 0, 100, 100);
                            g.DrawString(initial, font, textBrush, rect, format);
                        }
                    }

                    var ms = new MemoryStream();
                    bitmap.Save(ms, ImageFormat.Jpeg);

                    db.Execute(@"INSERT INTO Users (Username, Email, ProfilePicture, DateRegistered, Salt, PasswordHash)
                             VALUES (@Username, @Email, @ProfilePicture, @DateRegistered, @Salt, @PasswordHash)", new User(user.Username, user.Email, user.Salt, user.PasswordHash) { ProfilePicture = ms.ToArray() });

                    MessageBox.Show("You have successfully registered", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Failed to register", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        public static void AddAd(Ads ad)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Execute(@"INSERT INTO Ads (Title, Description, Price, CategoryId, UserId, Image, DatePosted)
                             VALUES (@Title, @Description, @Price, @CategoryId, @UserId, @Image, @DatePosted)", ad);

                    MessageBox.Show("Ad published successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Failed to publish", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        public static void DeleteAd(Ads ad)
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    db.Execute(@"DELETE FROM Ads WHERE Id = @Id", ad);

                    MessageBox.Show("Ad deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Failed to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        public static void UpdateUserPhoto(int id, byte[] img)
        {
            using (var db = new SqlConnection(connectionString))
            {
                db.Execute("UPDATE Users SET ProfilePicture = @ProfilePhoto WHERE Id = @Id", new { ProfilePhoto = img, Id = id });
            }
        }
        public static IEnumerable<Ads> GetAdsFromDatabase()
        {
            try
            {
                using (var db = new SqlConnection(connectionString))
                {
                    var ads = db.Query<Ads>("SELECT * FROM Ads");
                    return ads;
                }
            }
            catch {
                return new List<Ads>();
            }
        }
    }
}
