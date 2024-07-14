using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Application.Contracts.Repositories
{
    public interface ICoworkingRepository : IRepository<Coworking>
    {
        Task<Coworking?> GetByIdWithSalas(int id);
    }
}
