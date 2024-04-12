using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Customers;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.OperationResultDtos;
using NTI.Domain.Models;
using NTI.Infrastructure.Context;
using NTI.Infrastructure.Repositories.Core;

namespace NTI.Infrastructure.Repositories
{
    public class CustomersRepository : Repository<Customer, CustomerDto, CustomerInputModel>, ICustomersRepository
    {
        public CustomersRepository(ProjectDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OperationResult<IEnumerable<CustomerWithExpensiveItemDto>>> GetByCustomersWithExpensiveItems()
        {
            var opResult = OperationResult<IEnumerable<CustomerWithExpensiveItemDto>>.Failed();
            var result = await GetQueryable()
                         .Include(x => x.CustomerItems)
                             .ThenInclude(x => x.Item)
                        .ToListAsync();
            var customerWithExpensiveItemDtos = result.Select(x => new CustomerWithExpensiveItemDto
            {
                CustomerId = x.Id,
                CustomerName = x.Name,
                CustomerLastName = x.LastName,
                CustomerItemId = x.CustomerItems.OrderByDescending(x => x.Item.DefaultPrice).FirstOrDefault().Id,
                ItemDescription = x.CustomerItems.OrderByDescending(x => x.Item.DefaultPrice).FirstOrDefault().Item.Description,
                Price = x.CustomerItems.OrderByDescending(x => x.Item.DefaultPrice).FirstOrDefault().Price == 0 ? x.CustomerItems.OrderByDescending(x => x.Item.DefaultPrice).FirstOrDefault().Item.DefaultPrice : x.CustomerItems.OrderByDescending(x => x.Item.DefaultPrice).FirstOrDefault().Price,
                Quantity = x.CustomerItems.OrderByDescending(x => x.Item.DefaultPrice).FirstOrDefault().Quantity
            });
            return opResult.SetSucceeded(customerWithExpensiveItemDtos);
        }

    }
}
