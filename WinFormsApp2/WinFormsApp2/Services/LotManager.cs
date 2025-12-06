using Dapper;
using Microsoft.Data.SqlClient;
using WinFormsApp2.Models;

namespace WinFormsApp2.Services
{
    public static class LotManager
    {
        private static readonly string connectionString = "Server=DESKTOP-024LTB5\\MSSQLSERVER01;Database=LotFlowDB;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;";
        public static List<Category> GetAllCategories()
        {
            using var db = new SqlConnection(connectionString);
            return db.Query<Category>("SELECT * FROM Categories ORDER BY Name").ToList();
        }

        public static Category? GetCategoryById(int id)
        {
            using var db = new SqlConnection(connectionString);
            return db.QueryFirstOrDefault<Category>("SELECT * FROM Categories WHERE Id = @Id", new { Id = id });
        }

        public static int CreateLot(Lot lot)
        {
            using var db = new SqlConnection(connectionString);
            var sql = @"INSERT INTO Ads (Title, Description, Price, CategoryId, UserId, ImagePath, DatePosted)
                        VALUES (@Title, @Description, @Price, @CategoryId, @UserId, @ImagePath, @DatePosted);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            lot.DateCreated = DateTime.Now;

            return db.ExecuteScalar<int>(sql, new
            {
                lot.Title,
                lot.Description,
                lot.Price,
                lot.CategoryId,
                lot.UserId,
                ImagePath = lot.ImagePath ?? "",
                DatePosted = lot.DateCreated
            });
        }
        public static bool UpdateLot(Lot lot)
        {
            using var db = new SqlConnection(connectionString);
            var sql = @"UPDATE Ads SET 
                        Title = @Title, 
                        Description = @Description, 
                        Price = @Price, 
                        CategoryId = @CategoryId,
                        ImagePath = @ImagePath
                        WHERE Id = @Id AND UserId = @UserId";

            return db.Execute(sql, new
            {
                lot.Id,
                lot.Title,
                lot.Description,
                lot.Price,
                lot.CategoryId,
                lot.UserId,
                ImagePath = lot.ImagePath ?? ""
            }) > 0;
        }
        public static bool DeleteLot(int lotId, int userId)
        {
            using var db = new SqlConnection(connectionString);
            return db.Execute("DELETE FROM Ads WHERE Id = @Id AND UserId = @UserId", new { Id = lotId, UserId = userId }) > 0;
        }

        public static Lot? GetLotById(int id)
        {
            using var db = new SqlConnection(connectionString);
            var sql = @"SELECT a.Id, a.Title, a.Description, a.Price, a.CategoryId, a.UserId, 
                               a.ImagePath, a.DatePosted AS DateCreated,
                               c.Name AS CategoryName, u.Username AS SellerName 
                        FROM Ads a 
                        LEFT JOIN Categories c ON a.CategoryId = c.Id
                        LEFT JOIN Users u ON a.UserId = u.Id
                        WHERE a.Id = @Id";
            return db.QueryFirstOrDefault<Lot>(sql, new { Id = id });
        }

        public static List<Lot> GetUserLots(int userId)
        {
            using var db = new SqlConnection(connectionString);
            var sql = @"SELECT a.Id, a.Title, a.Description, a.Price, a.CategoryId, a.UserId, 
                               a.ImagePath, a.DatePosted AS DateCreated,
                               c.Name AS CategoryName 
                        FROM Ads a 
                        LEFT JOIN Categories c ON a.CategoryId = c.Id
                        WHERE a.UserId = @UserId 
                        ORDER BY a.DatePosted DESC";
            return db.Query<Lot>(sql, new { UserId = userId }).ToList();
        }

        public static List<Lot> SearchLots(LotSearchFilter filter)
        {
            using var db = new SqlConnection(connectionString);

            var sql = @"SELECT a.Id, a.Title, a.Description, a.Price, a.CategoryId, a.UserId, 
                               a.ImagePath, a.DatePosted AS DateCreated,
                               c.Name AS CategoryName, u.Username AS SellerName 
                        FROM Ads a 
                        LEFT JOIN Categories c ON a.CategoryId = c.Id
                        LEFT JOIN Users u ON a.UserId = u.Id
                        WHERE 1=1";

            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                sql += " AND (a.Title LIKE @Search OR a.Description LIKE @Search)";
                parameters.Add("Search", $"%{filter.SearchText}%");
            }

            if (filter.CategoryId.HasValue)
            {
                sql += " AND a.CategoryId = @CategoryId";
                parameters.Add("CategoryId", filter.CategoryId.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                sql += " AND a.Price >= @MinPrice";
                parameters.Add("MinPrice", filter.MinPrice.Value);
            }
            if (filter.MaxPrice.HasValue)
            {
                sql += " AND a.Price <= @MaxPrice";
                parameters.Add("MaxPrice", filter.MaxPrice.Value);
            }

            sql += filter.SortBy switch
            {
                SortOption.Newest => " ORDER BY a.DatePosted DESC",
                SortOption.PriceAsc => " ORDER BY a.Price ASC",
                SortOption.PriceDesc => " ORDER BY a.Price DESC",
                _ => " ORDER BY a.DatePosted DESC"
            };

            return db.Query<Lot>(sql, parameters).ToList();
        }
        public static List<Lot> GetTopLots(int count = 10)
        {
            using var db = new SqlConnection(connectionString);
            var sql = @"SELECT TOP (@Count) a.Id, a.Title, a.Description, a.Price, a.CategoryId, a.UserId, 
                               a.ImagePath, a.DatePosted AS DateCreated,
                               c.Name AS CategoryName, u.Username AS SellerName 
                        FROM Ads a 
                        LEFT JOIN Categories c ON a.CategoryId = c.Id
                        LEFT JOIN Users u ON a.UserId = u.Id
                        ORDER BY a.DatePosted DESC";
            return db.Query<Lot>(sql, new { Count = count }).ToList();
        }
        public static List<Lot> GetNewLots(int count = 10)
        {
            return GetTopLots(count);
        }
        public static int CopyLot(int lotId, int userId)
        {
            var original = GetLotById(lotId);
            if (original == null) return 0;

            var copy = new Lot
            {
                UserId = userId,
                Title = original.Title + " (копия)",
                Description = original.Description,
                Price = original.Price,
                CategoryId = original.CategoryId,
                ImagePath = original.ImagePath
            };

            return CreateLot(copy);
        }
        public static List<string> GetSearchSuggestions(string query, int maxResults = 5)
        {
            using var db = new SqlConnection(connectionString);
            var sql = @"SELECT DISTINCT TOP (@Max) Title FROM Ads 
                        WHERE Title LIKE @Query
                        UNION
                        SELECT DISTINCT TOP (@Max) Name FROM Categories 
                        WHERE Name LIKE @Query";
            return db.Query<string>(sql, new { Max = maxResults, Query = $"%{query}%" }).ToList();
        }
    }

    public class LotSearchFilter
    {
        public string? SearchText { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public SortOption SortBy { get; set; } = SortOption.Newest;
    }

    public enum SortOption
    {
        Newest,
        PriceAsc,
        PriceDesc
    }
}
