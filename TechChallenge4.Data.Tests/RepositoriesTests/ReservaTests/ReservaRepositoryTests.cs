using Microsoft.EntityFrameworkCore;
using Tech.Challenge4.Data.Contexts;
using Tech.Challenge4.Data.Repositories;
using Tech.Challenge4.Data.Tests.RepositoriesTests.CustomerTests.Fixture;
using Tech.Challenge4.Data.Tests.RepositoriesTests.ReservaTests.Fixture;
using TechChallenge4.Data.Tests.RepositoriesTests.SalaTests.Fixture;

namespace TechChallenge4.Data.Tests.RepositoriesTests.ReservaTests
{
    public class ReservaRepositoryTests
    {
        private ReservaRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CoworkingContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _repository = new ReservaRepository(new CoworkingContext(options));
        }

        [Test]
        public async Task GetByIdWithCustomerSala_WhenNoData_ShouldReturnNull()
        {
            var response = await _repository.GetByIdWithCustomerSala(1);

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Null);
            });
        }

        [Test]
        public async Task GetByIdWithCustomerSala_WhenData_ShouldReturnReserva()
        {
            var reserva = ReservaFixture.CreateReserva();

            reserva.Customer = CustomerFixture.CreateCustomer();
            reserva.Sala = SalaFixture.CreateSala();

            await _repository.Post(reserva);

            var response = await _repository.GetByIdWithCustomerSala(1);

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
            });
        }

        [Test]
        public async Task ReservasSalaFaixaHorario_WhenNoData_ShouldReturnZero()
        {
            var response = await _repository.ReservasSalaFaixaHorario(1, TimeOnly.FromDateTime(DateTime.Now), TimeOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now));

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.EqualTo(0));
            });
        }

        [Test]
        public async Task ReservasSalaFaixaHorario_WhenData_ShouldReturnCount()
        {
            var reserva = ReservaFixture.CreateReserva();

            await _repository.Post(reserva);

            var response = await _repository.ReservasSalaFaixaHorario(reserva.SalaID, reserva.HoraInicio, reserva.HoraFinal, reserva.DataReserva);

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.EqualTo(1));
            });
        }
    }
}
