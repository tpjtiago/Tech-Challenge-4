using Microsoft.EntityFrameworkCore;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Data.Contexts;
using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Data.Repositories
{
    public class SalaRepository : EFRepository<Sala>, ISalaRepository
    {
        public SalaRepository(CoworkingContext context) : base(context)
        {
        }

        public async Task<List<Sala>> GetAllWithCoworking()
        {
            var salas = await _dbSet.AsNoTracking()
                .Include(s => s.Coworking)
                .ToListAsync();

            salas = salas.Select(s =>
            {
                var sala = s;
                sala.Coworking.Salas = null;
                return sala;
            }).ToList();

            return salas;

        }

        public async Task<Sala?> GetByIdWithCoworking(int salaId)
        {
            var sala = await _dbSet.AsNoTracking()
                .Include(s => s.Coworking)
                .FirstOrDefaultAsync(s => s.Id == salaId);

            if (sala is not null)
                sala.Coworking.Salas = null;

            return sala;
        }
    }
}
