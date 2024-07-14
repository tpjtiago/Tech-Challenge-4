using AutoMapper;
using Moq;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Application.Services;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Exceptions;
using Tech.Challenge4.Domain.Models.Coworking;
using Tech.Challenge4.Domain.Tests.Services.CoworkingServiceTests.Fixture;

namespace Tech.Challenge4.Domain.Tests.Services.CoworkingServiceTests
{
    public class CoworkingServiceTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<ICoworkingRepository> _mockRepository;
        private CoworkingService _service;

        [SetUp]
        public void Setup()
        {

            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<ICoworkingRepository>();

            _service = new CoworkingService(_mockMapper.Object, _mockRepository.Object);
        }

        [Test]
        public async Task DeleteById_WhenCoworkExist_DeleteSuccess()
        {
            var input = CoworkingFixture.CreateCoworking();
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(input);

            await _service.DeleteById(1);

            Assert.Multiple(() =>
            {
                _mockRepository.Verify(x => x.Delete(It.IsAny<Coworking>()), Times.Once);
            });
        }

        [Test]
        public async Task DeleteById_WhenCoworkNotExist_ShouldThrowsNotFoundException()
        {
            var input = CoworkingFixture.CreateCoworking();
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Coworking)null);

            Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteById(1));
        }

        [Test]
        public async Task GetAll_WhenCalled_ShouldReturnAllCoworkings()
        {
            var input = CoworkingFixture.CreateCoworking();
            _mockRepository.Setup(x => x.GetAll()).ReturnsAsync([input]);

            var result = await _service.GetAll();

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.EqualTo(1));
                Assert.That(result.First().Id, Is.EqualTo(input.Id));
            });
        }

        [Test]
        public async Task GetById_WhenCoworkExist_ShouldReturnCoworking()
        {
            var input = CoworkingFixture.CreateCoworking();
            _mockRepository.Setup(x => x.GetByIdWithSalas(It.IsAny<int>())).ReturnsAsync(input);

            var result = await _service.GetById(1);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(input.Id));
            });
        }

        [Test]
        public async Task GetById_WhenCoworkNotExist_ShouldThrowsNotFoundException()
        {
            var input = CoworkingFixture.CreateCoworking();
            _mockRepository.Setup(x => x.GetByIdWithSalas(It.IsAny<int>())).ReturnsAsync((Coworking)null);

            Assert.ThrowsAsync<NotFoundException>(() => _service.GetById(1));
        }

        [Test]
        public async Task Post_WhenCalled_ShouldReturnCoworking()
        {
            var input = CoworkingFixture.CreateCoworking();
            var inputModel = CoworkingFixture.CreateCoworkingModel();
            _mockMapper.Setup(x => x.Map<Coworking>(It.IsAny<CoworkingModel>())).Returns(input);
            _mockRepository.Setup(x => x.Post(It.IsAny<Coworking>())).ReturnsAsync(input);

            var result = await _service.Post(inputModel);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(input.Id));
            });
        }

        [Test]
        public async Task Put_WhenCoworkExist_ShouldReturnCoworking()
        {
            var input = CoworkingFixture.CreateCoworking();
            var inputModel = CoworkingFixture.CreateCoworkingModel();
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(input);
            _mockMapper.Setup(x => x.Map(It.IsAny<CoworkingModel>(), It.IsAny<Coworking>())).Returns(input);
            _mockRepository.Setup(x => x.Put(It.IsAny<Coworking>())).ReturnsAsync(input);

            var result = await _service.Put(1, inputModel);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(input.Id));
            });
        }

        [Test]
        public async Task Put_WhenCoworkNotExist_ShouldThrowsNotFoundException()
        {
            var input = CoworkingFixture.CreateCoworking();
            var inputModel = CoworkingFixture.CreateCoworkingModel();
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Coworking)null);

            Assert.ThrowsAsync<NotFoundException>(() => _service.Put(1, inputModel));
        }
    }
}
