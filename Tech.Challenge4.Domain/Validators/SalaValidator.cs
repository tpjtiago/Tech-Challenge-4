using FluentValidation;
using Tech.Challenge4.Domain.Models.Sala;

namespace Tech.Challenge4.Domain.Validators
{
    public class SalaValidator : AbstractValidator<SalaModel>
    {
        public SalaValidator()
        {
            //public required int Capacidade { get; set; }
            //public required decimal PrecoHora { get; set; }

            //public required int CoworkingId { get; set; }
            RuleFor(s => s.Nome)
                .NotEmpty().WithMessage("O Nome é obrigatório.")
                .Length(5, 50).WithMessage("O Nome deve conter mais de 5 caracteres");

            RuleFor(s => s.Capacidade)
                .GreaterThan(0).WithMessage("A capacidade deve ser maior que 0");

            RuleFor(s => s.PrecoHora)
                .GreaterThanOrEqualTo(0).WithMessage("O preço não pode ser negativo");

            RuleFor(s => s.CoworkingId)
                .NotEmpty().WithMessage("É preciso associar a sala a um espaço de coworking");
        }
    }
}
