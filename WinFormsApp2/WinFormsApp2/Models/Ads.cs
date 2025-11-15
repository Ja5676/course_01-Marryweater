using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Models
{
    public class Ads
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public byte[] Image { get; set; }
        public DateTime DatePosted { get; set; }

        public Ads()
        {
            
        }
        public Ads(string title, string description, decimal price, int categoryId, int userId, byte[] image, DateTime datePosted)
        {
            Title = title;
            Description = description;
            Price = price;
            CategoryId = categoryId;
            UserId = userId;
            Image = image;
            DatePosted = datePosted;
        }

    }
}
