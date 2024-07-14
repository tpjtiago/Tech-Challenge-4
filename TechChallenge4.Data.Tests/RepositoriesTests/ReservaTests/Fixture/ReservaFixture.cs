using Bogus;
using Tech.Challenge4.Data.Tests.RepositoriesTests.CustomerTests.Fixture;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Enumerables;
using Tech.Challenge4.Domain.Models.Reserva;
using TechChallenge4.Data.Tests.RepositoriesTests.SalaTests.Fixture;

namespace Tech.Challenge4.Data.Tests.RepositoriesTests.ReservaTests.Fixture
{
    public static class ReservaFixture
    {
        public static Faker _faker { get; set; } = new Faker();

        public static Reserva CreateReserva()
        {
            return new Reserva(
                dataReserva: _faker.Date.PastDateOnly(),
                horaInicio: new TimeOnly(13,0),
                horaFinal: new TimeOnly(14, 0),  // Ensure HoraFinal is later than HoraInicio
                statusReserva: StatusReserva.Pendente,
                comparecimento: true,
                valor: _faker.Random.Decimal(),
                dataPagamento: _faker.Date.Past(), // DataPagamento
                statusPagamento: StatusPagamento.Pendente
            );
        }

        public static ReservaModel CreateReservaModel()
        {
            return new ReservaModel
            {
                CustomerID = 1,
                DataReserva = _faker.Date.PastDateOnly(),
                HoraFinal = new TimeOnly(13, 0),
                HoraInicio = new TimeOnly(14, 0),
                SalaID = 1
            };
        }
    }
}
