using Microsoft.EntityFrameworkCore;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Data.Contexts;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Enumerables;

namespace Tech.Challenge4.Data.Repositories
{
    public class ReservaRepository : EFRepository<Reserva>, IReservaRepository
    {
        public ReservaRepository(CoworkingContext context) : base(context)
        {
        }

        public async Task<Reserva> GetByIdWithCustomerSala(int reservaId)
        {
            var reserva = await _dbSet.AsNoTracking()
                .Include(r => r.Customer)
                .Include(r => r.Sala)
                .FirstOrDefaultAsync(r => r.Id == reservaId);

            if (reserva is not null)
            {
                reserva.Customer.Reservas = null;
                reserva.Sala.Reservas = null;
            }

            return reserva;
        }

        public async Task<int> ReservasSalaFaixaHorario(
            int salaId, 
            TimeOnly HoraInicio, 
            TimeOnly HoraFim, 
            DateOnly DataReserva
        )
        {
            var reservas = await _dbSet.AsNoTracking()
                .Where(r => r.SalaID == salaId
                    && r.StatusReserva != StatusReserva.Cancelada
                    && r.HoraFinal > HoraInicio
                    && r.HoraInicio < HoraFim
                    && r.DataReserva == DataReserva)
                .ToListAsync();

            return reservas.Count;
        }
    }
}
