using FluentValidation;
using TechnicalInterview.WebAPI.Dtos.Request;

namespace TechnicalInterview.WebAPI.Validators
{
    public class TransferRequestValidator : AbstractValidator<TransferRequest>
    {
        public TransferRequestValidator()
        {
            RuleFor(x => x.FromAccountId)
            .NotEmpty().WithMessage("El parametro fromAccountId es obligatorio")
            .Must(id => !string.IsNullOrWhiteSpace(id) && !id.Contains(" ")).WithMessage("El parámetro fromAccountId no debe contener espacios");

            RuleFor(x => x.ToAccountId)
            .NotEmpty().WithMessage("El parametro toAccountId es obligatorio")
            .Must(id => !string.IsNullOrWhiteSpace(id) && !id.Contains(" ")).WithMessage("El parámetro toAccountId no debe contener espacios");

            RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("El parametro amount es obligatorio");
        }
    }
}
