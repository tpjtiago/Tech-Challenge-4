using AutoMapper;
using Moq;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Application.Services;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Exceptions;
using Tech.Challenge4.Domain.Models.Customers;
using Tech.Challenge4.Domain.Tests.Services.CustomerServiceTests.Fixture;

namespace Tech.Challenge4.Domain.Tests.Services.CustomerServiceTests
{
    public class CustomerServiceTests
    {
        private Mock<IMapper> _mockMapper;
        private Mock<ICustomerRepository> _mockRepository;
        private CustomerService _service;

        [SetUp]
        public void Setup()
        {

            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<ICustomerRepository>();

            _service = new CustomerService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task DeleteById_WhenCustomerExist_DeleteSuccess()
        {
            var input = CustomerFixture.CreateCustomer();
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(input);

            await _service.DeleteById(1);

            Assert.Multiple(() =>
            {
                _mockRepository.Verify(x => x.Delete(It.IsAny<Customer>()), Times.Once);
            });
        }

        [Test]
        public async Task DeleteById_WhenCustomerNotExist_ShouldThrowsNotFoundException()
        {
            var input = CustomerFixture.CreateCustomer();
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Customer)null);

            Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteById(1));
        }

        [Test]
        public async Task GetAll_WhenCalled_ShouldReturnAllCustomers()
        {
            var input = CustomerFixture.CreateCustomer();
            _mockRepository.Setup(x => x.GetAll()).ReturnsAsync([input]);

            var result = await _service.GetAll();

            Assert.Multiple(() =>
            {
                _mockRepository.Verify(x => x.GetAll(), Times.Once);
            });
        }

        [Test]
        public async Task GetById_WhenCustomerExist_ShouldReturnCustomer()
        {
            var input = CustomerFixture.CreateCustomer();
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(input);
            _mockMapper.Setup(x => x.Map<CustomerModel>(It.IsAny<Customer>())).Returns(CustomerFixture.CreateCustomerModel());

            var result = await _service.GetById(1);

            Assert.Multiple(() =>
            {
                _mockRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
                Assert.That(input.Cpf, Is.EqualTo(result.Cpf));
            });
        }

        [Test]
        public async Task GetById_WhenCustomerNotExist_ShouldThrowsNotFoundException()
        {
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Customer)null);

            Assert.ThrowsAsync<NotFoundException>(() => _service.GetById(1));
        }

        [Test]
        public async Task Put_WhenCustomerExist_ShouldReturnCustomer()
        {
            var input = CustomerFixture.CreateCustomerModel();
            var customer = CustomerFixture.CreateCustomer();
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(customer);

            var result = await _service.Put(1, input);

            Assert.Multiple(() =>
            {
                _mockRepository.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
                Assert.That(input.Cpf, Is.EqualTo(customer.Cpf));
            });
        }

        [Test]
        public async Task Put_WhenCustomerNotExist_ShouldThrowsNotFoundException()
        {
            var input = CustomerFixture.CreateCustomerModel();
            _mockRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((Customer)null);

            Assert.ThrowsAsync<NotFoundException>(() => _service.Put(1, input));
        }

        [Test]
        public async Task Put_WhenEmailConflict_ShouldThrowsError()
        {
            var input = CustomerFixture.CreateCustomerModel();
            var customer = CustomerFixture.CreateCustomer();
            _mockRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync(customer);

            Assert.ThrowsAsync<ConflictException>(() => _service.Put(1, input));
        }

        [Test]
        public async Task Post_WhenCalled_ShouldReturnCustomer()
        {
            var input = CustomerFixture.CreateCustomer();
            var inputModel = CustomerFixture.CreateCustomerModel();
            _mockMapper.Setup(x => x.Map<Customer>(It.IsAny<CustomerModel>())).Returns(input);
            _mockRepository.Setup(x => x.Post(It.IsAny<Customer>())).ReturnsAsync(input);

            var result = await _service.Post(inputModel);

            Assert.Multiple(() =>
            {
                _mockRepository.Verify(x => x.Post(It.IsAny<Customer>()), Times.Once);
                Assert.That(input.Cpf, Is.EqualTo(result.Cpf));
            });
        }

        [Test]
        public async Task Post_WhenEmailConflict_ShouldThrowsError()
        {
            var input = CustomerFixture.CreateCustomer();
            var inputModel = CustomerFixture.CreateCustomerModel();
            _mockRepository.Setup(x => x.GetByEmail(It.IsAny<string>())).ReturnsAsync(input);

            Assert.ThrowsAsync<ConflictException>(() => _service.Post(inputModel));

        }
    }
}
