using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Models.Coworking;

namespace Tech.Challenge4.Domain.Contracts.Services.Coworkings
{
    public interface ICoworkingService
    {
        Task<Coworking> Post(CoworkingModel coworkingModel);

        Task<Coworking> GetById(int coworkingId);

        Task<IList<Coworking>> GetAll();

        Task<Coworking> Put(int coworkingId, CoworkingModel coworkingModel);

        Task DeleteById(int coworkingId);
    }
}
