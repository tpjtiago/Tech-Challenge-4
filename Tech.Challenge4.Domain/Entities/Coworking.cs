namespace Tech.Challenge4.Domain.Entities
{
    public class Coworking : BaseEntity
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
        public TimeOnly HoraAbertura { get; set; }
        public TimeOnly HoraFechamento { get; set; }

        public List<Sala>? Salas { get; set; }

        public Coworking(
            string nome,
            string endereco,
            string descricao,
            TimeOnly horaAbertura,
            TimeOnly horaFechamento
        )
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("Nome é obrigatório", nome);
            }

            if (string.IsNullOrWhiteSpace(endereco))
            {
                throw new ArgumentException("Endereço é obrigatório", endereco);
            }

            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new ArgumentException("Descrição é obrigatória", descricao);
            }

            if (horaFechamento < horaAbertura)
            {
                throw new ArgumentException("O horário de fechamento deve ser posterior ao horário de abertura", nameof(horaFechamento));
            }

            Nome = nome;
            Endereco = endereco;
            Descricao = descricao;
            HoraAbertura = horaAbertura;
            HoraFechamento = horaFechamento;
        }
    }
}
