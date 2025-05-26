using System.Data;
using Dapper;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Product>> GetAllAsycn()
        {
            var query = "SELECT * FROM products WHERE isdeleted = false";

            using var connection = CreateConnection();
            var products = await connection.QueryAsync<Product>(query);
            return products;
        }

        public async Task<Product> GetByIdAsycn(Guid id)
        {
            var query = "SELECT * FROM products WHERE id = @Id AND isdeleted = false";

            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
        }

        public async Task InsertAsync(Product product)
        {
            var query = @"
                INSERT INTO products (id, code, description, departmentCode, price, status, isdeleted)
                VALUES (@Id, @Code, @Description, @DepartmentCode, @Price, @Active, false);
            ";


            using var connection = CreateConnection();
            await connection.ExecuteAsync(query, product);
        }

        public async Task RemoveAsync(Guid id)
        {
            var query = "UPDATE products SET isdeleted = true WHERE id = @Id";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task UpdateAsync(Product product)
        {
            var query = @"
                UPDATE products
                SET code = @Code,
                    description = @Description,
                    departmentCode = @DepartmentCode,
                    price = @Price,
                    status = @Active
                WHERE id = @Id;
            ";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(query, product);
        }
    }
}