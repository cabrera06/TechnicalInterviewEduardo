namespace TechnicalInterview.Core.Domain.Models
{
    public class Transfer
    {
        public string FromAccountId { get; set; } = default!;
        public string ToAccountId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string? Description{ get; set; }
        public int ErrorCode { get; set;}
        public string? ErrorMessage { get; set; }
    }
}
