using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;
using TechnicalInterview.Core.Application.Dtos;
using TechnicalInterview.Core.Domain.Interfaces.Repositories;
using TechnicalInterview.Core.Domain.Models;
using TechnicalInterview.Infrastructure.SpResults;

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
                account.TransactionsList.Add(new Core.Domain.Models.Transaction
                {
                    //TransactionId = Convert.ToInt32(reader["TransaccionID"]),
                    //Descripcion = reader["Descripcion"].ToString(),
                    //Monto = Convert.ToDecimal(reader["Monto"]),
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


        public async Task<DepositSpResult> CreateDeposit(string accountId, decimal amount, string description, CancellationToken ct)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(ct);

      

            using var command = new SqlCommand("sp_CreateDeposit", connection);
            command.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros
            command.Parameters.AddWithValue("@AccountId", accountId);
            command.Parameters.AddWithValue("@Amount", amount);
            command.Parameters.AddWithValue("@Description", description);
            // Parámetros de salida
            var pNewBalance = command.Parameters.Add("@NewBalance", SqlDbType.Decimal);
            pNewBalance.Direction = ParameterDirection.Output;
            var pCodigoError = command.Parameters.Add("@ErrorCode", SqlDbType.Int);
            pCodigoError.Direction = ParameterDirection.Output;
            var prmMensajeError = command.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 4000);
            prmMensajeError.Direction = ParameterDirection.Output;

            await command.ExecuteNonQueryAsync(ct);

            DepositSpResult? result = null;
            // Obtener valores de los parámetros de salida
            result = new DepositSpResult
            {
                ErrorCode = (int)pCodigoError.Value,
                ErrorMessage = prmMensajeError.Value?.ToString(),
                NewBalance = (decimal)pNewBalance.Value

            };

            return result;

        }
    }
}
