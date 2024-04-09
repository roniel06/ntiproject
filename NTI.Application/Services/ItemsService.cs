using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Items;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.Interfaces.Services;

namespace NTI.Application.Services
{
    public class ItemsService : IItemsService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        public ItemsService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<ItemDto>> CreateAsync(ItemInputModel item)
        {
            var opResult = OperationResult<ItemDto>.Failed();

            if (item is not null)
            {
                var result = await _itemRepository.CreateAsync(item);
                return result;
            }
            return opResult.AddError("Item cannot be null");
        }

        public Task<OperationResult<IEnumerable<ItemDto>>> GetAllAsync()
            => _itemRepository.GetAllAsync();

        public Task<OperationResult<ItemDto>> GetByIdAsync(int id)
            => _itemRepository.GetByIdAsync(id);

        public async Task<OperationResult<ItemDto>> GetByItemNumberAsync(int itemNumber)
        {
            var opResult = OperationResult<ItemDto>.Failed();
            if (itemNumber > 0)
            {
                var item = await _itemRepository
                                        .GetQueryable()
                                        .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                                        .FirstOrDefaultAsync(x => x.ItemNumber == itemNumber);
                if (item is not null)
                {
                    return opResult.SetSucceeded(item);
                }
            }
            return opResult.AddError("Item not found");
        }

        public async Task<OperationResult<ItemDto>> UpdateAsync(ItemInputModel item)
        {
            var opResult = OperationResult<ItemDto>.Failed();
            if (item is not null)
            {
                return await _itemRepository.EditAsync(item.Id, item);
            }
            return opResult.AddError("Item cannot be null");
        }
    }
}