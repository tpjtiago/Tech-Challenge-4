using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Application.Contracts.Repositories
{
    public interface IReservaRepository : IRepository<Reserva>
    {
        Task<Reserva> GetByIdWithCustomerSala(int reservaId);
        Task<int> ReservasSalaFaixaHorario(int salaId, TimeOnly HoraInicio, TimeOnly HoraFim, DateOnly DataReserva);
    }
}
