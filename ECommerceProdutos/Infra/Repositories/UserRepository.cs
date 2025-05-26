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
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var query = @"
                INSERT INTO users (id, email, password, role)
                VALUES (@Id, @Email, @Password, @Role);
            ";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(query, user);
        }

        public async Task Update(string id, User user)
        {
            var query = @"
                UPDATE users
                SET email = @Email,
                    password = @Password,
                    role = @Role
                WHERE id = @Id;
            ";

            using var connection = CreateConnection();
            await connection.ExecuteAsync(query, new
            {
                Id = id,
                Email = user.Email,
                Password = user.Password, // Já deve estar hashado antes de chamar
            });
        }

        public async Task Delete(string id)
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

        public async Task<User> GetById(string id)
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