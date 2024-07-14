using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Tests.EntitiesTest.Coworking;

namespace Tech.Challenge4.Domain.Tests.EntitiesTests.Coworking
{
    public class CoworkingTests
    {

        [Test]
        public void Coworking_WhenNameIsEmpty_ShouldThrowArgumentException()
        {
            var input = CoworkingFixture.CreateCoworking();
            input.Nome = string.Empty;

            var exception = Assert.Throws<ArgumentException>(() => new Entities.Coworking(
                nome: input.Nome,
                endereco: input.Endereco,
                descricao: input.Descricao,
                horaAbertura: input.HoraAbertura,
                horaFechamento: input.HoraFechamento));

            Assert.That(exception.Message, Does.Contain("Nome é obrigatório"));
        }

        [Test]
        public void Coworking_WhenEnderecoIsEmpty_ShouldThrowArgumentException()
        {
            var input = CoworkingFixture.CreateCoworking();
            input.Endereco = string.Empty;

            var exception = Assert.Throws<ArgumentException>(() => new Entities.Coworking(
                nome: input.Nome,
                endereco: input.Endereco,
                descricao: input.Descricao,
                horaAbertura: input.HoraAbertura,
                horaFechamento: input.HoraFechamento));

            Assert.That(exception.Message, Does.Contain("Endereço é obrigatório"));
        }

        [Test]
        public void Coworking_WhenDescricaoIsEmpty_ShouldThrowArgumentException()
        {
            var input = CoworkingFixture.CreateCoworking();
            input.Descricao = string.Empty;

            var exception = Assert.Throws<ArgumentException>(() => new Entities.Coworking(
                nome: input.Nome,
                endereco: input.Endereco,
                descricao: input.Descricao,
                horaAbertura: input.HoraAbertura,
                horaFechamento: input.HoraFechamento));

            Assert.That(exception.Message, Does.Contain("Descrição é obrigatória"));
        }

        [Test]
        public void Coworking_WhenHoraFechamentoLessThanHoraAbertura_ShouldThrowArgumentException()
        {
            var input = CoworkingFixture.CreateCoworking();
            input.HoraFechamento = input.HoraAbertura.AddHours(-2);

            var exception = Assert.Throws<ArgumentException>(() => new Entities.Coworking(
                nome: input.Nome,
                endereco: input.Endereco,
                descricao: input.Descricao,
                horaAbertura: input.HoraAbertura,
                horaFechamento: input.HoraFechamento));

            Assert.That(exception.Message, Does.Contain("O horário de fechamento deve ser posterior ao horário de abertura"));
        }

    }
}
