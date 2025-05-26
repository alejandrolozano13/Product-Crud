using Dapper;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        public async Task<List<User>> GetAll()
        {
            var query = "SELECT * FROM users";

            using var connection = CreateConnection();
            var users = await connection.QueryAsync<User>(query);
            return users.AsList();
        }

        public async Task Add(User user)
        {
            var query = @"
                INSERT INTO users (id, email, name, password)
                VALUES (@Id, @Email, @Name, @Password);
            ";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(query, user);
        }

        public async Task Update(Guid id, User user)
        {
            var query = @"
                UPDATE users
                SET email = @Email,
                    password = @Password,
                    name = @Name
                WHERE id = @Id;
            ";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(query, new
            {
                Id = id,
                Email = user.Email,
                Password = user.Password,
                Name = user.Name
            });
        }

        public async Task Delete(Guid id)
        {
            var query = "DELETE FROM users WHERE id = @Id";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<User> GetByEmail(string email)
        {
            var query = "SELECT * FROM users WHERE email = @Email";

            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }

        public async Task<User> GetById(Guid id)
        {
            var query = "SELECT * FROM users WHERE id = @Id";

            using var connection = CreateConnection();
            var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });

            if (user == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            return user;
        }

        public async Task<bool> IsEmailTaken(string email)
        {
            var query = "SELECT EXISTS(SELECT 1 FROM users WHERE email = @Email)";

            using var connection = CreateConnection();
            return await connection.ExecuteScalarAsync<bool>(query, new { Email = email });
        }
    }
}