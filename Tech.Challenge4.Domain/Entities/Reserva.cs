using Tech.Challenge4.Domain.Enumerables;

namespace Tech.Challenge4.Domain.Entities
{
    public class Reserva : BaseEntity
    {
        public DateOnly DataReserva { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFinal { get; set; }
        public StatusReserva StatusReserva { get; set; }
        public bool Comparecimento { get; set; }
        public decimal Valor { get; set; }
        public DateTime? DataPagamento { get; set; }
        public StatusPagamento StatusPagamento { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public int SalaID { get; set; }
        public Sala Sala { get; set; }

        public Reserva()
        {
            
        }
        public Reserva(
            DateOnly dataReserva,
            TimeOnly horaInicio,
            TimeOnly horaFinal,
            StatusReserva statusReserva,
            bool comparecimento,
            decimal valor,
            DateTime? dataPagamento,
            StatusPagamento statusPagamento)
        {
            // Validações de data e hora
            if (horaInicio >= horaFinal)
            {
                throw new ArgumentException("A hora de início deve ser anterior à hora final.");
            }

            if (dataPagamento.HasValue && dataPagamento.Value > DateTime.Now)
            {
                throw new ArgumentException("A data de pagamento não pode ser futura.");
            }

            // Validações de valor e status de pagamento
            if (valor <= 0)
            {
                throw new ArgumentException("O valor da reserva deve ser positivo.");
            }

            if (statusPagamento != StatusPagamento.Pendente && !dataPagamento.HasValue)
            {
                throw new ArgumentException("Se o status de pagamento não for 'Pendente', a data de pagamento é obrigatória.");
            }

            DataReserva = dataReserva;
            HoraInicio = horaInicio;
            HoraFinal = horaFinal;
            StatusReserva = statusReserva;
            Comparecimento = comparecimento;
            Valor = valor;
            DataPagamento = dataPagamento;
            StatusPagamento = statusPagamento;
        }
    }
}
