using Bogus;
using Bogus.Extensions.Brazil;
using Tech.Challenge4.Domain.ObjetoValor;

namespace Tech.Challenge4.Domain.Tests.ObjetoValor
{
    public class CpfTests
    {
        public Faker _faker { get; set; } = new Faker();

        [Test]
        public void Cpf_WhenCpfIsValid_ShouldCreateCpf()
        {
            var cpfValue = _faker.Person.Cpf();
            var cpf = new Cpf(cpfValue);


            Assert.That(cpf.Valor.Equals(cpfValue));
        }

        [Test]
        public void Cpf_WhenCpfIsInvalid_ShouldThrowsArgumentException()
        {
            var cpfValue = "123";
            Assert.Throws<ArgumentException>(() => new Cpf(cpfValue));
        }
    }
}
