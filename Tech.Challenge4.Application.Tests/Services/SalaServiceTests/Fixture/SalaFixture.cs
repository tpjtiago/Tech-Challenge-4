using Bogus;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Models.Sala;
using Tech.Challenge4.Domain.Tests.Services.CoworkingServiceTests.Fixture;
using Tech.Challenge4.Domain.Tests.Services.ReservaServiceTests.Fixture;

namespace Tech.Challenge4.Domain.Tests.Services.SalaServiceTests.Fixture
{
    public static class SalaFixture
    {
        public static Faker _faker { get; set; } = new Faker("pt_BR");

        public static Sala CreateSala()
        {
            return new Sala(
                nome: _faker.Person.FullName,
                capacidade: _faker.Random.Int(1, 100),
                precoHora: _faker.Random.Decimal(1, 100)
            );
        }

        public static SalaModel CreateSalaModel()
        {
            return new SalaModel
            {
                Nome = _faker.Random.String2(10),
                Capacidade = _faker.Random.Int(1, 100),
                PrecoHora = _faker.Random.Decimal(1, 100),
                CoworkingId = 1,
            };
        }
    }
}
