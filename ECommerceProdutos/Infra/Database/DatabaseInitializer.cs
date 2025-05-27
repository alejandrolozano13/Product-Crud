using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infra.Database
{
    public class DatabaseInitializer
    {
        private readonly IConfiguration _configuration;

        public DatabaseInitializer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InitializeAsync()
        {
            var postgresConnectionString = _configuration.GetConnectionString("PostgresConnection");
            var defaultConnectionString = _configuration.GetConnectionString("DefaultConnection");

            await CreateDatabaseIfNotExists(postgresConnectionString);
            await CreateTablesAndSeedData(defaultConnectionString);
        }

        private async Task CreateDatabaseIfNotExists(string connectionString)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            var checkCmd = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = 'ecommerce_produtos';", connection);
            var exists = await checkCmd.ExecuteScalarAsync();

            if (exists == null)
            {
                var createDbCmd = new NpgsqlCommand("CREATE DATABASE ecommerce_produtos;", connection);
                await createDbCmd.ExecuteNonQueryAsync();
                Console.WriteLine("Banco de dados 'ecommerce_produtos' criado.");
            }
            else
            {
                Console.WriteLine("Banco de dados 'ecommerce_produtos' já existe.");
            }
        }

        private async Task CreateTablesAndSeedData(string connectionString)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();

            var createExtension = "CREATE EXTENSION IF NOT EXISTS \"pgcrypto\";";
            var cmdCreateExtension = new NpgsqlCommand(createExtension, connection);
            await cmdCreateExtension.ExecuteNonQueryAsync();

            var createDepartmentTable = @"
                CREATE TABLE IF NOT EXISTS Department (
                    Code VARCHAR(3) PRIMARY KEY,
                    Description VARCHAR(100) NOT NULL
                );";
            var cmdCreateDepartment = new NpgsqlCommand(createDepartmentTable, connection);
            await cmdCreateDepartment.ExecuteNonQueryAsync();

            var createProductTable = @"
                CREATE TABLE IF NOT EXISTS Products (
                    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                    Code VARCHAR(50) NOT NULL,
                    Description VARCHAR(200) NOT NULL,
                    DepartmentCode VARCHAR(3) NOT NULL REFERENCES Department(Code),
                    Price NUMERIC(12,2) NOT NULL,
                    Active BOOLEAN NOT NULL DEFAULT TRUE,
                    Removed BOOLEAN NOT NULL DEFAULT FALSE
                );";
            var cmdCreateProduct = new NpgsqlCommand(createProductTable, connection);
            await cmdCreateProduct.ExecuteNonQueryAsync();

            var createUsersTable = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                    Name VARCHAR(150) NOT NULL,
                    Email VARCHAR(150) UNIQUE NOT NULL,
                    Password VARCHAR(255) NOT NULL,
                    CreatedAt TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW()
                );
            ";

            var cmdCreateUsers = new NpgsqlCommand(createUsersTable, connection);
            await cmdCreateUsers.ExecuteNonQueryAsync();

            var insertDepartments = @"
                INSERT INTO Department (Code, Description)
                SELECT '010', 'BEBIDAS' WHERE NOT EXISTS (SELECT 1 FROM Department WHERE Code = '010');
                INSERT INTO Department (Code, Description)
                SELECT '020', 'CONGELADOS' WHERE NOT EXISTS (SELECT 1 FROM Department WHERE Code = '020');
                INSERT INTO Department (Code, Description)
                SELECT '030', 'LATICINIOS' WHERE NOT EXISTS (SELECT 1 FROM Department WHERE Code = '030');
                INSERT INTO Department (Code, Description)
                SELECT '040', 'VEGETAIS' WHERE NOT EXISTS (SELECT 1 FROM Department WHERE Code = '040');";
            var cmdInsertDepartments = new NpgsqlCommand(insertDepartments, connection);
            await cmdInsertDepartments.ExecuteNonQueryAsync();

            var checkAdminExists = "SELECT 1 FROM Users WHERE Email = 'admin@ecommerce.com';";
            var cmdCheckAdmin = new NpgsqlCommand(checkAdminExists, connection);
            var adminExists = await cmdCheckAdmin.ExecuteScalarAsync();

            if (adminExists == null)
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword("Admin@123");

                var insertAdmin = @"
                    INSERT INTO Users (Email, Password, Name)
                    VALUES (@Email, @Password, @Name);";

                var cmdInsertAdmin = new NpgsqlCommand(insertAdmin, connection);
                cmdInsertAdmin.Parameters.AddWithValue("Email", "admin@ecommerce.com");
                cmdInsertAdmin.Parameters.AddWithValue("Name", "admin");
                cmdInsertAdmin.Parameters.AddWithValue("Password", hashedPassword);
                await cmdInsertAdmin.ExecuteNonQueryAsync();

                Console.WriteLine("Usuário admin criado com email 'admin@ecommerce.com' e senha 'Admin@123'");
            }
            else
            {
                Console.WriteLine("Usuário admin já existe.");
            }
        }
    }
}