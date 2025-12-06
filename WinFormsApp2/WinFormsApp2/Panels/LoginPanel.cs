using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                MessageBox.Show("Введіть логін та пароль", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = DbManager.LoginUser(username, password);
            if (user != null)
            {
                SessionManager.Login(user);
                
                MessageBox.Show("Вхід Успішний!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                var form = FindForm();
                if (form != null)
                {
                    form.Size = new Size(1200, 800);
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.Text = "LotFlow";
                    form.Controls.Clear();
                    form.Controls.Add(new MainPanel { Dock = DockStyle.Fill });
                }
            }
            else
            {
                MessageBox.Show("Неправильний логін або пароль", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
