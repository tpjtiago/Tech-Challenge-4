using AutoMapper;
using FluentValidation;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Domain.Contracts.Services.Customers;
using Tech.Challenge4.Domain.Contracts.Services.Reservas;
using Tech.Challenge4.Domain.Contracts.Services.Salas;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Enumerables;
using Tech.Challenge4.Domain.Models.Reserva;

namespace Tech.Challenge4.Application.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IMapper _mapper;
        private readonly ISalaService _salasService;
        private readonly ICustomerService _customerService;
        private readonly IReservaRepository _reservaRepository;

        public ReservaService(
            IMapper mapper,
            ISalaService salasService,
            ICustomerService customerService,
            IReservaRepository reservaRepository)
        {
            _mapper = mapper;
            _salasService = salasService;
            _customerService = customerService;
            _reservaRepository = reservaRepository;
        }

        public Task<IList<Reserva>> GetAll()
        {
            return _reservaRepository.GetAll();
        }

        public async Task<Reserva> GetById(int reservaId)
        {
            return await _reservaRepository.GetByIdWithCustomerSala(reservaId);
        }

        public async Task<Reserva> Post(Reserva reserva)
        {
            var reservaInserida = await _reservaRepository.Post(reserva);
            reservaInserida.Customer = null;
            reservaInserida.Sala = null;
            return reservaInserida;
        }
        public async Task<Reserva> EfetuarReserva(ReservaModel reservaModel)
        {
            var customer = await _customerService.GetById(reservaModel.CustomerID);
            if (customer == null)
                throw new ValidationException("Cliente inexistente");

            var sala = await _salasService.GetById(reservaModel.SalaID);
            if (sala == null)
                throw new ValidationException("Sala inexistente");

            if (ValidarHoraInicioLocal(reservaModel, sala.Coworking))
                throw new ValidationException("A Hora de Início não pode ser antes da abertura do local");

            if (ValidarHoraFinalLocal(reservaModel, sala.Coworking))
                throw new ValidationException("A Hora Final não pode ser depois do fechamento do local");

            if (await ReservasDisponiveisSala(reservaModel, sala) <= 0)
                throw new ValidationException("A sala está lotada para essa faixa de horário");

            var reserva = _mapper.Map<Reserva>(reservaModel);
            reserva.StatusReserva = StatusReserva.Pendente;
            reserva.StatusPagamento = StatusPagamento.Pendente;
            reserva.Valor = CalcularValorReserva(reserva, sala);

            return await Post(reserva);
        }

        public async Task<int> ReservasDisponiveisSala(ReservaModel reservaModel, Sala sala)
        {
            var reservasAtivas = await _reservaRepository.ReservasSalaFaixaHorario(
                reservaModel.SalaID,
                reservaModel.HoraInicio,
                reservaModel.HoraFinal,
                reservaModel.DataReserva);

            return sala.Capacidade - reservasAtivas;
        }

        public bool ValidarHoraFinalLocal(ReservaModel reservaModel, Coworking coworking)
        {
            return reservaModel.HoraFinal > coworking.HoraFechamento;
        }

        public bool ValidarHoraInicioLocal(ReservaModel reservaModel, Coworking coworking)
        {
            return reservaModel.HoraInicio < coworking.HoraAbertura;
        }

        public decimal CalcularValorReserva(Reserva reserva, Sala sala)
        {
            var horas = reserva.HoraFinal - reserva.HoraInicio;
            var horaCobrada = horas.Hours;
            if (horas.Minutes > 0)
                horaCobrada += 1;

            var valorReserva = horaCobrada * sala.PrecoHora;

            return valorReserva;
        }

        public async Task<bool> CancelReservation(int reservationId)
        {
            var reservation = await _reservaRepository.GetById(reservationId);

            if (reservation is null)
                throw new ValidationException("Reserva não encontrada.");

            if (reservation.StatusReserva == StatusReserva.Cancelada)
                throw new ValidationException("A Reserva já foi cancelada.");

            if (reservation.StatusReserva == StatusReserva.Confirmada)
            {
                if (TimeOnly.FromDateTime(DateTime.Now) >= reservation.HoraInicio)
                {
                    throw new ValidationException("Não é possível realizar cancelamento após o Ínicio da reserva");
                }
            }

            reservation.StatusReserva = StatusReserva.Cancelada;

            await _reservaRepository.Put(reservation);

            return true;
        }

        public async Task<bool> PresentReservation(int reservationId)
        {
            var reservation = await _reservaRepository.GetById(reservationId);

            if (reservation is null)
                throw new ValidationException("Reserva não encontrada.");

            DateTime now = DateTime.Now;
            DateOnly dataAtual = DateOnly.FromDateTime(now);
            TimeOnly horaAtual = TimeOnly.FromDateTime(now);

            if (dataAtual != reservation.DataReserva)
                throw new ValidationException("A Data atual é diferente da reserva.");

            TimeOnly inicioPermitido = reservation.HoraInicio.AddHours(-1);
            TimeOnly fimPermitido = reservation.HoraFinal.AddHours(1);

            if (horaAtual < inicioPermitido || horaAtual > fimPermitido)
                throw new ValidationException("A hora atual não está dentro do intervalo permitido para confirmação da reserva.");

            var reservasCliente = await _reservaRepository.GetAll();
            var reservaClienteFiltro = reservasCliente
                .Where(p => p.CustomerID == reservation.CustomerID && p.DataReserva == reservation.DataReserva);

            foreach (var reserva in reservaClienteFiltro)
            {
                if (reserva.SalaID != reservation.SalaID &&
                    ((horaAtual >= reserva.HoraInicio && horaAtual <= reserva.HoraFinal) ||
                    (reservation.HoraInicio >= reserva.HoraInicio && reservation.HoraInicio <= reserva.HoraFinal) ||
                    (reservation.HoraFinal >= reserva.HoraInicio && reservation.HoraFinal <= reserva.HoraFinal)))
                {
                    throw new ValidationException("O cliente já possui uma reserva confirmada em outro local na mesma faixa de horário.");
                }
            }

            reservation.Comparecimento = true;

            await _reservaRepository.Put(reservation);

            return true;
        }
    }
}
