using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Application.Contracts.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> Post(T entity);

        Task<T> GetById(int id);

        Task<IList<T>> GetAll();

        Task<T> Put(T entity);

        Task<T> Delete(T entity);
    }
}
