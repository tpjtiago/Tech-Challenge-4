using System;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Enumerables;
using Tech.Challenge4.Domain.Tests.EntitiesTests.ReservaTests.Fixture;

namespace Tech.Challenge4.Domain.Tests.EntitiesTests.ReservaTests
{
    public class ReservaTest
    {

        [Test]
        public void Reserva_InvalidHoraInicio_ThrowsArgumentException()
        {

            var input = ReservaFixture.CreateReserva();

            input.HoraInicio = input.HoraFinal.AddHours(1);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new Reserva(
               dataReserva: input.DataReserva,
               horaInicio: input.HoraInicio,
               horaFinal: input.HoraFinal,
               statusReserva: input.StatusReserva,
               comparecimento: input.Comparecimento,
               valor: input.Valor,
               dataPagamento: input.DataPagamento,
               statusPagamento: input.StatusPagamento
               ));

            // Assert
            Assert.That(ex.Message, Does.Contain("A hora de início deve ser anterior à hora final"));
        }

        [Test]
        public void Reserva_InvalidDataPagamento_ThrowsArgumentException()
        {

            var input = ReservaFixture.CreateReserva();

            input.DataPagamento = DateTime.Now.AddHours(1);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new Reserva(
               dataReserva: input.DataReserva,
               horaInicio: input.HoraInicio,
               horaFinal: input.HoraFinal,
               statusReserva: input.StatusReserva,
               comparecimento: input.Comparecimento,
               valor: input.Valor,
               dataPagamento: input.DataPagamento,
               statusPagamento: input.StatusPagamento
               ));

            // Assert
            Assert.That(ex.Message, Does.Contain("A data de pagamento não pode ser futura"));
        }

        [Test]
        public void Reserva_WhenValorIsLowerThanZero_ThrowsArgumentException()
        {

            var input = ReservaFixture.CreateReserva();

            input.Valor = -1;

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new Reserva(
               dataReserva: input.DataReserva,
               horaInicio: input.HoraInicio,
               horaFinal: input.HoraFinal,
               statusReserva: input.StatusReserva,
               comparecimento: input.Comparecimento,
               valor: input.Valor,
               dataPagamento: input.DataPagamento,
               statusPagamento: input.StatusPagamento
               ));

            // Assert
            Assert.That(ex.Message, Does.Contain("O valor da reserva deve ser positivo"));
        }

        [Test]
        public void Reserva_WhenStatusPagamentoIsNotPendenteAndHasNoDataPagamento_ThrowsArgumentException()
        {

            var input = ReservaFixture.CreateReserva();

            input.StatusPagamento = StatusPagamento.Concluido;
            input.DataPagamento = null;

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new Reserva(
               dataReserva: input.DataReserva,
               horaInicio: input.HoraInicio,
               horaFinal: input.HoraFinal,
               statusReserva: input.StatusReserva,
               comparecimento: input.Comparecimento,
               valor: input.Valor,
               dataPagamento: input.DataPagamento,
               statusPagamento: input.StatusPagamento
               ));

            // Assert
            Assert.That(ex.Message, Does.Contain("Se o status de pagamento não for 'Pendente', a data de pagamento é obrigatória"));
        }

    }
}
