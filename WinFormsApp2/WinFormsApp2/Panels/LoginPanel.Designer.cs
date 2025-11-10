namespace WinFormsApp2.Panels
{
    partial class LoginPanel
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
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

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            btnSignIn = new Button();
            tbUsername = new TextBox();
            tbPassword = new TextBox();
            btnBack = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnSignIn
            // 
            btnSignIn.Cursor = Cursors.Hand;
            btnSignIn.Font = new Font("Trebuchet MS", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSignIn.ForeColor = Color.FromArgb(0, 113, 251);
            btnSignIn.Location = new Point(225, 275);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Size = new Size(150, 50);
            btnSignIn.TabIndex = 1;
            btnSignIn.Text = "Sign In";
            btnSignIn.UseVisualStyleBackColor = true;
            btnSignIn.Click += BtnSignIn_Click;
            // 
            // tbUsername
            // 
            tbUsername.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold);
            tbUsername.Location = new Point(200, 143);
            tbUsername.Name = "tbUsername";
            tbUsername.PlaceholderText = "Username";
            tbUsername.Size = new Size(200, 27);
            tbUsername.TabIndex = 0;
            tbUsername.TextAlign = HorizontalAlignment.Center;
            // 
            // tbPassword
            // 
            tbPassword.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            tbPassword.ImeMode = ImeMode.On;
            tbPassword.Location = new Point(200, 183);
            tbPassword.Name = "tbPassword";
            tbPassword.PlaceholderText = "Password";
            tbPassword.Size = new Size(200, 27);
            tbPassword.TabIndex = 2;
            tbPassword.TextAlign = HorizontalAlignment.Center;
            tbPassword.UseSystemPasswordChar = true;
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
            btnBack.TabIndex = 3;
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
            label1.Location = new Point(200, 21);
            label1.Name = "label1";
            label1.Size = new Size(195, 67);
            label1.TabIndex = 9;
            label1.Text = "Sign In";
            // 
            // LoginPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(label1);
            Controls.Add(btnBack);
            Controls.Add(tbPassword);
            Controls.Add(btnSignIn);
            Controls.Add(tbUsername);
            Name = "LoginPanel";
            Size = new Size(600, 400);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnSignIn;
        private TextBox tbUsername;
        private TextBox tbPassword;
        private Button btnBack;
        private Label label1;
    }
}
