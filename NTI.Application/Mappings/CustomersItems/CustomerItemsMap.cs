using AutoMapper;
using NTI.Application.Dtos;
using NTI.Application.InputModels.CustomerItems;
using NTI.Domain.Models;

namespace NTI.Application.Mappings.CustomersItems
{
    public class CustomerItemsMap : Profile
    {
        public CustomerItemsMap()
        {
            CreateMap<CustomerItem, CustomerItemsDto>().ReverseMap();
            CreateMap<CustomerItemInputModel, CustomerItemsDto>().ReverseMap();
            CreateMap<CustomerItemsDto, CustomerItem>().ReverseMap();
            CreateMap<CustomerItemInputModel, CustomerItem>().ReverseMap();
        }
    }
}