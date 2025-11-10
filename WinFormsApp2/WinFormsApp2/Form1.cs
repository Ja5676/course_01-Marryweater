using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Привязываем обработчики событий кнопок (если не привязаны в Designer)
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;

            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(600, 400);
            Text = "Form1";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Переход: заменяем содержимое формы на UserControl1
            Controls.Clear();
            var uc = new UserControl1
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(uc);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // Выход из приложения
            Application.Exit();
        }

        // Пустые методы, на которые может ссылаться Designer
        private void label1_Click(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
    }
}