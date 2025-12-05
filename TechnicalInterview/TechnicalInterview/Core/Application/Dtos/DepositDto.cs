namespace TechnicalInterview.Core.Application.Dtos
{
    public class DepositDto
    {
        public string AccountId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
