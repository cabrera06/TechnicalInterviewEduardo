using System.Text.Json.Serialization;

namespace TechnicalInterview.WebAPI.Dtos.Response
{
    public class DepositResponse
    {
        public decimal NewBalance { get; set; }
        public int Reference { get; set; }
    }
}
