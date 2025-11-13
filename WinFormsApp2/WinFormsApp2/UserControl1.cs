using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.Json;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class UserControl1 : UserControl
    {
        // Убрано: label «Це UserControl1»

        private readonly string dataDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "WinFormsApp2");
        private readonly string usersFileName = "users.json";

        // Почта разработчика и SMTP-настройки, вписанные в код (замените реальными данными)
        private const string developerEmail = "developer@example.com";
        private const string smtpHost = "smtp.example.com";
        private const int smtpPort = 587;
        private const string smtpUser = "developer@example.com";
        private const string smtpPass = "your_smtp_password"; // Заменить на пароль/токен
        private const bool smtpEnableSsl = true;

        public UserControl1()
        {
            InitializeComponent();

            Directory.CreateDirectory(dataDir);
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            // Ваш код инициализации при загрузке компонента
        }

        // Нажатие Зарегистрироваться
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;
            var email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var users = LoadUsers();
            if (users.Exists(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var salt = CreateSalt();
            var hash = HashPassword(password, salt);

            users.Add(new UserRecord
            {
                Username = username,
                Email = email,
                Salt = Convert.ToBase64String(salt),
                PasswordHash = Convert.ToBase64String(hash),
                Role = null
            });

            SaveUsers(users);

            // Генерируем код подтверждения и отправляем на email пользователя, используя почту разработчика
            var code = new Random().Next(100000, 999999).ToString();
            var subject = "Код подтверждения регистрации";
            var body = $"Здравствуйте, {username}!\nВаш код подтверждения: {code}\nНе передавайте этот код никому.";

            try
            {
                SendRegistrationEmail(smtpHost, smtpPort, smtpUser, smtpPass, smtpEnableSsl, email, subject, body);
                MessageBox.Show("Пользователь зарегистрирован. Код подтверждения отправлен на указанный Email.", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Переходим на UserControl2 после успешной регистрации
                SwitchToUserControl2();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Регистрация успешна, но при отправке письма произошла ошибка:\n" + ex.Message, "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Регистрация сохранена — тоже переходим на UserControl2
                SwitchToUserControl2();
            }
        }

        // Нажатие Войти
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;
            var email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Специальные жёстко заданные аккаунты (локальный обход)
            // Администратор: логин "ADM", пароль "1", email "1"
            // Тестувальник: логин "TST", пароль "1", email "1"
            if (username.Equals("ADM", StringComparison.OrdinalIgnoreCase) &&
                password == "1" &&
                email == "1")
            {
                MessageBox.Show("Вхід успішний — роль: Адміністратор", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SwitchToUserControl2();
                return;
            }

            if (username.Equals("TST", StringComparison.OrdinalIgnoreCase) &&
                password == "1" &&
                email == "1")
            {
                MessageBox.Show("Вхід успішний — роль: Тестувальник", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SwitchToUserControl2();
                return;
            }

            var users = LoadUsers();
            var user = users.Find(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var salt = Convert.FromBase64String(user.Salt);
            var hash = HashPassword(password, salt);
            if (Convert.ToBase64String(hash) == user.PasswordHash)
            {
                var roleInfo = string.IsNullOrEmpty(user.Role) ? "" : $" — роль: {user.Role}";
                MessageBox.Show("Вход успешен" + roleInfo, "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Переходим на UserControl2 после успешного входа
                SwitchToUserControl2();
            }
            else
            {
                MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- Работа с локальным хранилищем пользователей ---
        private List<UserRecord> LoadUsers()
        {
            try
            {
                var path = Path.Combine(dataDir, usersFileName);
                if (!File.Exists(path)) return new List<UserRecord>();
                var json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<UserRecord>>(json) ?? new List<UserRecord>();
            }
            catch
            {
                return new List<UserRecord>();
            }
        }

        private void SaveUsers(List<UserRecord> users)
        {
            var path = Path.Combine(dataDir, usersFileName);
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        // --- Хеширование паролей (Rfc2898) ---
        private static byte[] CreateSalt(int size = 16)
        {
            var salt = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        private static byte[] HashPassword(string password, byte[] salt, int iterations = 10000, int bytes = 32)
        {
            using var derive = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            return derive.GetBytes(bytes);
        }

        // --- Переход на UserControl2 ---
        private void SwitchToUserControl2()
        {
            var parentForm = this.FindForm();
            if (parentForm == null) return;

            parentForm.Controls.Clear();
            var uc2 = new UserControl2
            {
                Dock = DockStyle.Fill
            };
            parentForm.Controls.Add(uc2);
        }

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

        // --- Вспомогательные типы ---
        private class UserRecord
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Salt { get; set; }
            public string PasswordHash { get; set; }
            public string Role { get; set; }
        }
    }
}