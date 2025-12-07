using FluentValidation;
using System.Data;
using TechnicalInterview.WebAPI.Dtos.Request;

namespace TechnicalInterview.WebAPI.Validators
{
    public class AccountRequestValidator : AbstractValidator<AccountRequest>
    {
        public AccountRequestValidator()
        {
            RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("El accountId es obligatorio")
            .Must(id => id != "{accountId}" && id != ":accountId").WithMessage("Debe enviar accountId válido.")
            .Must(id => !string.IsNullOrWhiteSpace(id) && !id.Contains(" ")).WithMessage("El parámetro accountId no debe contener espacios");
        }
    }
}
