namespace WinFormsApp2.Panels
{
    partial class AuthorizationPanel
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
            btnSignUp = new Button();
            pictureBox1 = new PictureBox();
            btnExit = new Button();
            btnSignIn = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnSignUp
            // 
            btnSignUp.Cursor = Cursors.Hand;
            btnSignUp.Font = new Font("Trebuchet MS", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSignUp.ForeColor = Color.FromArgb(0, 113, 251);
            btnSignUp.Location = new Point(218, 196);
            btnSignUp.Name = "btnSignUp";
            btnSignUp.Size = new Size(169, 49);
            btnSignUp.TabIndex = 7;
            btnSignUp.Text = "Sign up";
            btnSignUp.UseVisualStyleBackColor = true;
            btnSignUp.Click += OpenSignUp;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Image = Properties.Resources.LotFlowLogo;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(600, 83);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Trebuchet MS", 16.2F, FontStyle.Bold);
            btnExit.ForeColor = Color.FromArgb(0, 113, 251);
            btnExit.Location = new Point(218, 251);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(169, 43);
            btnExit.TabIndex = 5;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += Exit;
            // 
            // btnSignIn
            // 
            btnSignIn.Cursor = Cursors.Hand;
            btnSignIn.Font = new Font("Trebuchet MS", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSignIn.ForeColor = Color.FromArgb(0, 113, 251);
            btnSignIn.Location = new Point(218, 141);
            btnSignIn.Name = "btnSignIn";
            btnSignIn.Size = new Size(169, 49);
            btnSignIn.TabIndex = 4;
            btnSignIn.Text = "Sign In";
            btnSignIn.UseVisualStyleBackColor = true;
            btnSignIn.Click += OpenSingIn;
            // 
            // AuthorizationPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnSignUp);
            Controls.Add(pictureBox1);
            Controls.Add(btnExit);
            Controls.Add(btnSignIn);
            Name = "AuthorizationPanel";
            Size = new Size(600, 400);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnSignUp;
        private PictureBox pictureBox1;
        private Button btnExit;
        private Button btnSignIn;
    }
}
