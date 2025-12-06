using WinFormsApp2.Models;
using WinFormsApp2.Services;

namespace WinFormsApp2.Panels
{
    public partial class HomePanel : UserControl
    {
        public event Action? OnNavigateToCatalog;
        public event Action? OnNavigateToAddLot;

        private FlowLayoutPanel newLotsPanel = null!;

        public HomePanel()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScroll = true;
            BackColor = Color.FromArgb(245, 245, 245);

            var welcomePanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150,
                BackColor = Color.FromArgb(63, 81, 181),
                Padding = new Padding(30)
            };

            string username = SessionManager.CurrentUser?.Username ?? "Гість";
            var lblWelcome = new Label
            {
                Text = "Ласкаво просимо, " + username + "!",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(30, 30)
            };

            var lblSubtitle = new Label
            {
                Text = "LotFlow - платформа для розміщення оголошень про товари та послуги",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(200, 200, 255),
                AutoSize = true,
                Location = new Point(30, 80)
            };

            var btnStartSelling = new Button
            {
                Text = "Розмістити оголошення",
                Location = new Point(30, 110),
                Size = new Size(220, 35),
                BackColor = Color.FromArgb(255, 152, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStartSelling.Click += (s, e) => OnNavigateToAddLot?.Invoke();

            welcomePanel.Controls.AddRange([lblWelcome, lblSubtitle, btnStartSelling]);

            var actionsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(20, 10, 20, 10),
                BackColor = Color.White
            };

            var btnBrowse = CreateActionButton("Переглянути каталог", Color.FromArgb(76, 175, 80));
            btnBrowse.Click += (s, e) => OnNavigateToCatalog?.Invoke();

            actionsPanel.Controls.Add(btnBrowse);

            var newSection = CreateSection("Останні оголошення", out newLotsPanel);

            Controls.Add(newSection);
            Controls.Add(actionsPanel);
            Controls.Add(welcomePanel);

            ResumeLayout();
        }

        private Button CreateActionButton(string text, Color color)
        {
            return new Button
            {
                Text = text,
                Size = new Size(200, 50),
                Margin = new Padding(10),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }

        private Panel CreateSection(string title, out FlowLayoutPanel contentPanel)
        {
            var section = new Panel
            {
                Dock = DockStyle.Top,
                Height = 280,
                Padding = new Padding(20, 10, 20, 10)
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 40
            };

            contentPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false,
                BackColor = Color.FromArgb(250, 250, 250)
            };

            section.Controls.Add(contentPanel);
            section.Controls.Add(lblTitle);

            return section;
        }

        private void LoadData()
        {
            try
            {
                var newLots = LotManager.GetNewLots(5);
                foreach (var lot in newLots)
                {
                    newLotsPanel.Controls.Add(CreateLotCard(lot));
                }

                if (newLots.Count == 0)
                {
                    var lblEmpty = new Label
                    {
                        Text = "Поки немає оголошень",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(20)
                    };
                    newLotsPanel.Controls.Add(lblEmpty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження даних: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateLotCard(Lot lot)
        {
            var card = new Panel
            {
                Size = new Size(200, 220),
                Margin = new Padding(10),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            card.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    Color.FromArgb(220, 220, 220), ButtonBorderStyle.Solid);
            };

            var imgPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(180, 100),
                BackColor = Color.FromArgb(240, 240, 240)
            };

            var lblNoImage = new Label
            {
                Text = "[Фото]",
                Font = new Font("Segoe UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                ForeColor = Color.Gray
            };
            imgPanel.Controls.Add(lblNoImage);

            if (!string.IsNullOrEmpty(lot.ImagePath) && File.Exists(lot.ImagePath))
            {
                try
                {
                    var pic = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        Image = Image.FromFile(lot.ImagePath),
                        SizeMode = PictureBoxSizeMode.Zoom
                    };
                    imgPanel.Controls.Clear();
                    imgPanel.Controls.Add(pic);
                }
                catch { }
            }

            string titleText = lot.Title;
            if (titleText.Length > 25)
            {
                titleText = titleText.Substring(0, 22) + "...";
            }

            var lblTitle = new Label
            {
                Text = titleText,
                Location = new Point(10, 115),
                Size = new Size(180, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            var lblPrice = new Label
            {
                Text = lot.Price.ToString("N0") + " грн.",
                Location = new Point(10, 155),
                Size = new Size(180, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(76, 175, 80)
            };

            var lblCategory = new Label
            {
                Text = lot.CategoryName ?? "Без категорії",
                Location = new Point(10, 180),
                Size = new Size(180, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };

            card.Controls.AddRange([imgPanel, lblTitle, lblPrice, lblCategory]);
            card.Click += (s, e) => OnNavigateToCatalog?.Invoke();

            return card;
        }
    }
}
