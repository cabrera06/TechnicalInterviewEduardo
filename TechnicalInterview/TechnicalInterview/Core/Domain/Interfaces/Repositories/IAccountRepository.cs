using TechnicalInterview.Core.Domain.Models;
using TechnicalInterview.Infrastructure.SpResults;

namespace TechnicalInterview.Core.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountInfo(string accountId, CancellationToken ct);
        Task<DepositSpResult> CreateDeposit(string accountId,decimal amount, string description, CancellationToken ct);

       // Task<Account> CreateWithdrawal(string accountId, decimal amount, CancellationToken ct);
    }
}
