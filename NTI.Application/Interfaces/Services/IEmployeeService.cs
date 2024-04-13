using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Employee;

namespace NTI.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<OperationResult<EmployeeDto>> CreateAsync(EmployeeInputModel inputModel);
        Task<OperationResult<EmployeeDto>> GetEmployeeByEmailAsync(string email);
        Task<OperationResult<EmployeeDto>> EmployeeCanLoginByEmailAndPassword(string email, string password);
    }
}