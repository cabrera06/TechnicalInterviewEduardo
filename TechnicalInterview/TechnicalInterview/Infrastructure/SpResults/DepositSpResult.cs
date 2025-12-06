namespace TechnicalInterview.Infrastructure.SpResults
{
    public class DepositSpResult
    {
        
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public decimal NewBalance { get; set; }
        public int Reference { get; set; }
        public DateTime TransactionDate { get; set; }
    }

}
