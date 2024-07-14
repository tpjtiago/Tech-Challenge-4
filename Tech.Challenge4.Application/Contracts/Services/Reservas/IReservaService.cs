using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Models.Reserva;

namespace Tech.Challenge4.Domain.Contracts.Services.Reservas
{
    public interface IReservaService
    {
        Task<Reserva> Post(Reserva reserva);

        Task<Reserva> GetById(int reservaId);

        Task<IList<Reserva>> GetAll();

        Task<Reserva> EfetuarReserva(ReservaModel reservaModel);
        bool ValidarHoraInicioLocal(ReservaModel reservaModel, Coworking coworking);
        bool ValidarHoraFinalLocal(ReservaModel reservaModel, Coworking coworking);
        Task<int> ReservasDisponiveisSala(ReservaModel reservaModel, Sala sala);
        decimal CalcularValorReserva(Reserva reserva, Sala sala);
        Task<bool> CancelReservation(int reservationId);
        Task<bool> PresentReservation(int reservationId);
    }
}
