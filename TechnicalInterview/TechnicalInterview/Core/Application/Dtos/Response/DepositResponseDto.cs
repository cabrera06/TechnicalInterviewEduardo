using System.Text.Json.Serialization;

namespace TechnicalInterview.Core.Application.Dtos.Response
{
    public class DepositResponseDto
    {
        [JsonIgnore]
        public int ErrorCode { get; set; }
        [JsonIgnore]
        public string? ErrorMessage { get; set; }
        public decimal newBalance { get; set; }
        public int reference { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
