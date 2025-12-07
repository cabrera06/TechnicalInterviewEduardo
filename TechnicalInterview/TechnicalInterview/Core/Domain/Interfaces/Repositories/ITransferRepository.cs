using TechnicalInterview.Core.Application.Dtos;

namespace TechnicalInterview.Core.Domain.Interfaces.Repositories
{
    public interface ITransferRepository
    {
        Task<TransferDto> ExecuteTransfer(string fromAccountId, string toAccountId, decimal amount, string? description, CancellationToken ct);
    }
}
