using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Repositories
{
    public class ProductSqlRepository
    {
        private readonly string _connectionString;

        public ProductSqlRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var result = new List<Product>();

            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            const string sql = "SELECT Id, Name, Description, Price, StockQuantity FROM Products";
            await using var command = new SqlCommand(sql, connection);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new Product
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    StockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity"))
                });
            }

            return result;
        }
    }
}
