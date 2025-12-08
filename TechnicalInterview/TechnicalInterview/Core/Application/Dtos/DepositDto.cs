namespace TechnicalInterview.Core.Application.Dtos
{
    public class DepositDto
    {
        
        public bool Success { get; set; }
        public string Message { get; set; } = default!;
        public decimal? NewBalance { get; set; }
        public int? Reference { get; set; }
    }

}
