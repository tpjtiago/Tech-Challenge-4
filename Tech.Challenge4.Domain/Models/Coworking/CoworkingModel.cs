namespace Tech.Challenge4.Domain.Models.Coworking
{
    public class CoworkingModel
    {
        public required string Nome { get; set; }
        public required string Endereco { get; set; }
        public required string Descricao { get; set; }
        public required TimeOnly HoraAbertura { get; set; }
        public required TimeOnly HoraFechamento { get; set; }
    }
}
