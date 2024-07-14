using AutoMapper;
using Tech.Challenge4.Domain.Entities;
using Tech.Challenge4.Domain.Models.Customers;

namespace Tech.Challenge4.Domain.AutoMapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerModel, Customer>().ReverseMap();
        }
    }
}
