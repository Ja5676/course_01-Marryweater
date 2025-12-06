using WinFormsApp2.Models;
using WinFormsApp2.Services;

namespace WinFormsApp2.Panels
{
    public partial class LotDetailPanel : UserControl
    {
        public event Action? OnBack;

        private int lotId;
        private Lot? lot;

        public LotDetailPanel(int lotId)
        {
            this.lotId = lotId;
            InitializeComponent();
            LoadLotData();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScroll = true;
            BackColor = Color.FromArgb(245, 245, 245);
            Padding = new Padding(20);

            var btnBack = new Button
            {
                Text = "<- Назад до каталогу",
                Location = new Point(20, 10),
                Size = new Size(180, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };
            btnBack.Click += (s, e) => OnBack?.Invoke();

            Controls.Add(btnBack);
            ResumeLayout();
        }

        private void LoadLotData()
        {
            try
            {
                lot = LotManager.GetLotById(lotId);
                if (lot == null)
                {
                    MessageBox.Show("Оголошення не знайдено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    OnBack?.Invoke();
                    return;
                }

                BuildUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження: " + ex.Message);
            }
        }

        private void BuildUI()
        {
            if (lot == null) return;

            SuspendLayout();

            var mainContainer = new TableLayoutPanel
            {
                Location = new Point(20, 55),
                Size = new Size(Width - 60, Height - 80),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                ColumnCount = 2,
                RowCount = 1
            };
            mainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mainContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            var leftPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            var mainImage = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 240, 240),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            if (!string.IsNullOrEmpty(lot.ImagePath) && File.Exists(lot.ImagePath))
            {
                try
                {
                    mainImage.Image = Image.FromFile(lot.ImagePath);
                }
                catch { }
            }
            else
            {
                var lblNoImage = new Label
                {
                    Text = "Немає зображення",
                    Font = new Font("Segoe UI", 16),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    ForeColor = Color.Gray
                };
                mainImage.Controls.Add(lblNoImage);
            }

            leftPanel.Controls.Add(mainImage);

            var rightPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };

            var lblTitle = new Label
            {
                Text = lot.Title,
                Location = new Point(20, 20),
                Size = new Size(400, 60),
                Font = new Font("Segoe UI", 18, FontStyle.Bold)
            };

            var lblPrice = new Label
            {
                Text = lot.Price.ToString("N0") + " грн.",
                Location = new Point(20, 85),
                AutoSize = true,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(76, 175, 80)
            };

            var lblCategory = new Label
            {
                Text = "Категорія: " + (lot.CategoryName ?? "Не вказано"),
                Location = new Point(20, 135),
                AutoSize = true,
                Font = new Font("Segoe UI", 11)
            };

            var lblSeller = new Label
            {
                Text = "Продавець: " + lot.SellerName,
                Location = new Point(20, 165),
                AutoSize = true,
                Font = new Font("Segoe UI", 11)
            };

            var lblDate = new Label
            {
                Text = "Розміщено: " + lot.DateCreated.ToString("dd.MM.yyyy"),
                Location = new Point(20, 195),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray
            };

            var lblDescTitle = new Label
            {
                Text = "Опис:",
                Location = new Point(20, 235),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            var txtDescription = new TextBox
            {
                Text = lot.Description ?? "Опис відсутній",
                Location = new Point(20, 265),
                Size = new Size(400, 150),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(250, 250, 250)
            };

            var btnContact = new Button
            {
                Text = "Зв'язатися з продавцем",
                Location = new Point(20, 430),
                Size = new Size(250, 45),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnContact.Click += (s, e) =>
            {
                MessageBox.Show("Контакт продавця: " + lot.SellerName + "\n\nФункція повідомлень буде додана у наступній версії.",
                    "Контакт", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            rightPanel.Controls.AddRange([
                lblTitle, lblPrice, lblCategory, lblSeller, lblDate,
                    lblDescTitle, txtDescription, btnContact
            ]);

            mainContainer.Controls.Add(leftPanel, 0, 0);
            mainContainer.Controls.Add(rightPanel, 1, 0);

            Controls.Add(mainContainer);
            ResumeLayout();
        }
    }
}
