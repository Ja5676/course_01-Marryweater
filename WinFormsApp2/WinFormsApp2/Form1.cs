using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp2.Panels;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(600, 400);
            Text = "Form1";

            Controls.Clear();
            var uc = new AuthorizationPanel
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(uc);
        }
    }
}