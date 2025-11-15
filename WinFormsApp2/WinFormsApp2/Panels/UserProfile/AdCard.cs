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
    public partial class AdCard : UserControl
    {
        public static Ads Ad { get; set; }
        public AdCard()
        {
            InitializeComponent();
        }

        public AdCard(Ads ad)
        {
            InitializeComponent();
            Ad = ad;

            Image img = null;
            if (ad.Image != null)
            {
                using (var ms = new MemoryStream(ad.Image))
                    img = Image.FromStream(ms);
            }

            AdLable.Text = ad.Title;
            AdDescription.Text = ad.Description;
            AdPrice.Text = ad.Price.ToString();
            AdPicture.Image = img;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this ad?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DbManager.DeleteAd(Ad);
            }
        }
    }
}
