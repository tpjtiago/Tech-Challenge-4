using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Application.Contracts.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetByEmail(string email);
    }
}
