using Bogus;
using Bogus.Extensions.Brazil;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Models.Customers;

namespace Tech.Challenge4.Data.Tests.RepositoriesTests.CustomerTests.Fixture
{
    public class CustomerFixture
    {
        public static Faker _faker { get; set; } = new Faker();

        public static Customer CreateCustomer()
        {
            return new Customer(
                name: _faker.Person.FullName,
                email: _faker.Internet.Email(),
                cpf: _faker.Person.Cpf(),
                phone: _faker.Phone.PhoneNumber()
            );
        }

        public static CustomerModel CreateCustomerModel()
        {
            return new CustomerModel
            {
                Name = _faker.Name.FullName(),
                Email = _faker.Internet.Email(),
                Phone = _faker.Phone.PhoneNumber(),
                Cpf = _faker.Person.Cpf()
            };
        }
    }
}
