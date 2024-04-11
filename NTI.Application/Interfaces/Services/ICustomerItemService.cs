
using NTI.Application.Dtos;
using NTI.Application.InputModels.CustomerItems;
using NTI.Domain.Models;

namespace NTI.Application.Interfaces.Services
{
    public interface ICustomerItemService
    {
        Task<OperationResult<CustomerItemsDto>> CreateAsync(CustomerItemInputModel inputModel);
        Task<OperationResult<CustomerItemsDto>> UpdateAsync(int id, CustomerItemInputModel inputModel);
        Task<OperationResult<CustomerItemsDto>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<CustomerItemsDto>>> GetAllAsync();
        Task<OperationResult> DeleteAsync(int id);

        Task<OperationResult<IEnumerable<CustomerItemsDto>>> GetByCustomerIdAsync(int customerId);
    }
}