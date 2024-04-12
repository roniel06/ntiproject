using NTI.Application.Dtos;
using NTI.Application.InputModels.Customers;
using NTI.Application.Interfaces.Repositories.Core;
using NTI.Domain.Models;

namespace NTI.Application.Interfaces.Repositories
{
    public interface ICustomersRepository : IRepository<Customer, CustomerDto, CustomerInputModel>
    {
        Task<OperationResult<IEnumerable<CustomerWithExpensiveItemDto>>> GetByCustomersWithExpensiveItems();
    }
}