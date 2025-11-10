using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2
{
    partial class UserControl1 : UserControl
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnRegister;
        private Button btnLogin;

        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.btnRegister = new Button();
            this.btnLogin = new Button();

            this.SuspendLayout();

            // lblUsername
            this.lblUsername.Location = new Point(10, 10);
            this.lblUsername.Size = new Size(80, 23);
            this.lblUsername.Text = "Логин:";

            // txtUsername
            this.txtUsername.Location = new Point(100, 10);
            this.txtUsername.Size = new Size(200, 23);
            this.txtUsername.Name = "txtUsername";

            // lblPassword
            this.lblPassword.Location = new Point(10, 40);
            this.lblPassword.Size = new Size(80, 23);
            this.lblPassword.Text = "Пароль:";

            // txtPassword
            this.txtPassword.Location = new Point(100, 40);
            this.txtPassword.Size = new Size(200, 23);
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Name = "txtPassword";

            // lblEmail
            this.lblEmail.Location = new Point(10, 70);
            this.lblEmail.Size = new Size(80, 23);
            this.lblEmail.Text = "Email:";

            // txtEmail
            this.txtEmail.Location = new Point(100, 70);
            this.txtEmail.Size = new Size(200, 23);
            this.txtEmail.Name = "txtEmail";

            // btnRegister
            this.btnRegister.Location = new Point(10, 105);
            this.btnRegister.Size = new Size(140, 30);
            this.btnRegister.Text = "Зарегистрироваться";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Click += new EventHandler(this.BtnRegister_Click);

            // btnLogin
            this.btnLogin.Location = new Point(160, 105);
            this.btnLogin.Size = new Size(140, 30);
            this.btnLogin.Text = "Войти";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Click += new EventHandler(this.BtnLogin_Click);

            // UserControl1
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Name = "UserControl1";
            this.Size = new Size(320, 150);
            this.Load += new EventHandler(this.UserControl1_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
