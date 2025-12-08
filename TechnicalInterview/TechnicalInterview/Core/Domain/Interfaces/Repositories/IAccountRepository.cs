using TechnicalInterview.Core.Application.Dtos;
using TechnicalInterview.Core.Domain.Models;

namespace TechnicalInterview.Core.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountInfo(string accountId, CancellationToken ct);
        Task<DepositDto> CreateDeposit(string accountId,decimal amount, string description, CancellationToken ct);

        Task<WithdrawalDto> CreateWithdrawal(string accountId, decimal amount, string description, CancellationToken ct);
    }
}
