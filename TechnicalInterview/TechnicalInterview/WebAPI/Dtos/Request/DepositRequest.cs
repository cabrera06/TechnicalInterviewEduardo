using System.ComponentModel.DataAnnotations;

namespace TechnicalInterview.WebAPI.Dtos.Request
{
    public class DepositRequest
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = default!;
    }
}
