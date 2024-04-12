
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

        /// <summary>
        /// Gets the customer items by the item number
        /// </summary>
        /// <param name="from">from item number</param>
        /// <param name="to">to item number</param>
        /// <returns></returns>
        Task<OperationResult<IEnumerable<CustomerItemsDto>>> GetByItemNumberRange(int from, int to);
    }
}