using AutoMapper;
using FluentValidation;
using Moq;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Application.Services;
using Tech.Challenge4.Domain.Contracts.Services.Customers;
using Tech.Challenge4.Domain.Contracts.Services.Salas;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Enumerables;
using Tech.Challenge4.Domain.Exceptions;
using Tech.Challenge4.Domain.Models.Customers;
using Tech.Challenge4.Domain.Models.Reserva;
using Tech.Challenge4.Domain.Tests.Services.CoworkingServiceTests.Fixture;
using Tech.Challenge4.Domain.Tests.Services.CustomerServiceTests.Fixture;
using Tech.Challenge4.Domain.Tests.Services.ReservaServiceTests.Fixture;
using Tech.Challenge4.Domain.Tests.Services.SalaServiceTests.Fixture;

namespace Tech.Challenge4.Domain.Tests.Services.ReservaServiceTests
{
    public class ReservaServiceTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<IReservaRepository> _mockRepository;
        private Mock<ISalaService> _mockSalaService;
        private Mock<ICustomerService> _mockCustomerService;
        private ReservaService _service;

        [SetUp]
        public void Setup()
        {

            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<IReservaRepository>();
            _mockSalaService = new Mock<ISalaService>();
            _mockCustomerService = new Mock<ICustomerService>();

            _service = new ReservaService(_mockMapper.Object, _mockSalaService.Object, _mockCustomerService.Object, _mockRepository.Object);
        }

        [Test]
        public async Task GetAll_WhenCalled_ShouldReturnAllReservas()
        {
            var input = ReservaFixture.CreateReserva();
            _mockRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<Reserva> { input });

            var result = await _service.GetAll();

