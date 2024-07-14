using Microsoft.EntityFrameworkCore;
using Tech.Challenge4.Data.Contexts;
using Tech.Challenge4.Data.Repositories;
using TechChallenge4.Data.Tests.RepositoriesTests.CoworkingTests.Fixture;

namespace TechChallenge4.Data.Tests.RepositoriesTests.CoworkingTests
{
    public class CoworkingRepositoryTests
    {
        private CoworkingRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CoworkingContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _repository = new CoworkingRepository(new CoworkingContext(options));
        }

        [Test]
        public async Task GetByIdWithSalas_WhenNoData_ShouldReturnZero()
        {
            var response = await _repository.GetByIdWithSalas(1);

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Null);
            });

        }

        [Test]
        public async Task GetByIdWithSalas_WhenData_ShouldReturnData()
        {
            var entity = CoworkingFixture.CreateCoworking();

            await _repository.Post(entity);

            var response = await _repository.GetByIdWithSalas(entity.Id);

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
            });
        }

    }
}
