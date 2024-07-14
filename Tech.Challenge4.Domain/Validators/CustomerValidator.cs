using FluentValidation;
using Tech.Challenge4.Domain.Models.Customers;

namespace Tech.Challenge4.Domain.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O Nome é obrigatório.")
                .Length(5, 50).WithMessage("O Nome deve conter mais de 5 caracteres");

            RuleFor(p => p.Email)
                .EmailAddress().WithMessage("Digite um email válido");

            RuleFor(p => p.Cpf)
                .Matches("^\\d{11}$").WithMessage("O CPF deve ter 11 caracteres, apenas números");
        }
    }
}
