using AutoMapper;
using Tech.Challenge4.Application.Contracts.Repositories;
using Tech.Challenge4.Domain.Contracts.Services.Coworkings;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Exceptions;
using Tech.Challenge4.Domain.Models.Coworking;

namespace Tech.Challenge4.Application.Services
{
    public class CoworkingService : ICoworkingService
    {
        private readonly IMapper _mapper;
        private readonly ICoworkingRepository _coworkingRepository;

        public CoworkingService(
            IMapper mapper,
            ICoworkingRepository coworkingRepository)
        {
            _mapper = mapper;
            _coworkingRepository = coworkingRepository;
        }

        public async Task DeleteById(int coworkingId)
        {
            var coworking = await _coworkingRepository.GetById(coworkingId);

            if (coworking is null)
            {
                throw new NotFoundException($"Coworking não encontrado com id {coworkingId}");
            }

            await _coworkingRepository.Delete(coworking);
        }

        public async Task<IList<Coworking>> GetAll()
        {
            var coworkings = await _coworkingRepository.GetAll();

            return coworkings;
        }

        public async Task<Coworking> GetById(int coworkingId)
        {
            var coworking = await _coworkingRepository.GetByIdWithSalas(coworkingId);

            if (coworking is null)
            {
                throw new NotFoundException($"Coworking não encontrado com id {coworkingId}");
            }

            return coworking;
        }

        public async Task<Coworking> Post(CoworkingModel coworkingModel)
        {
            var coworkingMap = _mapper.Map<Coworking>(coworkingModel);

            var result = await _coworkingRepository.Post(coworkingMap);

            return result;
        }

        public async Task<Coworking> Put(int coworkingId, CoworkingModel coworkingModel)
        {
            var coworking = await _coworkingRepository.GetById(coworkingId);

            if (coworking is null)
            {
                throw new NotFoundException($"Coworking não encontrado com id {coworkingId}");
            }

            var coworkingMap = _mapper.Map(coworkingModel, coworking);

            var result = await _coworkingRepository.Put(coworkingMap);

            return result;
        }
    }
}
