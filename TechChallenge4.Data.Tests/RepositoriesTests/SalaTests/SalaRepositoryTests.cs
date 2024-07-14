using Microsoft.EntityFrameworkCore;
using Tech.Challenge4.Data.Contexts;
using Tech.Challenge4.Data.Repositories;
using TechChallenge4.Data.Tests.RepositoriesTests.CoworkingTests.Fixture;
using TechChallenge4.Data.Tests.RepositoriesTests.SalaTests.Fixture;

namespace TechChallenge4.Data.Tests.RepositoriesTests.SalaTests
{
    public class SalaRepositoryTests
    {
        private SalaRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CoworkingContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _repository = new SalaRepository(new CoworkingContext(options));
        }

        [Test]
        public async Task GetAllWithCoworking_WhenNoData_ShouldReturnZero()
        {
            var response = await _repository.GetAllWithCoworking();

            Assert.Multiple(() =>
            {
                Assert.That(response.Count, Is.EqualTo(0));
            });

        }

        [Test]
        public async Task GetAllWithCoworking_WhenData_ShouldReturnData()
        {
            var entity = SalaFixture.CreateSala();
            entity.Coworking = CoworkingFixture.CreateCoworking();

            await _repository.Post(entity);

            var response = await _repository.GetAllWithCoworking();

            Assert.Multiple(() =>
            {
                Assert.That(response.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task GetByIdWithCoworking_WhenData_ShouldReturnData()
        {
            var entity = SalaFixture.CreateSala();
            entity.Coworking = CoworkingFixture.CreateCoworking();

            await _repository.Post(entity);

            var response = await _repository.GetByIdWithCoworking(entity.Id);

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
            });
        }

        [Test]
        public async Task GetByIdWithCoworking_WhenNoData_ShouldReturnNull()
        {
            var response = await _repository.GetByIdWithCoworking(1);

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Null);
            });
        }
    }
}
