namespace WinFormsApp2.Panels
{
    partial class UserProfilePanel
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
            ProfilePhoto = new PictureBox();
            Username = new Label();
            btnBack = new Button();
            btnUserPurchases = new Button();
            btnUserAds = new Button();
            btnFavorite = new Button();
            ((System.ComponentModel.ISupportInitialize)ProfilePhoto).BeginInit();
            SuspendLayout();
            // 
            // ProfilePhoto
            // 
            ProfilePhoto.Cursor = Cursors.Hand;
            ProfilePhoto.Location = new Point(200, 3);
            ProfilePhoto.Name = "ProfilePhoto";
            ProfilePhoto.Size = new Size(100, 100);
            ProfilePhoto.SizeMode = PictureBoxSizeMode.Zoom;
            ProfilePhoto.TabIndex = 0;
            ProfilePhoto.TabStop = false;
            ProfilePhoto.Click += ProfilePhoto_Click;
            // 
            // Username
            // 
            Username.AutoSize = true;
            Username.Font = new Font("Trebuchet MS", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Username.Location = new Point(196, 110);
            Username.Name = "Username";
            Username.Size = new Size(108, 26);
            Username.TabIndex = 1;
            Username.Text = "Username";
            Username.TextAlign = ContentAlignment.MiddleCenter;
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
            btnBack.TabIndex = 4;
            btnBack.TextImageRelation = TextImageRelation.ImageAboveText;
            btnBack.UseVisualStyleBackColor = true;
            // 
            // btnUserPurchases
            // 
            btnUserPurchases.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnUserPurchases.ForeColor = Color.FromArgb(0, 113, 251);
            btnUserPurchases.Location = new Point(175, 232);
            btnUserPurchases.Name = "btnUserPurchases";
            btnUserPurchases.Size = new Size(150, 50);
            btnUserPurchases.TabIndex = 5;
            btnUserPurchases.Text = "Your Purchases";
            btnUserPurchases.UseVisualStyleBackColor = true;
            // 
            // btnUserAds
            // 
            btnUserAds.Font = new Font("Trebuchet MS", 16.2F, FontStyle.Bold);
            btnUserAds.ForeColor = Color.FromArgb(0, 113, 251);
            btnUserAds.Location = new Point(175, 176);
            btnUserAds.Name = "btnUserAds";
            btnUserAds.Size = new Size(150, 50);
            btnUserAds.TabIndex = 6;
            btnUserAds.Text = "Your Ads";
            btnUserAds.UseVisualStyleBackColor = true;
            btnUserAds.Click += btnYourAds_click;
            // 
            // btnFavorite
            // 
            btnFavorite.Font = new Font("Trebuchet MS", 16.2F, FontStyle.Bold);
            btnFavorite.ForeColor = Color.FromArgb(0, 113, 251);
            btnFavorite.Location = new Point(175, 288);
            btnFavorite.Name = "btnFavorite";
            btnFavorite.Size = new Size(150, 50);
            btnFavorite.TabIndex = 7;
            btnFavorite.Text = "Favorite";
            btnFavorite.UseVisualStyleBackColor = true;
            // 
            // UserProfilePanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnFavorite);
            Controls.Add(btnUserAds);
            Controls.Add(btnUserPurchases);
            Controls.Add(btnBack);
            Controls.Add(Username);
            Controls.Add(ProfilePhoto);
            Name = "UserProfilePanel";
            Size = new Size(500, 350);
            ((System.ComponentModel.ISupportInitialize)ProfilePhoto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox ProfilePhoto;
        private Label Username;
        private Button btnBack;
        private Button btnUserPurchases;
        private Button btnUserAds;
        private Button btnFavorite;
    }
}
