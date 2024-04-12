using NTI.Application.Dtos;
using NTI.Application.InputModels.CustomerItems;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.Interfaces.Services;

namespace NTI.Application.Services
{
    public class CustomerItemsService : ICustomerItemService
    {
        private readonly ICustomerItemsRepository _repository;
        public CustomerItemsService(ICustomerItemsRepository repository)
        {
            _repository = repository;
        }
        public async Task<OperationResult<CustomerItemsDto>> CreateAsync(CustomerItemInputModel inputModel)
        {
            var opResult = OperationResult<CustomerItemsDto>.Failed();
            if (inputModel is not null)
            {
                return await _repository.CreateAsync(inputModel);
            }
            return opResult.AddError("The input value should not be null");
        }

        public async Task<OperationResult> DeleteAsync(int id)
         => await _repository.SoftDeleteByIdAsync(id);


        public Task<OperationResult<IEnumerable<CustomerItemsDto>>> GetAllAsync()
         => _repository.GetAllAsync();

        public async Task<OperationResult<IEnumerable<CustomerItemsDto>>> GetByCustomerIdAsync(int customerId)
        {
            return await _repository.GetByCustomerIdAsync(customerId);
        }

        public async Task<OperationResult<CustomerItemsDto>> GetByIdAsync(int id)
        {
            var opResult = OperationResult<CustomerItemsDto>.Failed();
            if (id > 0)
            {
                return await _repository.GetByIdAsync(id);
            }
            return opResult.AddError("The id should be greater than 0");
        }

        public Task<OperationResult<IEnumerable<CustomerItemsDto>>> GetByItemNumberRange(int from, int to)
        {
            return _repository.GetByItemNumberRange(from, to);
        }

        public async Task<OperationResult<CustomerItemsDto>> UpdateAsync(int id, CustomerItemInputModel inputModel)
        {
            var opResult = OperationResult<CustomerItemsDto>.Failed();
            if (id > 0 && inputModel is not null)
            {
                inputModel.Id ??= id;
                return await _repository.EditAsync(id, inputModel);
            }
            return opResult.AddError("The id should be greater than 0 and the input value should not be null");
        }
    }
}