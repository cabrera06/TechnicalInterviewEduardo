using Microsoft.Data.SqlClient;
using System.Data;
using TechnicalInterview.Core.Application.Dtos;
using TechnicalInterview.Core.Domain.Interfaces.Repositories;

namespace TechnicalInterview.Infrastructure.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private readonly string _connectionString;

        public TransferRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<TransferDto> ExecuteTransfer(string fromAccountId, string toAccountId, decimal amount, string? description, CancellationToken ct)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync(ct);
                using var command = new SqlCommand("sp_ExecuteTransfer", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FromAccountId", fromAccountId);
                command.Parameters.AddWithValue("@ToAccountId", toAccountId);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@Description", (object?)description ?? DBNull.Value);

                var reader = await command.ExecuteReaderAsync(ct);

                if (!await reader.ReadAsync(ct))
                {
                    throw new InvalidOperationException("El procedimiento almacenado informacion sobre la tranferencia");
                }

                TransferDto? result = null;
                // Leer los resultados
                result = new TransferDto
                {
                    Success = reader.GetInt32(0) == 1,
                    Message = reader.GetString(1),
                    Reference = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2)

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
