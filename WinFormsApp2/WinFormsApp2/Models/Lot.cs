namespace WinFormsApp2.Models
{
    public class Lot
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? ImagePath { get; set; }
        public DateTime DateCreated { get; set; }
        public string? CategoryName { get; set; }
        public string? SellerName { get; set; }
    }
}
