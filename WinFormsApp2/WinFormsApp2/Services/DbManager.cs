using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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

                    db.Execute(@"INSERT INTO Users (Username, Email, DateRegistered, Salt, PasswordHash)
                             VALUES (@Username, @Email, @DateRegistered, @Salt, @PasswordHash)", new User(user.Username, user.Email, user.Salt, user.PasswordHash));

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
    }
}
