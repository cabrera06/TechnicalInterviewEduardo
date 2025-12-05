using TechnicalInterview.Core.Domain.Models;

namespace TechnicalInterview.Core.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(string accountId, CancellationToken ct);
        Task UpdateAsync(Account account, CancellationToken ct);
    }
}
