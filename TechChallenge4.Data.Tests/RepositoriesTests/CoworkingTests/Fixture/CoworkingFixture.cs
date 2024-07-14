using Bogus;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Models.Coworking;

namespace TechChallenge4.Data.Tests.RepositoriesTests.CoworkingTests.Fixture
{
    public static class CoworkingFixture
    {
        public static Faker _faker { get; set; } = new Faker();

        public static Coworking CreateCoworking()
        {
            return new Coworking(
                nome: _faker.Company.CompanyName(),
                endereco: _faker.Address.FullAddress(),
                descricao: _faker.Lorem.Paragraph(),
                horaAbertura: new TimeOnly(13, 0),
                horaFechamento: new TimeOnly(14, 0));
        }

        public static CoworkingModel CreateCoworkingModel()
        {
            return new CoworkingModel
            {
                Nome = _faker.Company.CompanyName(),
                Endereco = _faker.Address.FullAddress(),
                Descricao = _faker.Lorem.Paragraph(),
                HoraAbertura = new TimeOnly(13, 0),
                HoraFechamento = new TimeOnly(14, 0)
            };
        }
    }
}
