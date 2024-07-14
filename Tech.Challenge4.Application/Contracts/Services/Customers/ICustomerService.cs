using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Models.Customers;

namespace Tech.Challenge4.Domain.Contracts.Services.Customers
{
    public interface ICustomerService
    {
        Task<Customer> Post(CustomerModel customerModel);

        Task<CustomerModel> GetById(int customerId);

        Task<IList<CustomerModel>> GetAll();

        Task<Customer> Put(int customerId, CustomerModel customerModel);

        Task DeleteById(int customerId);

        Task<Customer> GetByEmail(string email);
    }
}
