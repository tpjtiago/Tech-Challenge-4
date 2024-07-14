namespace Tech.Challenge4.Domain.Models.Sala
{
    public class SalaModel
    {
        public required string Nome { get; set; }
        public required int Capacidade { get; set; }
        public required decimal PrecoHora { get; set; }

        public required int CoworkingId { get; set; }
    }
}
