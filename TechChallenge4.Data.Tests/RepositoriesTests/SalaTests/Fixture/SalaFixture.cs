using Bogus;
using Tech.Challenge4.Domain.Entities;
using TechChallenge4.Data.Tests.RepositoriesTests.CoworkingTests.Fixture;

namespace TechChallenge4.Data.Tests.RepositoriesTests.SalaTests.Fixture
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
    }
}
