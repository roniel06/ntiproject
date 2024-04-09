using AutoMapper;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Items;
using NTI.Domain.Models;

namespace NTI.Application.Mappings.Items
{
    public class ItemMapping : Profile
    {
        public ItemMapping()
        {
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<ItemInputModel, Item>().ReverseMap();
        }
    }
}