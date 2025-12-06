using WinFormsApp2.Models;
using WinFormsApp2.Services;

namespace WinFormsApp2.Panels
{
    public partial class LotEditorPanel : UserControl
    {
        public event Action? OnSaved;
        public event Action? OnCancel;

        private int? editingLotId;
        private TextBox txtTitle = null!;
        private TextBox txtDescription = null!;
        private TextBox txtPrice = null!;
        private ComboBox cmbCategory = null!;
        private TextBox txtImagePath = null!;
        private PictureBox picPreview = null!;

        public LotEditorPanel(int? lotId = null)
        {
            editingLotId = lotId;
            InitializeComponent();
            LoadCategories();

            if (lotId.HasValue)
            {
                LoadLotData(lotId.Value);
            }
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScroll = true;
            BackColor = Color.FromArgb(245, 245, 245);
            Padding = new Padding(20);

            string headerText = editingLotId.HasValue ? "Редагування оголошення" : "Нове оголошення";
            var lblHeader = new Label
            {
                Text = headerText,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 50
            };

            var formPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 350,
                ColumnCount = 2,
                RowCount = 6,
                Padding = new Padding(10),
                BackColor = Color.White
            };
            formPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            formPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            formPanel.Controls.Add(CreateLabel("Назва *"), 0, 0);
            txtTitle = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 11) };
            formPanel.Controls.Add(txtTitle, 1, 0);

            formPanel.Controls.Add(CreateLabel("Опис"), 0, 1);
            txtDescription = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                Height = 80,
                Font = new Font("Segoe UI", 10),
                ScrollBars = ScrollBars.Vertical
            };
            formPanel.Controls.Add(txtDescription, 1, 1);

            formPanel.Controls.Add(CreateLabel("Ціна (грн.) *"), 0, 2);
            txtPrice = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 11) };
            formPanel.Controls.Add(txtPrice, 1, 2);

            formPanel.Controls.Add(CreateLabel("Категорія *"), 0, 3);
            cmbCategory = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            formPanel.Controls.Add(cmbCategory, 1, 3);

            formPanel.Controls.Add(CreateLabel("Зображення"), 0, 4);
            var imagePanel = new Panel { Dock = DockStyle.Fill };
            txtImagePath = new TextBox
            {
                Location = new Point(0, 0),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true
            };
            var btnBrowse = new Button
            {
                Text = "Обрати...",
                Location = new Point(310, 0),
                Size = new Size(100, 25),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White
            };
            btnBrowse.Click += BtnBrowse_Click;
            imagePanel.Controls.AddRange([txtImagePath, btnBrowse]);
            formPanel.Controls.Add(imagePanel, 1, 4);

            var previewPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 200,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            var lblPreview = new Label
            {
                Text = "Попередній перегляд зображення:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 30
            };

            picPreview = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            previewPanel.Controls.Add(picPreview);
            previewPanel.Controls.Add(lblPreview);

            var buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(10),
                FlowDirection = FlowDirection.LeftToRight
            };

            var btnSave = new Button
            {
                Text = "Зберегти",
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Margin = new Padding(5)
            };
            btnSave.Click += BtnSave_Click;

            var btnCancel = new Button
            {
                Text = "Скасувати",
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11),
                Margin = new Padding(5)
            };
            btnCancel.Click += (s, e) => OnCancel?.Invoke();

            buttonsPanel.Controls.AddRange([btnSave, btnCancel]);

            Controls.Add(buttonsPanel);
            Controls.Add(previewPanel);
            Controls.Add(formPanel);
            Controls.Add(lblHeader);

            ResumeLayout();
        }

        private Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private void LoadCategories()
        {
            try
            {
                var categories = LotManager.GetAllCategories();
                foreach (var cat in categories)
                {
                    cmbCategory.Items.Add(new CategoryItem { Id = cat.Id, Name = cat.Name });
                }
                if (cmbCategory.Items.Count > 0)
                    cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження категорій: " + ex.Message);
            }
        }

        private void LoadLotData(int lotId)
        {
            try
            {
                var lot = LotManager.GetLotById(lotId);
                if (lot == null)
                {
                    MessageBox.Show("Оголошення не знайдено!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    OnCancel?.Invoke();
                    return;
                }

                txtTitle.Text = lot.Title;
                txtDescription.Text = lot.Description;
                txtPrice.Text = lot.Price.ToString();
                txtImagePath.Text = lot.ImagePath ?? "";

                for (int i = 0; i < cmbCategory.Items.Count; i++)
                {
                    if (cmbCategory.Items[i] is CategoryItem cat && cat.Id == lot.CategoryId)
                    {
                        cmbCategory.SelectedIndex = i;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(lot.ImagePath) && File.Exists(lot.ImagePath))
                {
                    picPreview.Image = Image.FromFile(lot.ImagePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження даних: " + ex.Message);
            }
        }

        private void BtnBrowse_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Зображення|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Оберіть зображення"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtImagePath.Text = ofd.FileName;
                try
                {
                    picPreview.Image = Image.FromFile(ofd.FileName);
                }
                catch
                {
                    MessageBox.Show("Не вдалося завантажити зображення", "Помилка");
                }
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
                errors.Add("Введіть назву");

            if (!decimal.TryParse(txtPrice.Text, out var price) || price <= 0)
                errors.Add("Ціна повинна бути більшою за нуль");

            if (cmbCategory.SelectedItem == null)
                errors.Add("Оберіть категорію");

            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Помилка валідації",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var lot = new Lot
                {
                    UserId = SessionManager.CurrentUser!.Id,
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Price = price,
                    CategoryId = ((CategoryItem)cmbCategory.SelectedItem!).Id,
                    ImagePath = txtImagePath.Text
                };

                if (editingLotId.HasValue)
                {
                    lot.Id = editingLotId.Value;
                    LotManager.UpdateLot(lot);
                    MessageBox.Show("Оголошення оновлено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LotManager.CreateLot(lot);
                    MessageBox.Show("Оголошення створено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                OnSaved?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка збереження: " + ex.Message, "Помилка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class CategoryItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public override string ToString() => Name;
        }
    }
}
