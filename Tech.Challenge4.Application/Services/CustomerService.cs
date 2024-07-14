using AutoMapper;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Domain.Contracts.Services.Customers;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Exceptions;
using Tech.Challenge4.Domain.Models.Customers;

namespace Tech.Challenge4.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Customer> Post(CustomerModel customerModel)
        {
            var customerEmail = await GetByEmail(customerModel.Email);
            if (customerEmail != null)
                throw new ConflictException("Email já utilizado");

            var customerMap = _mapper.Map<Customer>(customerModel);

            var result = await _customerRepository.Post(customerMap);

            return result;
        }

        public async Task<CustomerModel> GetById(int customerId)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer is null)
            {
                throw new NotFoundException($"Cliente não encontrado com id {customerId}");
            }

            var customerMap = _mapper.Map<CustomerModel>(customer);

            return customerMap;
        }

        public async Task<IList<CustomerModel>> GetAll()
        {
            var customers = await _customerRepository.GetAll();

            var customersMap = _mapper.Map<IList<CustomerModel>>(customers);

            return customersMap;

        }

        public async Task<Customer> Put(int customerId, CustomerModel customerModel)
        {
            var customerEmail = await GetByEmail(customerModel.Email);
            if (customerEmail != null && customerEmail.Id != customerId)
                throw new ConflictException("Email já utilizado");

            var customer = await _customerRepository.GetById(customerId);

            if (customer is null)
            {
                throw new NotFoundException($"Cliente não encontrado com id {customerId}");
            }

            var customerMap = _mapper.Map(customerModel, customer);

            var result = await _customerRepository.Put(customerMap);

            return result;
        }

        public async Task DeleteById(int customerId)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer is null)
            {
                throw new NotFoundException($"Cliente não encontrado com id {customerId}");
            }

            await _customerRepository.Delete(customer);
        }

        public async Task<Customer> GetByEmail(string email)
        {
            var customer = await _customerRepository.GetByEmail(email);

            return customer;
        }
    }
}
