using System.Data;
using Dapper;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infra.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var query = "SELECT * FROM departments";

            using var connection = CreateConnection();
            return await connection.QueryAsync<Department>(query);
        }

        public async Task<Department> GetByIdAsync(string code)
        {
            var query = "SELECT * FROM departments WHERE code = @Code";

            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Department>(query, new { Code = code });
        }

        public async Task InsertAsync(Department department)
        {
            var query = @"
                INSERT INTO departments (code, description)
                VALUES (@Code, @Description);
            ";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(query, department);
        }

        public async Task RemoveAsync(string code)
        {
            var query = "DELETE FROM departments WHERE code = @Code";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(query, new { Code = code });
        }

        public async Task UpdateAsync(Department department)
        {
            var query = @"
                UPDATE departments
                SET description = @Description
                WHERE code = @Code;
            ";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(query, department);
        }
    }
}