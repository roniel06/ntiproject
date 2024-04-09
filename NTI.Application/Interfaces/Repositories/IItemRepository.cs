
using NTI.Application.Dtos;
using NTI.Application.InputModels.Items;
using NTI.Application.Interfaces.Repositories.Core;
using NTI.Domain.Models;

namespace NTI.Application.Interfaces.Repositories
{
    public interface IItemRepository: IRepository<Item, ItemDto, ItemInputModel>
    {
        
    }
}