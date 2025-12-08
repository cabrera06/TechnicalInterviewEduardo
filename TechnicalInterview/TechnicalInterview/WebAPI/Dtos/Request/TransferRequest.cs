namespace TechnicalInterview.WebAPI.Dtos.Request
{
    public class TransferRequest
    {
        public string FromAccountId { get; set; } = default!;
        public string ToAccountId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
