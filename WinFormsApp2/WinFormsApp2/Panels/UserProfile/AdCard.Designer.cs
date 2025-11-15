namespace WinFormsApp2.Panels.UserProfile
{
    partial class AdCard
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
            AdLable = new Label();
            AdDescription = new Label();
            AdPrice = new Label();
            btnDelete = new Button();
            btnEdit = new Button();
            ((System.ComponentModel.ISupportInitialize)AdPicture).BeginInit();
            SuspendLayout();
            // 
            // AdPicture
            // 
            AdPicture.Location = new Point(13, 3);
            AdPicture.Name = "AdPicture";
            AdPicture.Size = new Size(100, 100);
            AdPicture.SizeMode = PictureBoxSizeMode.Zoom;
            AdPicture.TabIndex = 0;
            AdPicture.TabStop = false;
            // 
            // AdLable
            // 
            AdLable.AutoSize = true;
            AdLable.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            AdLable.Location = new Point(13, 115);
            AdLable.Name = "AdLable";
            AdLable.Size = new Size(86, 23);
            AdLable.TabIndex = 1;
            AdLable.Text = "Ads Lable";
            AdLable.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AdDescription
            // 
            AdDescription.AutoSize = true;
            AdDescription.Font = new Font("Trebuchet MS", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            AdDescription.ForeColor = SystemColors.ControlDarkDark;
            AdDescription.Location = new Point(13, 138);
            AdDescription.Name = "AdDescription";
            AdDescription.Size = new Size(78, 18);
            AdDescription.TabIndex = 2;
            AdDescription.Text = "Description";
            AdDescription.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AdPrice
            // 
            AdPrice.AutoSize = true;
            AdPrice.Font = new Font("Trebuchet MS", 10.2F, FontStyle.Bold);
            AdPrice.Location = new Point(13, 218);
            AdPrice.Name = "AdPrice";
            AdPrice.Size = new Size(51, 23);
            AdPrice.TabIndex = 3;
            AdPrice.Text = "Price";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.IndianRed;
            btnDelete.Font = new Font("Trebuchet MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(94, 214);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(88, 27);
            btnDelete.TabIndex = 12;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.Font = new Font("Trebuchet MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnEdit.ForeColor = Color.FromArgb(0, 113, 251);
            btnEdit.Location = new Point(94, 181);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(88, 27);
            btnEdit.TabIndex = 13;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // AdCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(AdPrice);
            Controls.Add(AdDescription);
            Controls.Add(AdLable);
            Controls.Add(AdPicture);
            Name = "AdCard";
            Size = new Size(194, 250);
            ((System.ComponentModel.ISupportInitialize)AdPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox AdPicture;
        private Label AdLable;
        private Label AdDescription;
        private Label AdPrice;
        private Button btnDelete;
        private Button btnEdit;
    }
}
