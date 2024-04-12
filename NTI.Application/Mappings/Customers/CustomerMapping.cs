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
            CreateMap<Customer, CustomerWithExpensiveItemDto>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CustomerLastName, opt => opt.MapFrom(src => src.LastName))
                .ReverseMap();
        }
    }
}