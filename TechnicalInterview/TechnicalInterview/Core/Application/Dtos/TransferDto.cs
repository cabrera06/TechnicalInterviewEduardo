namespace TechnicalInterview.Core.Application.Dtos
{
    public class TransferDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = default!;
        public int? Reference { get; set; }
    }
}
