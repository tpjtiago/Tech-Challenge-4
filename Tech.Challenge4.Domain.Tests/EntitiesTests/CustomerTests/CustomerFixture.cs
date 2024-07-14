using Bogus;
using Bogus.Extensions.Brazil;
using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Domain.Tests.EntitiesTests.CustomerTests
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
    }
}
