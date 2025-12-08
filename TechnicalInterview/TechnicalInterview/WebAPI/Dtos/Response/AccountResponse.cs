namespace TechnicalInterview.WebAPI.Dtos.Response
{
    public class AccountResponse
    {
        public string AccountId { get; set; } = default!;
        public decimal Balance { get; set; }

        public decimal Interest { get; set; }

        public List<TransactionsList> Transactions { get; set; } = [];
    }

    public class TransactionsList
    {
        public int TransacTionId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = default!;
        public int Reference { get; set; }
        public string? Description { get; set; }
    }
}

