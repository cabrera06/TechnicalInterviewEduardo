namespace TechnicalInterview.WebAPI.Dtos.Request
{
    public class WithdrawalRequest
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = default!;
    }
}
