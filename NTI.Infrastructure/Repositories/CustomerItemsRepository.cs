using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NTI.Application.Dtos;
using NTI.Application.InputModels.CustomerItems;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.OperationResultDtos;
using NTI.Domain.Models;
using NTI.Infrastructure.Context;
using NTI.Infrastructure.Repositories.Core;

namespace NTI.Infrastructure.Repositories
{
    public class CustomerItemsRepository : Repository<CustomerItem, CustomerItemsDto, CustomerItemInputModel>, ICustomerItemsRepository
    {
        private readonly ProjectDbContext _context;
        private readonly IMapper _mapper;
        public CustomerItemsRepository(ProjectDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<CustomerItemsDto>>> GetByCustomerIdAsync(int customerId)
        {
            var opResult = OperationResult<IEnumerable<CustomerItemsDto>>.Failed();
            var customerItemsDto = await _context.CustomerItems
                .Where(ci => ci.CustomerId == customerId)
                .ProjectTo<CustomerItemsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return opResult.SetSucceeded(customerItemsDto);
        }

        public async Task<OperationResult<IEnumerable<CustomerItemsDto>>> GetByItemNumberRange(int from, int to)
        {
            var opResult = OperationResult<IEnumerable<CustomerItemsDto>>.Failed();
            var customerItemsDto = await GetQueryable()
                .Include(x => x.Item)
                .Where(x => x.Item.ItemNumber >= from && x.Item.ItemNumber <= to)
                .ProjectTo<CustomerItemsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return opResult.SetSucceeded(customerItemsDto);
        }
    }
}