using Bogus;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Models.Reserva;

namespace Tech.Challenge4.Domain.Tests.Services.ReservaServiceTests.Fixture
{
    public static class ReservaFixture
    {
        public static Faker _faker { get; set; } = new Faker();

        public static Reserva CreateReserva()
        {
            return new Reserva(
                dataReserva: _faker.Date.PastDateOnly(),
                horaInicio: new TimeOnly(9,0),
                horaFinal: new TimeOnly(10, 0),  // Ensure HoraFinal is later than HoraInicio
                statusReserva: Enumerables.StatusReserva.Pendente,
                comparecimento: true,
                valor: _faker.Random.Decimal(),
                dataPagamento: _faker.Date.Past(), // DataPagamento
                statusPagamento: Enumerables.StatusPagamento.Pendente
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
