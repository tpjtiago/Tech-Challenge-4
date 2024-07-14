using AutoMapper;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Domain.Contracts.Services.Coworkings;
using Tech.Challenge4.Domain.Contracts.Services.Salas;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Exceptions;
using Tech.Challenge4.Domain.Models.Sala;

namespace Tech.Challenge4.Application.Services
{
    public class SalaService : ISalaService
    {
        private readonly IMapper _mapper;
        private readonly ICoworkingService _coworkingService;
        private readonly ISalaRepository _salaRepository;

        public SalaService(
            IMapper mapper,
            ICoworkingService coworkingService,
            ISalaRepository salaRepository)
        {
            _mapper = mapper;
            _coworkingService = coworkingService;
            _salaRepository = salaRepository;
        }

        public async Task DeleteById(int salaId)
        {
            var sala = await _salaRepository.GetById(salaId);

            if (sala is null)
            {
                throw new NotFoundException($"Sala não encontrada com id {salaId}");
            }

            await _salaRepository.Delete(sala);
        }

        public async Task<IList<Sala>> GetAll()
        {
            var salas = await _salaRepository.GetAllWithCoworking();

            return salas;
        }

        public async Task<Sala> GetById(int salaId)
        {
            var sala = await _salaRepository.GetByIdWithCoworking(salaId);

            if (sala is null)
            {
                throw new NotFoundException($"Sala não encontrada com id {salaId}");
            }

            return sala;
        }

        public async Task<Sala> Post(SalaModel salaModel)
        {
            var coworking = await _coworkingService.GetById(salaModel.CoworkingId);
            if (coworking is null)
                throw new NotFoundException("O espaço informado não existe!");

            var salaMap = _mapper.Map<Sala>(salaModel);

            var result = await _salaRepository.Post(salaMap);

            return result;
        }

        public async Task<Sala> Put(int salaId, SalaModel salaModel)
        {
            var coworking = await _coworkingService.GetById(salaModel.CoworkingId);
            if (coworking is null)
                throw new NotFoundException("O espaço informado não existe!");

            var sala = await _salaRepository.GetById(salaId);

            if (sala is null)
            {
                throw new NotFoundException($"Sala não encontrada com id {salaId}");
            }

            var salaMap = _mapper.Map(salaModel, sala);

            var result = await _salaRepository.Put(salaMap);
            result.Coworking.Salas = null;

            return result;
        }
    }
}
