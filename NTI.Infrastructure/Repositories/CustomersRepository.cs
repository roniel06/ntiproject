using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Customers;
using NTI.Application.Interfaces.Repositories;
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
    }
}