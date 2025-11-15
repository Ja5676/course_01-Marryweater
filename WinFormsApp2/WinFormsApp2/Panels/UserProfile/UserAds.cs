using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2.Models;
using WinFormsApp2.Services;

namespace WinFormsApp2.Panels.UserProfile
{
    public partial class UserAds : UserControl
    {
        public static User User;
        public UserAds(User user, Size size)
        {
            InitializeComponent();
            User = user;
            flowAds.Size = size;
            LoadUserAds();
        }

        private void LoadUserAds()
        {

            flowAds.Controls.Clear();

            var ads = DbManager.GetAdsFromDatabase().ToList();
            var userAds = ads.Select(ad => ad).Where(ad => ad.UserId == User.Id).ToList();

            if (!userAds.Any())
            {
                MessageBox.Show("There are no publications", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (var ad in ads)
            {
                var card = new AdCard(ad);
                flowAds.Controls.Add(card);
            }
        }

        private void btnCreateAd_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            var userAds = new AdCardCreate(User)
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(userAds);
            FindForm().Size = new Size(600, 600);
            
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Controls.Clear();
            var userAds = new UserProfilePanel(User)
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(userAds);
        }
    }
}