            Assert.Multiple(() =>
            {
                _mockRepository.Verify(x => x.GetAll(), Times.Once);
                Assert.AreEqual(input, result.First());
            });
        }

        [Test]
        public async Task GetById_WhenCalled_ShouldReturnReserva()
        {
            var input = ReservaFixture.CreateReserva();
            _mockRepository.Setup(x => x.GetByIdWithCustomerSala(It.IsAny<int>())).ReturnsAsync(input);

            var result = await _service.GetById(1);

            Assert.Multiple(() =>
            {
                _mockRepository.Verify(x => x.GetByIdWithCustomerSala(It.IsAny<int>()), Times.Once);
                Assert.That(input.Id, Is.EqualTo(result.Id));
            });
        }

        [Test]
        public async Task Post_WhenReservaCreated_ShouldReturnReserva()
        {
            var input = ReservaFixture.CreateReserva();
            _mockRepository.Setup(x => x.Post(It.IsAny<Reserva>())).ReturnsAsync(input);

            var result = await _service.Post(input);

            Assert.Multiple(() =>
            {
                _mockRepository.Verify(x => x.Post(It.IsAny<Reserva>()), Times.Once);
                Assert.That(input.Id, Is.EqualTo(result.Id));
            });
        }

        [Test]
        public async Task EfetuarReserva_WhenCustomerDoesNotExist_ShouldThrowValidationException()
        {
            var input = ReservaFixture.CreateReservaModel();

            _mockCustomerService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((CustomerModel)null);

            Assert.ThrowsAsync<ValidationException>(() => _service.EfetuarReserva(input));
        }

        [Test]
        public async Task EfetuarReserva_WhenSalaDoesNotExist_ShouldThrowValidationException()
        {
            var input = ReservaFixture.CreateReservaModel();
            var customerModel = CustomerFixture.CreateCustomerModel();

            _mockCustomerService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(customerModel);
            _mockSalaService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Sala)null);

            Assert.ThrowsAsync<ValidationException>(() => _service.EfetuarReserva(input));
        }

        [Test]
        public async Task EfetuarReserva_WhenHoraInicioIsBeforeOpeningTime_ShouldThrowValidationException()
        {
            var input = ReservaFixture.CreateReservaModel();
            var customerModel = CustomerFixture.CreateCustomerModel();
            var sala = SalaFixture.CreateSala();

            sala.Coworking = CoworkingFixture.CreateCoworking();

            input.HoraInicio = sala.Coworking.HoraAbertura.AddHours(-1);

            _mockCustomerService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(customerModel);
            _mockSalaService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(sala);

            Assert.ThrowsAsync<ValidationException>(() => _service.EfetuarReserva(input));
        }

        [Test]
        public async Task EfetuarReserva_HoraFinalIsAfterClosingTime_ShouldThrowValidationException()
        {
            var input = ReservaFixture.CreateReservaModel();
            var customerModel = CustomerFixture.CreateCustomerModel();
            var sala = SalaFixture.CreateSala();
            sala.Coworking = CoworkingFixture.CreateCoworking();

            input.HoraFinal = sala.Coworking.HoraFechamento.AddHours(1);

            _mockCustomerService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(customerModel);
            _mockSalaService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(sala);

            Assert.ThrowsAsync<ValidationException>(() => _service.EfetuarReserva(input));
        }

        [Test]
        public async Task EfetuarReserva_WhenReservasDisponiveisSalaIsZero_ShouldThrowValidationException()
        {
            var input = ReservaFixture.CreateReservaModel();
            var customerModel = CustomerFixture.CreateCustomerModel();
            var sala = SalaFixture.CreateSala();

            sala.Coworking = CoworkingFixture.CreateCoworking();

            input.HoraFinal = sala.Coworking.HoraFechamento;
            input.HoraInicio = sala.Coworking.HoraAbertura;

            sala.Coworking.HoraFechamento = input.HoraFinal;

            sala.Capacidade = 1;

            _mockCustomerService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(customerModel);
            _mockSalaService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(sala);
            _mockRepository.Setup(x => x.ReservasSalaFaixaHorario(
                It.IsAny<int>(),
                It.IsAny<TimeOnly>(),
                It.IsAny<TimeOnly>(),
                It.IsAny<DateOnly>()
            )).ReturnsAsync(1);


            Assert.ThrowsAsync<ValidationException>(() => _service.EfetuarReserva(input));
        }

        [Test]
        public async Task EfetuarReserva_WhenCalled_ShouldReturnReserva()
        {
            var input = ReservaFixture.CreateReservaModel();
            var customerModel = CustomerFixture.CreateCustomerModel();
            var sala = SalaFixture.CreateSala();
            var reserva = ReservaFixture.CreateReserva();

            sala.Coworking = CoworkingFixture.CreateCoworking();

            input.HoraFinal = sala.Coworking.HoraFechamento;
            input.HoraInicio = sala.Coworking.HoraAbertura;

            sala.Coworking.HoraFechamento = input.HoraFinal;

            sala.Capacidade = 1;

            _mockCustomerService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(customerModel);
            _mockSalaService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(sala);
            _mockRepository.Setup(x => x.ReservasSalaFaixaHorario(
               It.IsAny<int>(),
               It.IsAny<TimeOnly>(),
               It.IsAny<TimeOnly>(),
               It.IsAny<DateOnly>()
            )).ReturnsAsync(0);
            _mockMapper.Setup(x => x.Map<Reserva>(It.IsAny<ReservaModel>())).Returns(reserva);
            _mockRepository.Setup(x => x.Post(It.IsAny<Reserva>())).ReturnsAsync(reserva);

            var result = await _service.EfetuarReserva(input);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(reserva.Id));
            });
        }

        [Test]
        public async Task CancelReservation_WhenReservationDoesNotExist_ShouldThrowValidationException()
        {
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Reserva)null);

            Assert.ThrowsAsync<ValidationException>(() => _service.CancelReservation(1));
        }

        [Test]
        public async Task CancelReservation_WhenReservationIsAlreadyStarted_ShouldThrowValidationException()
        {
            var input = ReservaFixture.CreateReserva();
            input.HoraInicio = TimeOnly.FromDateTime(DateTime.Now.AddHours(-1));
            input.StatusReserva = StatusReserva.Confirmada;

            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(input);

            Assert.ThrowsAsync<ValidationException>(() => _service.CancelReservation(1));
        }

        [Test]
        public async Task CancelReservation_WhenReservationIsAlreadyCanceled_ShouldThrowValidationException()
        {
            var input = ReservaFixture.CreateReserva();
            input.StatusReserva = StatusReserva.Cancelada;

            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(input);

            Assert.ThrowsAsync<ValidationException>(() => _service.CancelReservation(1));
        }

        [Test]
        public async Task CancelReservation_WhenCanCancel_ShouldCancel()
        {
            var input = ReservaFixture.CreateReserva();
            input.StatusReserva = StatusReserva.Confirmada;
            input.HoraInicio = TimeOnly.FromDateTime(DateTime.Now.AddHours(1));

            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(input);

            var result = await _service.CancelReservation(1);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
            });

        }

        [Test]
        public async Task CalcularValorReserva_WhenHorasCobradasOverpass_ShouldChargeHours()
        {
            var input = ReservaFixture.CreateReserva();
            var sala = SalaFixture.CreateSala();

            input.HoraInicio = TimeOnly.FromDateTime(DateTime.Now);
            input.HoraFinal = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(30));

            sala.PrecoHora = 10;

            var result = _service.CalcularValorReserva(input, sala);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(10));
            });

        }
    }
}
