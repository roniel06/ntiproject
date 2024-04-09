
using AutoMapper;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Items;
using NTI.Application.Interfaces.Repositories;
using NTI.Domain.Models;
using NTI.Infrastructure.Context;
using NTI.Infrastructure.Repositories.Core;

namespace NTI.Infrastructure.Repositories
{
    public class ItemsRepository : Repository<Item, ItemDto, ItemInputModel>, IItemRepository
    {
        public ItemsRepository(ProjectDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}