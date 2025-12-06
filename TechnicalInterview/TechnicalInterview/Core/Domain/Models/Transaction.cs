namespace TechnicalInterview.Core.Domain.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string AccountId { get; set; } = default!;
        public string Type { get; set; } = default!;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public int Reference { get; set; }
    }
}
