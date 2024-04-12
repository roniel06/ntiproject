using NTI.Application.Dtos;
using NTI.Application.InputModels.CustomerItems;
using NTI.Application.Interfaces.Repositories.Core;
using NTI.Domain.Models;

namespace NTI.Application.Interfaces.Repositories
{
    public interface ICustomerItemsRepository : IRepository<CustomerItem, CustomerItemsDto, CustomerItemInputModel>
    {
        /// <summary>
        /// Gets the customer items by the customer id
        /// </summary>
        /// <param name="customerId">the customer id</param>
        /// <returns></returns>
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