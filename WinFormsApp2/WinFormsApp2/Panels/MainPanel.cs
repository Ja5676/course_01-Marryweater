namespace WinFormsApp2.Panels
{
    public partial class MainPanel : UserControl
    {
        private Panel navigationPanel = null!;
        private Panel contentPanel = null!;
        private Button btnCatalog = null!;
        private Button btnAddLot = null!;
        private Button btnCabinet = null!;
        private Button btnSupport = null!;
        private Button btnLogout = null!;

        public MainPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            Size = new Size(1200, 800);
            BackColor = Color.FromArgb(245, 245, 245);

            navigationPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(33, 150, 243),
                Padding = new Padding(10)
            };

            var lblTitle = new Label
            {
                Text = "LotFlow",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };

            btnCatalog = CreateNavButton("Каталог", 150);
            btnCatalog.Click += BtnCatalog_Click;

            btnAddLot = CreateNavButton("Додати", 280);
            btnAddLot.Click += BtnAddLot_Click;

            btnCabinet = CreateNavButton("Кабінет", 410);
            btnCabinet.Click += BtnCabinet_Click;

            btnSupport = CreateNavButton("Підтримка", 540);
            btnSupport.Click += BtnSupport_Click;

            btnLogout = CreateNavButton("Вийти", 680);
            btnLogout.Click += BtnLogout_Click;

            navigationPanel.Controls.AddRange([lblTitle, btnCatalog, btnAddLot, btnCabinet, btnSupport, btnLogout]);

            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            Controls.Add(contentPanel);
            Controls.Add(navigationPanel);

            ResumeLayout();

            ShowHomeContent();
        }

        private Button CreateNavButton(string text, int x)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, 12),
                Size = new Size(120, 36),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand
            };
        }

        private void ShowHomeContent()
        {
            contentPanel.Controls.Clear();
            var homePanel = new HomePanel { Dock = DockStyle.Fill };
            homePanel.OnNavigateToCatalog += () => BtnCatalog_Click(this, EventArgs.Empty);
            homePanel.OnNavigateToAddLot += () => BtnAddLot_Click(this, EventArgs.Empty);
            contentPanel.Controls.Add(homePanel);
        }

        private void BtnCatalog_Click(object? sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            var catalogPanel = new CatalogPanel { Dock = DockStyle.Fill };
            catalogPanel.OnLotSelected += (lotId) =>
            {
                contentPanel.Controls.Clear();
                var detailPanel = new LotDetailPanel(lotId) { Dock = DockStyle.Fill };
                detailPanel.OnBack += () => BtnCatalog_Click(this, EventArgs.Empty);
                contentPanel.Controls.Add(detailPanel);
            };
            contentPanel.Controls.Add(catalogPanel);
        }

        private void BtnAddLot_Click(object? sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            var editorPanel = new LotEditorPanel { Dock = DockStyle.Fill };
            editorPanel.OnSaved += () => BtnCabinet_Click(this, EventArgs.Empty);
            editorPanel.OnCancel += () => ShowHomeContent();
            contentPanel.Controls.Add(editorPanel);
        }

        private void BtnCabinet_Click(object? sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            var cabinetPanel = new CabinetPanel { Dock = DockStyle.Fill };
            cabinetPanel.OnEditLot += (lotId) =>
            {
                contentPanel.Controls.Clear();
                var editorPanel = new LotEditorPanel(lotId) { Dock = DockStyle.Fill };
                editorPanel.OnSaved += () => BtnCabinet_Click(this, EventArgs.Empty);
                editorPanel.OnCancel += () => BtnCabinet_Click(this, EventArgs.Empty);
                contentPanel.Controls.Add(editorPanel);
            };
            contentPanel.Controls.Add(cabinetPanel);
        }

        private void BtnSupport_Click(object? sender, EventArgs e)
        {
            contentPanel.Controls.Clear();
            var supportPanel = new SupportPanel { Dock = DockStyle.Fill };
            contentPanel.Controls.Add(supportPanel);
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("Ви впевнені, що хочете вийти?", "Вихід",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Services.SessionManager.Logout();
                var form = FindForm();
                if (form != null)
                {
                    form.Controls.Clear();
                    form.Controls.Add(new AuthorizationPanel { Dock = DockStyle.Fill });
                }
            }
        }
    }
}
