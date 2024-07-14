using Bogus;

namespace Tech.Challenge4.Domain.Tests.EntitiesTest.Coworking
{
    public static class CoworkingFixture
    {
        public static Faker _faker { get; set; } = new Faker();

        public static Domain.Entities.Coworking CreateCoworking()
        {
            return new Domain.Entities.Coworking(
                nome: _faker.Company.CompanyName(),
                endereco: _faker.Address.FullAddress(),
                descricao: _faker.Lorem.Paragraph(),
                horaAbertura: new TimeOnly(13, 0),
                horaFechamento: new TimeOnly(14, 0));
        }
    }
}
