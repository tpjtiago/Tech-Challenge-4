using FluentValidation;
using Tech.Challenge4.Domain.Models.Coworking;

namespace Tech.Challenge4.Domain.Validators
{
    public class CoworkingValidator : AbstractValidator<CoworkingModel>
    {
        public CoworkingValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O Nome é obrigatório.")
                .Length(5, 50).WithMessage("O Nome deve conter mais de 5 caracteres");

            RuleFor(c => c.HoraAbertura)
                .NotEmpty().WithMessage("O horário de abertura é obrigatório")
                .LessThan(c => c.HoraFechamento).WithMessage("O horário de abertura deve ser anterior a hora de fechamento");

            RuleFor(c => c.HoraFechamento)
                .NotEmpty().WithMessage("O horário de fechamento é obrigatório");
        }
    }
}
