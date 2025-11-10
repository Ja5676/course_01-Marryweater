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
        // Убрано: label «Це UserControl1»

        // Почта разработчика и SMTP-настройки, вписанные в код (замените реальными данными)
        private const string developerEmail = "developer@example.com";
        private const string smtpHost = "smtp.example.com";
        private const int smtpPort = 587;
        private const string smtpUser = "developer@example.com";
        private const string smtpPass = "your_smtp_password"; // Заменить на пароль/токен
        private const bool smtpEnableSsl = true;

        public RegisterPanel()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            // Ваш код инициализации при загрузке компонента
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;
            var email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var salt = PasswordHasher.CreateSalt();
            var hash = PasswordHasher.HashPassword(password, salt);

            DbManager.AddUser(new User(username, email, Convert.ToBase64String(salt), Convert.ToBase64String(hash)));

            //// Генерируем код подтверждения и отправляем на email пользователя, используя почту разработчика
            //var code = new Random().Next(100000, 999999).ToString();
            //var subject = "Код подтверждения регистрации";
            //var body = $"Здравствуйте, {username}!\nВаш код подтверждения: {code}\nНе передавайте этот код никому.";

            //try
            //{
            //    SendRegistrationEmail(smtpHost, smtpPort, smtpUser, smtpPass, smtpEnableSsl, email, subject, body);
            //    MessageBox.Show("Пользователь зарегистрирован. Код подтверждения отправлен на указанный Email.", "Успех",
            //        MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    // При необходимости сохраните код для последующей проверки (не реализовано здесь)
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Регистрация успешна, но при отправке письма произошла ошибка:\n" + ex.Message, "Внимание",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }


        // --- Работа с локальным хранилищем пользователей ---

        // --- Отправка письма при регистрации ---
        private void SendRegistrationEmail(string host, int port, string user, string pass, bool enableSsl, string toEmail, string subject, string body)
        {
            var from = new MailAddress(user);
            var to = new MailAddress(toEmail);
            using var msg = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            using var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(user, pass),
                EnableSsl = enableSsl
            };

            smtp.Send(msg);
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

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