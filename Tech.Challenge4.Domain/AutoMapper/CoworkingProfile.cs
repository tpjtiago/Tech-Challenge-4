using AutoMapper;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Models.Coworking;

namespace Tech.Challenge4.Domain.AutoMapper
{
    public class CoworkingProfile : Profile
    {
        public CoworkingProfile()
        {
            CreateMap<CoworkingModel, Coworking>().ReverseMap();
        }
    }
}
