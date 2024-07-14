using Microsoft.EntityFrameworkCore;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Data.Contexts;
using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Data.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected CoworkingContext _context;
        protected DbSet<T> _dbSet;

        public EFRepository(CoworkingContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> Post(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
        public async Task<T> GetById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IList<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> Put(T entity)
        {
            var result = _dbSet.Update(entity);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<T> Delete(T entity)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
