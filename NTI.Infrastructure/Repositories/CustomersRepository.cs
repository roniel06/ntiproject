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
            var customers = await GetQueryable()
                             .Include(x => x.CustomerItems)
                                 .ThenInclude(x => x.Item)
                            .ToListAsync();

            var customerWithExpensiveItemDtos = customers.Select(customer =>
            {
                if(customer.CustomerItems == null || customer.CustomerItems.Count() == 0)
                {
                    return null;
                }
                var mostExpensiveItem = customer.CustomerItems.OrderByDescending(x => x.Item.DefaultPrice).FirstOrDefault();

                if (mostExpensiveItem == null)
                {
                    return null;
                }

                return new CustomerWithExpensiveItemDto
                {
                    CustomerId = customer.Id,
                    CustomerName = customer.Name,
                    CustomerLastName = customer.LastName,
                    CustomerItemId = mostExpensiveItem.Id,
                    ItemDescription = mostExpensiveItem.Item?.Description,
                    Price = (mostExpensiveItem.Price == 0 ? mostExpensiveItem.Item?.DefaultPrice : mostExpensiveItem.Price) ?? 0,
                    Quantity = mostExpensiveItem.Quantity
                };
            })
            .Where(x => x != null);

            return opResult.SetSucceeded(customerWithExpensiveItemDtos);
        }

    }
}
