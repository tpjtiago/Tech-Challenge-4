using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Application.Contracts.Repositories
{
    public interface ISalaRepository : IRepository<Sala>
    {
        Task<Sala?> GetByIdWithCoworking(int salaId);
        Task<List<Sala>> GetAllWithCoworking();
    }
}
