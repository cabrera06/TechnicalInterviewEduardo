using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;
using TechnicalInterview.Core.Application.Dtos;
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

        public async Task<Account?> GetAccountInfo(string accountId, CancellationToken ct)
        {

            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync(ct);

                using var command = new SqlCommand("sp_GetAccountInfo", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AccountId", accountId);
                using var reader = await command.ExecuteReaderAsync(ct);

                if (!await reader.ReadAsync(ct))
                {
                    return null;
                }

                Account? account = null;
                // Leer la información de la cuenta
                account = new Account
                {
                    AccountId = reader.GetString(0),
                    CustomerName = reader.GetString(1),
                    Balance = reader.GetDecimal(2),
                    CreatedDate = reader.GetDateTime(3),
                };

                // Leer las transacciones asociadas
                await reader.NextResultAsync(ct);

                while (await reader.ReadAsync(ct))
                {
                    account.Transactions.Add(new Core.Domain.Models.Transaction
                    {
                        TransactionId = reader.GetInt32(0),
                        AccountId = reader.GetString(1),
                        Type = reader.GetString(2),
                        Amount = reader.GetDecimal(3),
                        Date = reader.GetDateTime(4),
                        Description = reader.IsDBNull(5) ? null : reader.GetString(5),
                        Reference = reader.GetInt32(6)

                    });
                }

                return account;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el llamado a base de datos", ex);
            }
        }

        public async Task<DepositDto> CreateDeposit(string accountId, decimal amount, string description, CancellationToken ct)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync(ct);



                using var command = new SqlCommand("sp_CreateDeposit", connection);
                command.CommandType = CommandType.StoredProcedure;
                // Agregar parámetros
                command.Parameters.AddWithValue("@AccountId", accountId);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@Description", description);


                var reader = await command.ExecuteReaderAsync(ct);

                if (!await reader.ReadAsync(ct))
                {
                    throw new InvalidOperationException("El procedimiento almacenado no retornó informacion sobre el deposito.");
                }

                DepositDto? result = null;
                // Leer los resultados
                result = new DepositDto
                {
                    Success = reader.GetInt32(0) == 1,
                    Message = reader.GetString(1),
                    NewBalance = reader.IsDBNull(2) ? (decimal?)null : reader.GetDecimal(2),
                    Reference = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3)

                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el llamado a base de datos", ex);

            }
        }

        public async Task<WithdrawalDto> CreateWithdrawal(string accountId, decimal amount, string description, CancellationToken ct)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync(ct);
                using var command = new SqlCommand("sp_CreateWithdrawal", connection);
                command.CommandType = CommandType.StoredProcedure;
                // Agregar parámetros
                command.Parameters.AddWithValue("@AccountId", accountId);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@Description", description);
                var reader = await command.ExecuteReaderAsync(ct);

                if (!await reader.ReadAsync(ct))
                {
                    throw new InvalidOperationException("El procedimiento almacenado no retornó informacion sobre el retiro.");
                }

                WithdrawalDto? result = null;
                // Leer los resultados
                result = new WithdrawalDto
                {
                    Success = reader.GetInt32(0) == 1,
                    Message = reader.GetString(1),
                    NewBalance = reader.IsDBNull(2)? (decimal?)null: reader.GetDecimal(2),
                    Reference = reader.IsDBNull(3)? (int?)null: reader.GetInt32(3)

                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el llamado a base de datos", ex);

            }
        }
    }
}
