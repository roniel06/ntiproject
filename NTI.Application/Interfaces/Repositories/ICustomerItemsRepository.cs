using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NTI.Application.Dtos;
using NTI.Application.InputModels.CustomerItems;
using NTI.Application.Interfaces.Repositories.Core;
using NTI.Domain.Models;

namespace NTI.Application.Interfaces.Repositories
{
    public interface ICustomerItemsRepository : IRepository<CustomerItem, CustomerItemsDto, CustomerItemInputModel>
    {
        Task<OperationResult<IEnumerable<CustomerItemsDto>>> GetByCustomerIdAsync(int customerId);
    }
}