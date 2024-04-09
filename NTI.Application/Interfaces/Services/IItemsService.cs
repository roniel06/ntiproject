using NTI.Application.Dtos;
using NTI.Application.InputModels.Items;

namespace NTI.Application.Interfaces.Services
{
    public interface IItemsService
    {
        Task<OperationResult<ItemDto>> CreateAsync(ItemInputModel item);
        Task<OperationResult<ItemDto>> UpdateAsync(ItemInputModel item);
        Task<OperationResult<ItemDto>> GetByIdAsync(int id);
        Task<OperationResult<ItemDto>> GetByItemNumberAsync(int itemNumber);
        Task<OperationResult<IEnumerable<ItemDto>>> GetAllAsync();
    }
}