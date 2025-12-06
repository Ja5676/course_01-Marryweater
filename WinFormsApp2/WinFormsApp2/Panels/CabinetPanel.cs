using WinFormsApp2.Models;
using WinFormsApp2.Services;

namespace WinFormsApp2.Panels
{
    public partial class CabinetPanel : UserControl
    {
        public event Action<int>? OnEditLot;

        private FlowLayoutPanel lotsPanel = null!;

        public CabinetPanel()
        {
            InitializeComponent();
            LoadUserLots();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScroll = true;
            BackColor = Color.FromArgb(245, 245, 245);

            var profilePanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150,
                BackColor = Color.White,
                Padding = new Padding(30)
            };

            var avatarPanel = new Panel
            {
                Location = new Point(30, 25),
                Size = new Size(100, 100),
                BackColor = Color.FromArgb(33, 150, 243)
            };

            string avatarLetter = "?";
            if (SessionManager.CurrentUser?.Username != null && SessionManager.CurrentUser.Username.Length > 0)
            {
                avatarLetter = SessionManager.CurrentUser.Username[..1].ToUpper();
            }

            var lblAvatar = new Label
            {
                Text = avatarLetter,
                Font = new Font("Segoe UI", 40, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            avatarPanel.Controls.Add(lblAvatar);

            var lblUsername = new Label
            {
                Text = SessionManager.CurrentUser?.Username ?? "Гість",
                Location = new Point(150, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 20, FontStyle.Bold)
            };

            var lblEmail = new Label
            {
                Text = "Email: " + (SessionManager.CurrentUser?.Email ?? ""),
                Location = new Point(150, 70),
                AutoSize = true,
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Gray
            };

            string dateText = "Зареєстрований: ";
            if (SessionManager.CurrentUser != null)
            {
                dateText += SessionManager.CurrentUser.DateRegistered.ToString("dd.MM.yyyy");
            }
            var lblDate = new Label
            {
                Text = dateText,
                Location = new Point(150, 100),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray
            };

            profilePanel.Controls.AddRange([avatarPanel, lblUsername, lblEmail, lblDate]);

            var headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(30, 10, 30, 10)
            };

            var lblMyLots = new Label
            {
                Text = "Мої оголошення",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            headerPanel.Controls.Add(lblMyLots);

            lotsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(245, 245, 245)
            };

            Controls.Add(lotsPanel);
            Controls.Add(headerPanel);
            Controls.Add(profilePanel);

            ResumeLayout();
        }

        private void LoadUserLots()
        {
            try
            {
                lotsPanel.Controls.Clear();

                if (SessionManager.CurrentUser == null) return;

                var lots = LotManager.GetUserLots(SessionManager.CurrentUser.Id);

                if (lots.Count == 0)
                {
                    var lblEmpty = new Label
                    {
                        Text = "У вас поки немає оголошень\n\nНажміть <Додати> щоб створити!",
                        Font = new Font("Segoe UI", 14),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Margin = new Padding(50)
                    };
                    lotsPanel.Controls.Add(lblEmpty);
                    return;
                }

                foreach (var lot in lots)
                {
                    lotsPanel.Controls.Add(CreateLotCard(lot));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантажинні оголошень: " + ex.Message);
            }
        }

        private Panel CreateLotCard(Lot lot)
        {
            var card = new Panel
            {
                Size = new Size(500, 180),
                Margin = new Padding(10),
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            var imgPanel = new Panel
            {
                Location = new Point(15, 15),
                Size = new Size(100, 100),
                BackColor = Color.FromArgb(240, 240, 240)
            };

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
                    imgPanel.Controls.Add(pic);
                }
                catch
                {
                    AddNoImageLabel(imgPanel);
                }
            }
            else
            {
                AddNoImageLabel(imgPanel);
            }

            var lblTitle = new Label
            {
                Text = lot.Title,
                Location = new Point(130, 15),
                Size = new Size(card.Width - 150, 25),
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };

            var lblPrice = new Label
            {
                Text = lot.Price.ToString("N0") + " грн.",
                Location = new Point(130, 45),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(76, 175, 80)
            };

            var lblInfo = new Label
            {
                Text = (lot.CategoryName ?? "Без категорії") + " | " + lot.DateCreated.ToString("dd.MM.yyyy"),
                Location = new Point(130, 75),
                Size = new Size(card.Width - 150, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };

            var btnEdit = CreateActionButton("Редагувати", 130, Color.FromArgb(33, 150, 243));
            btnEdit.Click += (s, e) => OnEditLot?.Invoke(lot.Id);

            var btnCopy = CreateActionButton("Копіювати", 260, Color.FromArgb(156, 39, 176));
            btnCopy.Click += (s, e) =>
            {
                var newId = LotManager.CopyLot(lot.Id, SessionManager.CurrentUser!.Id);
                if (newId > 0)
                {
                    MessageBox.Show("Копія створена!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUserLots();
                }
            };

            var btnDelete = CreateActionButton("Видалити", 390, Color.FromArgb(244, 67, 54));
            btnDelete.Click += (s, e) =>
            {
                var result = MessageBox.Show("Ви впевнені, що хочете видалити?",
                    "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    LotManager.DeleteLot(lot.Id, SessionManager.CurrentUser!.Id);
                    LoadUserLots();
                }
            };

            card.Controls.AddRange([imgPanel, lblTitle, lblPrice, lblInfo, btnEdit, btnCopy, btnDelete]);
            return card;
        }

        private void AddNoImageLabel(Panel panel)
        {
            var lblNoImg = new Label
            {
                Text = "[Фото]",
                Font = new Font("Segoe UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                ForeColor = Color.Gray
            };
            panel.Controls.Add(lblNoImg);
        }

        private Button CreateActionButton(string text, int x, Color color)
        {
            const int NEW_Y_POSITION = 118;

            return new Button
            {
                Text = text,
                Location = new Point(x, NEW_Y_POSITION),
                Size = new Size(120, 28),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
        }
    }
}
