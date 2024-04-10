using AutoMapper;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Customers;
using NTI.Domain.Models;

namespace NTI.Application.Mappings.Customers
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap()
                .ForMember(dest => dest.CustomerItems, opt => opt.MapFrom(src => src.CustomerItems)); ;
            CreateMap<CustomerInputModel, Customer>().ReverseMap();
            CreateMap<CustomerDto, CustomerInputModel>().ReverseMap();
        }
    }
}