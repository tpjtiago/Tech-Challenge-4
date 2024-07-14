using FluentValidation;
using Tech.Challenge4.Domain.Models.Reserva;

namespace Tech.Challenge4.Domain.Validators
{
    public class ReservaValidator : AbstractValidator<ReservaModel>
    {
        public ReservaValidator()
        {
            RuleFor(r => r.DataReserva)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                    .WithMessage("A Data da Reserva não pode ser menor que a data atual");

            RuleFor(r => r.HoraInicio)
                .Must((r, t) =>
                {
                    if (r.DataReserva == DateOnly.FromDateTime(DateTime.Now)
                        && r.HoraInicio < TimeOnly.FromDateTime(DateTime.Now))
                    {
                        return false;
                    }
                    return true;
                })
                .WithMessage("A Hora de Início da Reserva não pode ser anterior a agora")
                .LessThan(r => r.HoraFinal).WithMessage("A Hora de Início não pode ser depois da Hora Final");

            RuleFor(r => r.HoraFinal)
                .Must((r, t) =>
                {
                    if (r.DataReserva == DateOnly.FromDateTime(DateTime.Now)
                        && r.HoraFinal < TimeOnly.FromDateTime(DateTime.Now))
                    {
                        return false;
                    }
                    return true;
                })
                .WithMessage("A Hora Final da Reserva não pode ser anterior a agora");
        }
    }
}
