using NTI.Application.Dtos;
using NTI.Application.InputModels.Employee;
using NTI.Application.Interfaces.Repositories.Core;
using NTI.Domain.Models;

namespace NTI.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee, EmployeeDto, EmployeeInputModel>
    {
        
    }
}