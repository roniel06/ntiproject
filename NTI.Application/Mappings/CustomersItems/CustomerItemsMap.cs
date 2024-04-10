using AutoMapper;
using NTI.Application.Dtos;
using NTI.Domain.Models;

namespace NTI.Application.Mappings.CustomersItems
{
    public class CustomerItemsMap : Profile
    {
        public CustomerItemsMap()
        {
            CreateMap<CustomerItem, CustomerItemsDto>()
            .ReverseMap();
            // CreateMap<CustomerItemsInputModel,CustomerItemsDto>().ReverseMap();
        }
    }
}