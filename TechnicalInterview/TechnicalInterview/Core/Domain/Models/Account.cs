namespace TechnicalInterview.Core.Domain.Models
{
    public class Account
    {
        public string AccountId { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public decimal Balance { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;


        public List<Transaction> TransactionsList { get; set; } = [];
    }
}
