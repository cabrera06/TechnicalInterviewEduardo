namespace TechnicalInterview.Core.Application.Dtos.Request
{
    public class DepositRequestDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = default!;
    }
}
