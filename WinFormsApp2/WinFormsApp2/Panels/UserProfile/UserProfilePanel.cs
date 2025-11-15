using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2.Models;
using WinFormsApp2.Panels.UserProfile;
using WinFormsApp2.Services;

namespace WinFormsApp2.Panels
{
    public partial class UserProfilePanel : UserControl
    {
        public static User User;
        public UserProfilePanel(User user)
        {
            InitializeComponent();
            User = user;
            Username.Text = user.Username;

            if (user.ProfilePicture == null)
            {
                ProfilePhoto.Image = Properties.Resources.BlankProfile;
            }
            else
            {
                using (var ms = new MemoryStream(user.ProfilePicture))
                {
                    ProfilePhoto.Image = Image.FromStream(ms);
                }
            }
        }

        private void btnYourAds_click(object sender, EventArgs e)
        {
            Controls.Clear();
            var userAds = new UserAds(User, FindForm().Size)
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(userAds);
        }

        private void ProfilePhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Title = "Виберіть фото";

            if (ofd.ShowDialog() == DialogResult.OK) { }

            using (MemoryStream ms = new MemoryStream())
            {
                if (ofd.FileName.Any())
                {
                    ProfilePhoto.Image = Image.FromFile(ofd.FileName);
                    Image.FromFile(ofd.FileName).Save(ms, ImageFormat.Jpeg);
                    DbManager.UpdateUserPhoto(User.Id, ms.ToArray());
                    User.ProfilePicture = ms.ToArray();
                }
            }
        }
    }
}
