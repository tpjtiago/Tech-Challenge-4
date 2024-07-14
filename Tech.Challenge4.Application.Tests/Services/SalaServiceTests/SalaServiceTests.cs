using AutoMapper;
using Moq;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Application.Services;
using Tech.Challenge4.Domain.Contracts.Services.Coworkings;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Exceptions;
using Tech.Challenge4.Domain.Models.Sala;
using Tech.Challenge4.Domain.Tests.Services.CoworkingServiceTests.Fixture;
using Tech.Challenge4.Domain.Tests.Services.SalaServiceTests.Fixture;

namespace Tech.Challenge4.Domain.Tests.Services.SalaServiceTests
{
    public class SalaServiceTests
    {

        private Mock<IMapper> _mockMapper;
        private Mock<ISalaRepository> _mockRepository;
        private Mock<ICoworkingService> _mockCoworkingService;
        private SalaService _service;

        [SetUp]
        public void Setup()
        {

            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<ISalaRepository>();
            _mockCoworkingService = new Mock<ICoworkingService>();

            _service = new SalaService(_mockMapper.Object, _mockCoworkingService.Object, _mockRepository.Object);
        }

        [Test]
        public async Task DeleteById_WhenDoesNotHasSala_ShouldThrowsNotFoundException()
        {
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Sala)null);

            Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteById(It.IsAny<int>()));
        }

        [Test]
        public async Task DeleteById_WhenSalaExists_ShouldBeDeleted()
        {
            var sala = SalaFixture.CreateSala();

            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(sala);

            await _service.DeleteById(It.IsAny<int>());

            _mockRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task GetAll_WhenCalled_ShouldReturn()
        {
            var sala = SalaFixture.CreateSala();

            _mockRepository.Setup(x => x.GetAllWithCoworking()).ReturnsAsync([sala]);

            var response = await _service.GetAll();

            Assert.Multiple(() =>
            {
                Assert.That(response.Count, Is.EqualTo(1));
                _mockRepository.Verify(x => x.GetAllWithCoworking(), Times.Once);
            });
        }

        [Test]
        public async Task GetById_WhenSalaDoesNotExists_ShouldThrowsNotFoundException()
        {
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Sala)null);

            Assert.ThrowsAsync<NotFoundException>(() => _service.GetById(It.IsAny<int>()));
        }

        [Test]
        public async Task GetById_WhenSalaExists_ShouldReturn()
        {
            var sala = SalaFixture.CreateSala();

            _mockRepository.Setup(x => x.GetByIdWithCoworking(It.IsAny<int>())).ReturnsAsync(sala);

            await _service.GetById(It.IsAny<int>());

            _mockRepository.Verify(x => x.GetByIdWithCoworking(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task Post_WhenCoworkingDoesNotExist_ShouldThrowsNotFoundException()
        {
            var salaModel = SalaFixture.CreateSalaModel();

            _mockCoworkingService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Coworking)null);

            Assert.ThrowsAsync<NotFoundException>(() => _service.Post(salaModel));

        }

        [Test]
        public async Task Post_WhenCoworkingExists_ShouldReturn()
        {
            var salaModel = SalaFixture.CreateSalaModel();
            var sala = SalaFixture.CreateSala();
            var coworking = CoworkingFixture.CreateCoworking();

            _mockCoworkingService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(coworking);
            _mockMapper.Setup(x => x.Map<Sala>(It.IsAny<SalaModel>())).Returns(sala);
            _mockRepository.Setup(x => x.Post(It.IsAny<Sala>())).ReturnsAsync(sala);

            var response = await _service.Post(salaModel);

            Assert.Multiple(() =>
            {
                Assert.That(response.Id, Is.EqualTo(sala.Id));

                _mockCoworkingService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            });
        }

        [Test]
        public async Task Put_WhenCoworkingDoesNotExist_ShouldThrowsNotFoundException()
        {
            var salaModel = SalaFixture.CreateSalaModel();

            _mockCoworkingService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Coworking)null);

            Assert.ThrowsAsync<NotFoundException>(() => _service.Put(It.IsAny<int>(), salaModel));

        }

        [Test]
        public async Task Put_WhenSalaDoesNotExist_ShouldThrowsNotFoundException()
        {
            var salaModel = SalaFixture.CreateSalaModel();
            var coworking = CoworkingFixture.CreateCoworking();

            _mockCoworkingService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(coworking);
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Sala)null);

            Assert.ThrowsAsync<NotFoundException>(() => _service.Put(It.IsAny<int>(), salaModel));

        }

        [Test]
        public async Task Put_WhenSalaExists_ShouldReturn()
        {
            var salaModel = SalaFixture.CreateSalaModel();
            var sala = SalaFixture.CreateSala();
            sala.Coworking = CoworkingFixture.CreateCoworking();
            var coworking = CoworkingFixture.CreateCoworking();

            _mockCoworkingService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(coworking);
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(sala);
            _mockMapper.Setup(x => x.Map(It.IsAny<SalaModel>(), It.IsAny<Sala>())).Returns(sala);
            _mockRepository.Setup(x => x.Put(It.IsAny<Sala>())).ReturnsAsync(sala);

            var response = await _service.Put(It.IsAny<int>(), salaModel);

            Assert.Multiple(() =>
            {
                Assert.That(response.Id, Is.EqualTo(sala.Id));

                _mockCoworkingService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            });
        }

    }
}
