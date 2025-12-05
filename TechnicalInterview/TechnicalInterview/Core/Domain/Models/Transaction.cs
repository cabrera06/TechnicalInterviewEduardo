namespace TechnicalInterview.Core.Domain.Models
{
    public class Transaction
    {
        public string AccountId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = default!;
    }
}
