using WinFormsApp2.Models;
using WinFormsApp2.Services;

namespace WinFormsApp2.Panels
{
    public partial class CatalogPanel : UserControl
    {
        public event Action<int>? OnLotSelected;

        private TextBox txtSearch = null!;
        private ComboBox cmbCategory = null!;
        private TextBox txtMinPrice = null!;
        private TextBox txtMaxPrice = null!;
        private ComboBox cmbSort = null!;
        private FlowLayoutPanel lotsContainer = null!;
        private ListBox lstSuggestions = null!;

        public CatalogPanel()
        {
            InitializeComponent();
            LoadCategories();
            LoadLots();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            BackColor = Color.FromArgb(245, 245, 245);

            // Search Panel
            var searchPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 10, 20, 10)
            };

            txtSearch = new TextBox
            {
                Location = new Point(20, 15),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 12),
                PlaceholderText = "Пошук по назві..."
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            txtSearch.KeyDown += TxtSearch_KeyDown;

            lstSuggestions = new ListBox
            {
                Location = new Point(20, 45),
                Size = new Size(400, 100),
                Visible = false,
                Font = new Font("Segoe UI", 10)
            };
            lstSuggestions.Click += LstSuggestions_Click;

            var btnSearch = new Button
            {
                Text = "Знайти",
                Location = new Point(430, 12),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSearch.Click += (s, e) => LoadLots();

            searchPanel.Controls.AddRange([txtSearch, lstSuggestions, btnSearch]);

            // Filters Panel
            var filtersPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(250, 250, 250),
                Padding = new Padding(20, 5, 20, 5)
            };

