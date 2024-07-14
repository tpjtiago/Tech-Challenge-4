using Microsoft.EntityFrameworkCore;
using Tech.Challenge4.Data.Contexts;
using Tech.Challenge4.Data.Repositories;
using Tech.Challenge4.Data.Tests.RepositoriesTests.CustomerTests.Fixture;

namespace TechChallenge4.Data.Tests.RepositoriesTests.CustomerTests
{
    public class CustomerRepositoryTests
    {
        private CustomerRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CoworkingContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _repository = new CustomerRepository(new CoworkingContext(options));
        }

        [Test]
        public async Task GetByEmail_WhenNoData_ShouldReturnNull()
        {
            var response = await _repository.GetByEmail("email");

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Null);
            });
        }

        [Test]
        public async Task GetByEmail_WhenData_ShouldReturnCustomer()
        {
            var customer = CustomerFixture.CreateCustomer();

            await _repository.Post(customer);

            var response = await _repository.GetByEmail(customer.Email);

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
            });
        }
    }
}
