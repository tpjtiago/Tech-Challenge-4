namespace Tech.Challenge4.Domain.Entities
{
    public class Sala : BaseEntity
    {
        public string Nome { get; set; }
        public int Capacidade { get; set; }
        public decimal PrecoHora { get; set; }
        public int CoworkingId { get; set; }
        public Coworking? Coworking { get; set; }
        public List<Reserva> Reservas { get; set; } = [];

        public Sala(
        string nome,
        int capacidade,
        decimal precoHora)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome da sala é obrigatório.");
            }

            if (capacidade <= 0)
            {
                throw new ArgumentException("A capacidade da sala deve ser positiva.");
            }

            if (precoHora <= 0)
            {
                throw new ArgumentException("O preço por hora da sala deve ser positivo.");
            }

            Nome = nome;
            Capacidade = capacidade;
            PrecoHora = precoHora;
        }
    }
}
