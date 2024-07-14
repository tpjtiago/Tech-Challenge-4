using Microsoft.EntityFrameworkCore;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Data.Contexts;
using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Data.Repositories
{
    public class CustomerRepository : EFRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CoworkingContext context) : base(context)
        {
        }

        public async Task<Customer?> GetByEmail(string email)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
