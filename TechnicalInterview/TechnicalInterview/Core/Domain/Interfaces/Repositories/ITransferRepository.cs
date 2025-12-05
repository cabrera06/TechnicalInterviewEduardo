namespace TechnicalInterview.Core.Domain.Interfaces.Repositories
{
    public interface ITransferRepository
    {
        Task<bool> CreateTransferAsync(string sourceAccountId, string targetAccountId, decimal amount, CancellationToken ct);
    }
}
