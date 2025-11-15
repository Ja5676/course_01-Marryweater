namespace WinFormsApp2.Panels.UserProfile
{
    partial class UserAds
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
            btnBack = new Button();
            flowAds = new FlowLayoutPanel();
            btnCreateAd = new Button();
            SuspendLayout();
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
            btnBack.TabIndex = 5;
            btnBack.TextImageRelation = TextImageRelation.ImageAboveText;
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // flowAds
            // 
            flowAds.AutoScroll = true;
            flowAds.Location = new Point(3, 57);
            flowAds.Name = "flowAds";
            flowAds.Size = new Size(394, 440);
            flowAds.TabIndex = 6;
            // 
            // btnCreateAd
            // 
            btnCreateAd.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnCreateAd.ForeColor = Color.FromArgb(0, 113, 251);
            btnCreateAd.Location = new Point(247, 3);
            btnCreateAd.Name = "btnCreateAd";
            btnCreateAd.Size = new Size(150, 50);
            btnCreateAd.TabIndex = 7;
            btnCreateAd.Text = "Create New Ad";
            btnCreateAd.UseVisualStyleBackColor = true;
            btnCreateAd.Click += btnCreateAd_Click;
            // 
            // UserAds
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnCreateAd);
            Controls.Add(flowAds);
            Controls.Add(btnBack);
            Name = "UserAds";
            Size = new Size(400, 500);
            ResumeLayout(false);
        }

        #endregion

        private Button btnBack;
        private FlowLayoutPanel flowAds;
        private Button btnCreateAd;
    }
}
