using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2.Models;
using WinFormsApp2.Services;

namespace WinFormsApp2.Panels.UserProfile
{
    public partial class AdCardCreate : UserControl
    {
        public static User User;
        public AdCardCreate(User user)
        {
            InitializeComponent();
            User = user;

            var categories = DbManager.GetCategories();

            if (categories.Any())
            {
                foreach (var category in DbManager.GetCategories())
                {
                    Categories.Items.Add(category.Name);
                }
            }
            else
            {
                Categories.Text = "No categories found";
            }
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            var userAds = new UserAds(User, FindForm().Size)
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(userAds);
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            if (!AdLable.Text.Any() || !Description.Text.Any() || Categories.SelectedItem == null || !Price.Text.Any())
            {
                MessageBox.Show("Fill all gaps", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AdPicture.Image == null)
            {
                MessageBox.Show("Upload a photo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int categoryId = DbManager.GetCategoryId(Categories.SelectedItem.ToString());
            Image img = AdPicture.Image;

            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Jpeg);
                DbManager.AddAd(new Ads(AdLable.Text, Description.Text, Convert.ToInt32(Price.Text), categoryId, User.Id, ms.ToArray(), DateTime.Now));
            }

        }

        private void AdPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Title = "Виберіть фото";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName.Any())
                {
                    AdPicture.Image = Image.FromFile(ofd.FileName);
                }
            }
        }
    }
}
