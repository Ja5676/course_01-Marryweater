namespace WinFormsApp2.Panels.UserProfile
{
    partial class AdCardCreate
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
            AdPicture = new PictureBox();
            AdLable = new TextBox();
            Description = new TextBox();
            Price = new TextBox();
            btnPublish = new Button();
            btnBack = new Button();
            Categories = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)AdPicture).BeginInit();
            SuspendLayout();
            // 
            // AdPicture
            // 
            AdPicture.Location = new Point(101, 3);
            AdPicture.Name = "AdPicture";
            AdPicture.Size = new Size(100, 100);
            AdPicture.SizeMode = PictureBoxSizeMode.Zoom;
            AdPicture.TabIndex = 4;
            AdPicture.TabStop = false;
            AdPicture.Click += AdPicture_Click;
            // 
            // AdLable
            // 
            AdLable.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold);
            AdLable.Location = new Point(57, 109);
            AdLable.Name = "AdLable";
            AdLable.PlaceholderText = "Ad Lable";
            AdLable.Size = new Size(180, 27);
            AdLable.TabIndex = 8;
            // 
            // Description
            // 
            Description.Font = new Font("Trebuchet MS", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Description.Location = new Point(34, 142);
            Description.Multiline = true;
            Description.Name = "Description";
            Description.PlaceholderText = "Description";
            Description.Size = new Size(233, 88);
            Description.TabIndex = 9;
            // 
            // Price
            // 
            Price.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold);
            Price.Location = new Point(34, 365);
            Price.Name = "Price";
            Price.PlaceholderText = "Price";
            Price.Size = new Size(104, 27);
            Price.TabIndex = 10;
            // 
            // btnPublish
            // 
            btnPublish.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnPublish.ForeColor = Color.FromArgb(0, 113, 251);
            btnPublish.Location = new Point(144, 365);
            btnPublish.Name = "btnPublish";
            btnPublish.Size = new Size(123, 27);
            btnPublish.TabIndex = 11;
            btnPublish.Text = "Publish";
            btnPublish.UseVisualStyleBackColor = true;
            btnPublish.Click += btnPublish_Click;
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
            btnBack.TabIndex = 12;
            btnBack.TextImageRelation = TextImageRelation.ImageAboveText;
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // Categories
            // 
            Categories.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold);
            Categories.ForeColor = SystemColors.WindowFrame;
            Categories.FormattingEnabled = true;
            Categories.Location = new Point(34, 257);
            Categories.Name = "Categories";
            Categories.Size = new Size(233, 31);
            Categories.TabIndex = 13;
            Categories.Text = "Categories";
            // 
            // AdCardCreate
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Categories);
            Controls.Add(btnBack);
            Controls.Add(btnPublish);
            Controls.Add(Price);
            Controls.Add(Description);
            Controls.Add(AdLable);
            Controls.Add(AdPicture);
            Cursor = Cursors.Hand;
            Name = "AdCardCreate";
            Size = new Size(300, 406);
            ((System.ComponentModel.ISupportInitialize)AdPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox AdPicture;
        private TextBox AdLable;
        private TextBox Description;
        private TextBox Price;
        private Button btnPublish;
        private Button btnBack;
        private ComboBox Categories;
    }
}
