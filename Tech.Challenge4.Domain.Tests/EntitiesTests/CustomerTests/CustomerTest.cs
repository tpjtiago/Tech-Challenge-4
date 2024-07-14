using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Challenge4.Domain.Entities;

namespace Tech.Challenge4.Domain.Tests.EntitiesTests.CustomerTests
{
    public class CustomerTest
    {

        [Test]
        public void Customer_WhenNameIsEmpty_ShouldThrowArgumentException()
        {
            var input = CustomerFixture.CreateCustomer();
            input.Name = string.Empty;

            var exception = Assert.Throws<ArgumentException>(() => new Customer(
                name: input.Name,
                email: input.Email,
                cpf: input.Cpf,
                phone: input.Phone));

            Assert.That(exception.Message, Does.Contain("Nome é obrigatório"));
        }

        [Test]
        public void Customer_WhenEmailIsEmpty_ShouldThrowArgumentException()
        {
            var input = CustomerFixture.CreateCustomer();
            input.Email = string.Empty;

            var exception = Assert.Throws<ArgumentException>(() => new Customer(
                name: input.Name,
                email: input.Email,
                cpf: input.Cpf,
                phone: input.Phone));

            Assert.That(exception.Message, Does.Contain("Email é obrigatório"));
        }

        [Test]
        public void Customer_WhenCpfIsEmpty_ShouldThrowArgumentException()
        {
            var input = CustomerFixture.CreateCustomer();
            input.Cpf = string.Empty;

            var exception = Assert.Throws<ArgumentException>(() => new Customer(
                name: input.Name,
                email: input.Email,
                cpf: input.Cpf,
                phone: input.Phone));

            Assert.That(exception.Message, Does.Contain("Cpf é obrigatório"));
        }

        [Test]
        public void Customer_WhenPhoneIsEmpty_ShouldThrowArgumentException()
        {
            var input = CustomerFixture.CreateCustomer();
            input.Phone = string.Empty;

            var exception = Assert.Throws<ArgumentException>(() => new Customer(
                name: input.Name,
                email: input.Email,
                cpf: input.Cpf,
                phone: input.Phone));

            Assert.That(exception.Message, Does.Contain("Telefone é obrigatório"));
        }
    }
}
