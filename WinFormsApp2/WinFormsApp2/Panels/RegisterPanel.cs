using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.Json;
using System.Windows.Forms;
using WinFormsApp2.Models;
using WinFormsApp2.Panels;
using WinFormsApp2.Services;

namespace WinFormsApp2
{
    public partial class RegisterPanel : UserControl
    {
        private const string developerEmail = "developer@example.com";
        private const string smtpHost = "smtp.example.com";
        private const int smtpPort = 587;
        private const string smtpUser = "developer@example.com";
        private const string smtpPass = "your_smtp_password";
        private const bool smtpEnableSsl = true;

        public RegisterPanel()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;
            var email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Заповніть всі поля", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DbManager.AddUser(username, email, password))
            {
                var form = FindForm();
                if (form != null)
                {
                    form.Controls.Clear();
                    form.Controls.Add(new AuthorizationPanel { Dock = DockStyle.Fill });
                }
            }
        }

        private void Back(object sender, EventArgs e)
        {
            var form = FindForm();
            if (form != null)
            {
                form.Controls.Clear();
                form.Controls.Add(new AuthorizationPanel { Dock = DockStyle.Fill });
            }
        }
    }
}
