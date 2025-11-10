using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2.Panels
{
    public partial class AuthorizationPanel : UserControl
    {
        public AuthorizationPanel()
        {
            InitializeComponent();
        }

        private void OpenSingIn(object sender, EventArgs e)
        {
            // Переход: заменяем содержимое формы на UserControl1
            Controls.Clear();
            var uc = new LoginPanel
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(uc);
        }

        private void OpenSignUp(object sender, EventArgs e)
        {
            Controls.Clear();
            var uc = new RegisterPanel
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(uc);
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
