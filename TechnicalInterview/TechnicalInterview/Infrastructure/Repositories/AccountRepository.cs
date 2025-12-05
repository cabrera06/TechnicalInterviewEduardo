using Microsoft.Data.SqlClient;
using TechnicalInterview.Core.Domain.Interfaces.Repositories;
using TechnicalInterview.Core.Domain.Models;

namespace TechnicalInterview.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;
        public AccountRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<Account?> GetByIdAsync(string accountId, CancellationToken ct)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(ct);
            
            var sql = "SELECT AccountId, CustomerName, Balance FROM Accounts WHERE AccountId = @AccountId";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@AccountId", accountId);
            using var reader = await command.ExecuteReaderAsync(ct);
            if (!await reader.ReadAsync(ct))
            {
                return null;
            }

            return new Account
            {
                AccountId = reader.GetString(0),
                CustomerName = reader.GetString(1),
                Balance = reader.GetDecimal(2)
            };
        }

        public async Task UpdateAsync(Account account, CancellationToken ct)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(ct);
            
            var sql = "UPDATE Accounts SET CustomerName = @CustomerName, Balance = @Balance WHERE AccountId = @AccountId";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@AccountId", account.AccountId);
            command.Parameters.AddWithValue("@CustomerName", account.CustomerName);
            command.Parameters.AddWithValue("@Balance", account.Balance);
            await command.ExecuteNonQueryAsync(ct);
        }

    }
}
