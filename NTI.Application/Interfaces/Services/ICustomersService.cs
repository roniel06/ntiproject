using NTI.Application.Dtos;
using NTI.Application.InputModels.Customers;

namespace NTI.Application.Interfaces.Services
{
    public interface ICustomersService
    {
        Task<OperationResult<CustomerDto>> CreateAsync(CustomerInputModel inputModel);
        Task<OperationResult<CustomerDto>> UpdateAsync(int id, CustomerInputModel inputModel);
        Task<OperationResult<CustomerDto>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<CustomerDto>>> GetAllAsync();
        Task<OperationResult> DeleteAsync(int id);
    }
}