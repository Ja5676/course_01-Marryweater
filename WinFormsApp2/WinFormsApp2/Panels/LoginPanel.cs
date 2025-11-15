using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2.Models;
using WinFormsApp2.Services;

namespace WinFormsApp2.Panels
{
    public partial class LoginPanel : UserControl
    {
        public LoginPanel()
        {
            InitializeComponent();
        }
        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            var username = tbUsername.Text.Trim();
            var password = tbPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter username and password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var users = DbManager.GetUsers().ToList();
            var user = users.Find(u => u.Username.Equals(username));
            if (user == null)
            {
                MessageBox.Show("Incorrect password or username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var salt = Convert.FromBase64String(user.Salt);
            var hash = PasswordHasher.HashPassword(password, salt);
            if (Convert.ToBase64String(hash) == user.PasswordHash)
            {
                MessageBox.Show("Successful login", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Controls.Clear();
                var profile = new UserProfilePanel(user)
                {
                    Dock = DockStyle.Fill
                };
                Controls.Add(profile);
            }
            else
            {
                MessageBox.Show("Incorrect password or username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Back(object sender, EventArgs e)
        {
            Controls.Clear();
            var uc = new AuthorizationPanel
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(uc);
        }
    }
}
