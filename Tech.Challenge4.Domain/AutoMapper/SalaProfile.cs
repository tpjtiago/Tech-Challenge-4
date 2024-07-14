using AutoMapper;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Models.Sala;

namespace Tech.Challenge4.Domain.AutoMapper
{
    public class SalaProfile : Profile
    {
        public SalaProfile()
        {
            CreateMap<SalaModel, Sala>().ReverseMap();
        }
    }
}
