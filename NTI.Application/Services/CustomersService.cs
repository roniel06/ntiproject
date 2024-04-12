using NTI.Application.Dtos;
using NTI.Application.InputModels.Customers;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.Interfaces.Services;

namespace NTI.Application.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customerRepository;

        public CustomersService(ICustomersRepository customersRepository)
        {
            _customerRepository = customersRepository;
        }
        public async Task<OperationResult<CustomerDto>> CreateAsync(CustomerInputModel inputModel)
        {
            var opResult = OperationResult<CustomerDto>.Failed();
            if (inputModel is not null)
            {
                return await _customerRepository.CreateAsync(inputModel);
            }
            return opResult.AddError("The input value should not be null");
        }

        public async Task<OperationResult> DeleteAsync(int id)
            => await _customerRepository.SoftDeleteByIdAsync(id);

        public Task<OperationResult<IEnumerable<CustomerDto>>> GetAllAsync()
            => _customerRepository.GetAllAsync();

        public async Task<OperationResult<IEnumerable<CustomerWithExpensiveItemDto>>> GetByCustomersWithExpensiveItems()
        {
            return await _customerRepository.GetByCustomersWithExpensiveItems();
        }

        public async Task<OperationResult<CustomerDto>> GetByIdAsync(int id)
        {
            var opResult = OperationResult<CustomerDto>.Failed();
            if (id > 0)
            {
                return await _customerRepository.GetByIdAsync(id);
            }
            return opResult.AddError("The id should be greater than 0");
        }

        public async Task<OperationResult<CustomerDto>> UpdateAsync(int id, CustomerInputModel inputModel)
        {
            var opResult = OperationResult<CustomerDto>.Failed();
            if (id > 0 && inputModel is not null)
            {
                inputModel.Id ??= id;
                return await _customerRepository.EditAsync(id, inputModel);
            }
            return opResult.AddError("The id should be greater than 0 and the input value should not be null");
        }
    }
}