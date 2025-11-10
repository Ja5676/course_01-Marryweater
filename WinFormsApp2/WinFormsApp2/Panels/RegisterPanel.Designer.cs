using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2
{
    partial class RegisterPanel : UserControl
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtEmail;
        private Button btnRegister;

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
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            txtEmail = new TextBox();
            btnRegister = new Button();
            btnBack = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold);
            txtUsername.Location = new Point(200, 120);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Username";
            txtUsername.Size = new Size(200, 27);
            txtUsername.TabIndex = 6;
            txtUsername.TextAlign = HorizontalAlignment.Center;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold);
            txtPassword.Location = new Point(200, 160);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Password";
            txtPassword.Size = new Size(200, 27);
            txtPassword.TabIndex = 4;
            txtPassword.TextAlign = HorizontalAlignment.Center;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold);
            txtEmail.Location = new Point(200, 200);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Email";
            txtEmail.Size = new Size(200, 27);
            txtEmail.TabIndex = 2;
            txtEmail.TextAlign = HorizontalAlignment.Center;
            // 
            // btnRegister
            // 
            btnRegister.Font = new Font("Trebuchet MS", 16.2F, FontStyle.Bold);
            btnRegister.ForeColor = Color.FromArgb(0, 113, 251);
            btnRegister.Location = new Point(225, 275);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(150, 50);
            btnRegister.TabIndex = 1;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += BtnRegister_Click;
            // 
            // btnBack
            // 
            btnBack.BackgroundImage = Properties.Resources.back1;
            btnBack.BackgroundImageLayout = ImageLayout.Stretch;
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Location = new Point(3, 3);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(48, 48);
            btnBack.TabIndex = 7;
            btnBack.TextImageRelation = TextImageRelation.ImageAboveText;
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += Back;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Font = new Font("Trebuchet MS", 31.8000011F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.FromArgb(0, 113, 251);
            label1.Location = new Point(135, 21);
            label1.Name = "label1";
            label1.Size = new Size(330, 67);
            label1.TabIndex = 8;
            label1.Text = "Registration";
            // 
            // RegisterPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = SystemColors.Control;
            Controls.Add(label1);
            Controls.Add(btnBack);
            Controls.Add(btnRegister);
            Controls.Add(txtEmail);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Name = "RegisterPanel";
            Size = new Size(600, 400);
            Load += UserControl1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBack;
        private Label label1;
    }
}
