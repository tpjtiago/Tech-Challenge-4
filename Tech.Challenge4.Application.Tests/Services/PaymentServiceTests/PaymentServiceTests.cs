using FluentValidation;
using Moq;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Application.Services;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Tests.Services.PaymentServiceTests.Fixture;
using Tech.Challenge4.Domain.Tests.Services.ReservaServiceTests.Fixture;

namespace Tech.Challenge4.Domain.Tests.Services.PaymentServiceTests
{
    public class PaymentServiceTests
    {
        private Mock<IReservaRepository> _mockRepository;
        private PaymentService _service;

        [SetUp]
        public void Setup()
        {

            _mockRepository = new Mock<IReservaRepository>();

            _service = new PaymentService(_mockRepository.Object);
        }

        [Test]
        public async Task ReservationPayment_WhenReservationExist_PaymentSuccess()
        {
            var input = PaymentFixture.CreatePaymentModel();
            var reserva = ReservaFixture.CreateReserva();

            input.ReservationPaymentValue = reserva.Valor;

            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(reserva);

            await _service.ReservationPayment(input);

            Assert.Multiple(() =>
            {
                _mockRepository.Verify(x => x.Put(It.IsAny<Reserva>()), Times.Once);
            });
        }

        [Test]
        public async Task ReservationPayment_WhenReservationNotExist_ShouldThrowsValidationException()
        {
            var input = PaymentFixture.CreatePaymentModel();

            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Reserva)null);

            Assert.ThrowsAsync<ValidationException>(() => _service.ReservationPayment(input));
        }

        [Test]
        public async Task ReservationPayment_WhenReservationValueIsDifferent_ShouldThrowsValidationException()
        {
            var input = PaymentFixture.CreatePaymentModel();
            var reserva = ReservaFixture.CreateReserva();

            input.ReservationPaymentValue = reserva.Valor + 1;

            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(reserva);

            Assert.ThrowsAsync<ValidationException>(() => _service.ReservationPayment(input));
        }

    }
}
