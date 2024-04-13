using AutoMapper;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Employee;
using NTI.Application.Interfaces.Repositories;
using NTI.Domain.Models;
using NTI.Infrastructure.Context;
using NTI.Infrastructure.Repositories.Core;

namespace NTI.Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee, EmployeeDto, EmployeeInputModel>, IEmployeeRepository
    {
        public EmployeeRepository(ProjectDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        
    }
}