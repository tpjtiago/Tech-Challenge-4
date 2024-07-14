using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Tests.EntitiesTests.SalaTests.Fixture;

namespace Tech.Challenge4.Domain.Tests.EntitiesTests.SalaTests
{
    public class SalaTest
    {

        [Test]
        public void Sala_WhenNomeIsEmpty_ShouldThrowArgumentException()
        {
            var input = SalaFixture.CreateSala();
            input.Nome = string.Empty;

            var exception = Assert.Throws<ArgumentException>(() => new Sala(
                nome: input.Nome,
                capacidade: input.Capacidade,
                precoHora: input.PrecoHora));

            Assert.That(exception.Message, Does.Contain("O nome da sala é obrigatório"));
        }

        [Test]
        public void Sala_WhenCapacidadeLessThanOrEqualZero_ShouldThrowArgumentException()
        {
            var input = SalaFixture.CreateSala();
            input.Capacidade = 0;

            var exception = Assert.Throws<ArgumentException>(() => new Sala(
                nome: input.Nome,
                capacidade: input.Capacidade,
                precoHora: input.PrecoHora));

            Assert.That(exception.Message, Does.Contain("A capacidade da sala deve ser positiva"));
        }

        [Test]
        public void Sala_WhenPrecoHoraLessThanOrEqualZero_ShouldThrowArgumentException()
        {
            var input = SalaFixture.CreateSala();
            input.PrecoHora = 0;

            var exception = Assert.Throws<ArgumentException>(() => new Sala(
                nome: input.Nome,
                capacidade: input.Capacidade,
                precoHora: input.PrecoHora));

            Assert.That(exception.Message, Does.Contain("O preço por hora da sala deve ser positivo"));
        }
    }
}
