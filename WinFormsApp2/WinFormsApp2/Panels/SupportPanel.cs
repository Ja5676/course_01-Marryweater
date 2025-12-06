namespace WinFormsApp2.Panels
{
    public partial class SupportPanel : UserControl
    {
        public SupportPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            BackColor = Color.FromArgb(245, 245, 245);
            AutoScroll = true;
            Padding = new Padding(30);

            var lblHeader = new Label
            {
                Text = "Підтримка",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 60
            };

            var infoPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 300,
                BackColor = Color.White,
                Padding = new Padding(30)
            };

            var lblInfo = new Label
            {
                Text = "LotFlow - ваша платформа для оголошень\r\n\r\n" +
                       "Як користуватися:\r\n\r\n" +
                       "* Каталог - перегляд усіх активних оголошень\r\n" +
                       "* Додати - створення нового оголошення\r\n" +
                       "* Кабінет - керування вашими оголошеннями\r\n" +
                       "* Пошук - пошук за назвою та категоріями\r\n\r\n" +
                       "Поради:\r\n" +
                       "- Додавайте якісні фотографії\r\n" +
                       "- Пишіть детальні описи\r\n" +
                       "- Вказуйте актуальну ціну\r\n" +
                       "- Перевіряйте контактні дані\r\n\r\n" +
                       "Зв'язатися з нами:\r\n" +
                       "Email: support@lotflow.ua\r\n" +
                       "Телефон: +380 (99) 123-45-67\r\n\r\n" +
                       "(c) 2024 LotFlow. Усі права захищені.",
                Font = new Font("Segoe UI", 11),
                Dock = DockStyle.Fill
            };

            infoPanel.Controls.Add(lblInfo);

            var faqPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 250,
                BackColor = Color.FromArgb(250, 250, 250),
                Padding = new Padding(30, 20, 30, 20)
            };

            var lblFaq = new Label
            {
                Text = "Часті запитання:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 35
            };

            var txtFaq = new Label
            {
                Text = "Запитання: Як додати оголошення?\r\n" +
                       "Відповідь: Натисніть кнопку «Додати» у меню навігації та заповніть форму.\r\n\r\n" +
                       "Запитання: Як редагувати оголошення?\r\n" +
                       "Відповідь: Перейдіть у «Кабінет» і натисніть «Редагувати» на потрібному оголошенні.\r\n\r\n" +
                       "Запитання: Як видалити оголошення?\r\n" +
                       "Відповідь: У розділі «Кабінет» натисніть «Видалити» на оголошенні.\r\n\r\n" +
                       "Запитання: Скільки фото можна додати?\r\n" +
                       "Відповідь: Ви можете додати одне фото до оголошення.",
                Font = new Font("Segoe UI", 10),
                Dock = DockStyle.Fill
            };

            faqPanel.Controls.Add(txtFaq);
            faqPanel.Controls.Add(lblFaq);

            Controls.Add(faqPanel);
            Controls.Add(infoPanel);
            Controls.Add(lblHeader);

            ResumeLayout();
        }
    }
}
