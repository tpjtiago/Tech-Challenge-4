using AutoMapper;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Models.Reserva;

namespace Tech.Challenge4.Domain.AutoMapper
{
    public class ReservaProfile : Profile
    {
        public ReservaProfile()
        {
            CreateMap<ReservaModel, Reserva>().ReverseMap();
        }
    }
}
