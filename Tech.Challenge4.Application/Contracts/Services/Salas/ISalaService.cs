using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Models.Sala;

namespace Tech.Challenge4.Domain.Contracts.Services.Salas
{
    public interface ISalaService
    {
        Task<Sala> Post(SalaModel salaModel);

        Task<Sala> GetById(int salaId);

        Task<IList<Sala>> GetAll();

        Task<Sala> Put(int salaId, SalaModel salaModel);

        Task DeleteById(int salaId);
    }
}
