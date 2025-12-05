namespace TechnicalInterview.Core.Application.Dtos
{
    public class AccountDto
    {
        public string AccountId { get; set; } = default!;
        public decimal Balance { get; set; }

        public decimal Interest { get; set; }

        public List<Transactions> Transactions { get; set; } = [];
    }

    public class  Transactions 
    {
        public int TransacTionId { get; set; }
        public string AccountId { get; set; } = default!;

        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = default!;
        public int Reference { get; set; }
    }
}
