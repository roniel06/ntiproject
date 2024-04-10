
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Items;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.OperationResultDtos;
using NTI.Domain.Models;
using NTI.Infrastructure.Context;
using NTI.Infrastructure.Repositories.Core;

namespace NTI.Infrastructure.Repositories
{
    public class ItemsRepository : Repository<Item, ItemDto, ItemInputModel>, IItemRepository
    {
        private readonly IMapper _mapper;
        public ItemsRepository(ProjectDbContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public async Task<ItemDto?> GetItemByItemNumber(int itemNumber)
        {
            return await GetQueryable().ProjectTo<ItemDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.ItemNumber == itemNumber);
        }
    }
}