using Microsoft.EntityFrameworkCore;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Data.Contexts;
using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Data.Repositories
{
    public class CoworkingRepository : EFRepository<Coworking>, ICoworkingRepository
    {
        public CoworkingRepository(CoworkingContext context) : base(context)
        {
        }

        public async Task<Coworking?> GetByIdWithSalas(int id)
        {
            var coworking = await _dbSet.AsNoTracking()
                .Include(c => c.Salas)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (coworking is not null)
            {
                coworking.Salas = coworking.Salas?.Select(s =>
                {
                    s.Coworking = null;
                    return s;
                }).ToList();
            }

            return coworking;
        }
    }
}