            // Category filter
            var lblCategory = new Label { Text = "Категорія:", Location = new Point(20, 10), AutoSize = true };
            cmbCategory = new ComboBox
            {
                Location = new Point(20, 30),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategory.SelectedIndexChanged += (s, e) => LoadLots();

            // Price filters
            var lblPrice = new Label { Text = "Ціна:", Location = new Point(190, 10), AutoSize = true };
            txtMinPrice = new TextBox
            {
                Location = new Point(190, 30),
                Size = new Size(80, 25),
                PlaceholderText = "Від"
            };
            var lblTo = new Label { Text = "-", Location = new Point(275, 33), AutoSize = true };
            txtMaxPrice = new TextBox
            {
                Location = new Point(295, 30),
                Size = new Size(80, 25),
                PlaceholderText = "До"
            };

            // Sort
            var lblSort = new Label { Text = "Сортування:", Location = new Point(400, 10), AutoSize = true };
            cmbSort = new ComboBox
            {
                Location = new Point(400, 30),
                Size = new Size(140, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSort.Items.AddRange(["Нові", "Дешевші", "Дорогі"]);
            cmbSort.SelectedIndex = 0;
            cmbSort.SelectedIndexChanged += (s, e) => LoadLots();

            var btnApply = new Button
            {
                Text = "Застосувати фільтри",
                Location = new Point(20, 65),
                Size = new Size(150, 28),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnApply.Click += (s, e) => LoadLots();

            var btnReset = new Button
            {
                Text = "Скинути",
                Location = new Point(180, 65),
                Size = new Size(100, 28),
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnReset.Click += BtnReset_Click;

            filtersPanel.Controls.AddRange([
                lblCategory, cmbCategory,
                lblPrice, txtMinPrice, lblTo, txtMaxPrice,
                lblSort, cmbSort,
                btnApply, btnReset
            ]);

            // Lots Container
            lotsContainer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(245, 245, 245)
            };

            Controls.Add(lotsContainer);
            Controls.Add(filtersPanel);
            Controls.Add(searchPanel);

            ResumeLayout();
        }

        private void LoadCategories()
        {
            try
            {
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add(new CategoryItem { Id = 0, Name = "Всі каталоги" });

                var categories = LotManager.GetAllCategories();
                foreach (var cat in categories)
                {
                    cmbCategory.Items.Add(new CategoryItem { Id = cat.Id, Name = cat.Name });
                }
                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні каталогів: " + ex.Message);
            }
        }

        private void LoadLots()
        {
            try
            {
                var filter = new LotSearchFilter
                {
                    SearchText = txtSearch.Text,
                    CategoryId = cmbCategory.SelectedItem is CategoryItem cat && cat.Id > 0 ? cat.Id : null,
                    MinPrice = decimal.TryParse(txtMinPrice.Text, out var min) ? min : null,
                    MaxPrice = decimal.TryParse(txtMaxPrice.Text, out var max) ? max : null,
                    SortBy = (SortOption)cmbSort.SelectedIndex
                };

                var lots = LotManager.SearchLots(filter);
                DisplayLots(lots);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні оголошень: " + ex.Message);
            }
        }

        private void DisplayLots(List<Lot> lots)
        {
            lotsContainer.Controls.Clear();

            if (lots.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = "Оголошення не знайдене",
                    Font = new Font("Segoe UI", 14),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(20)
                };
                lotsContainer.Controls.Add(lblEmpty);
                return;
            }

            foreach (var lot in lots)
            {
                lotsContainer.Controls.Add(CreateTileCard(lot));
            }
        }

        private Panel CreateTileCard(Lot lot)
        {
            var card = new Panel
            {
                Size = new Size(220, 260),
                Margin = new Padding(10),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            card.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle,
                    Color.FromArgb(220, 220, 220), ButtonBorderStyle.Solid);
            };

            // Image
            var imgPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(200, 120),
                BackColor = Color.FromArgb(240, 240, 240)
            };

            var lblNoImage = new Label
            {
                Text = "[Фото]",
                Font = new Font("Segoe UI", 14),
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
            if (titleText.Length > 28)
            {
                titleText = titleText.Substring(0, 25) + "...";
            }

            var lblTitle = new Label
            {
                Text = titleText,
                Location = new Point(10, 135),
                Size = new Size(200, 45),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            var lblPrice = new Label
            {
                Text = lot.Price.ToString("N0") + " ГРН",
                Location = new Point(10, 175),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(76, 175, 80)
            };

            var lblCategory = new Label
            {
                Text = lot.CategoryName ?? "Без категорії",
                Location = new Point(10, 200),
                Size = new Size(200, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };

            var btnDetails = new Button
            {
                Text = "Більше",
                Location = new Point(10, 225),
                Size = new Size(200, 28),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDetails.Click += (s, e) => OnLotSelected?.Invoke(lot.Id);

            card.Controls.AddRange([imgPanel, lblTitle, lblPrice, lblCategory, btnDetails]);
            card.Click += (s, e) => OnLotSelected?.Invoke(lot.Id);

            return card;
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            if (txtSearch.Text.Length >= 2)
            {
                var suggestions = LotManager.GetSearchSuggestions(txtSearch.Text);
                if (suggestions.Count > 0)
                {
                    lstSuggestions.Items.Clear();
                    lstSuggestions.Items.AddRange(suggestions.ToArray());
                    lstSuggestions.Visible = true;
                    lstSuggestions.BringToFront();
                }
                else
                {
                    lstSuggestions.Visible = false;
                }
            }
            else
            {
                lstSuggestions.Visible = false;
            }
        }

        private void TxtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstSuggestions.Visible = false;
                LoadLots();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void LstSuggestions_Click(object? sender, EventArgs e)
        {
            if (lstSuggestions.SelectedItem != null)
            {
                txtSearch.Text = lstSuggestions.SelectedItem.ToString();
                lstSuggestions.Visible = false;
                LoadLots();
            }
        }

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            txtMinPrice.Clear();
            txtMaxPrice.Clear();
            cmbCategory.SelectedIndex = 0;
            cmbSort.SelectedIndex = 0;
            LoadLots();
        }

        private class CategoryItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public override string ToString() => Name;
        }
    }
}
