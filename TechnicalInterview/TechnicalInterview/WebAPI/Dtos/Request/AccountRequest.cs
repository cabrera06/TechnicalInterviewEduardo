namespace TechnicalInterview.WebAPI.Dtos.Request
{
    public class AccountRequest
    {
        //Se permite nullable ya que se valida con fluent
        public string? AccountId { get; set; }
    }
}
