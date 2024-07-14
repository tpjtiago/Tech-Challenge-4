namespace Tech.Challenge4.Domain.Models.Reserva
{
    public class ReservaModel
    {
        public required int CustomerID { get; set; }
        public required int SalaID { get; set; }
        public required DateOnly DataReserva { get; set; }
        public required TimeOnly HoraInicio { get; set; }
        public required TimeOnly HoraFinal { get; set; }
    }
}
