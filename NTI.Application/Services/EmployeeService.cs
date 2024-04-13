
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NTI.Application.Dtos;
using NTI.Application.InputModels.Employee;
using NTI.Application.Interfaces.Repositories;
using NTI.Application.Interfaces.Services;
using NTI.Application.Utils;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace NTI.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<OperationResult<EmployeeDto>> CreateAsync(EmployeeInputModel inputModel)
        {
            inputModel.Password = StringUtils.HashPassword(inputModel.Password!);
            var result = await _employeeRepository.CreateAsync(inputModel);
            return result;
        }

        public async Task<OperationResult<EmployeeDto>> EmployeeCanLoginByEmailAndPassword(string email, string password)
        {
            var opResult = OperationResult<EmployeeDto>.Failed();
            var result = await _employeeRepository.GetQueryable()
                .Where(e => e.Email == email && e.PasswordHash == StringUtils.HashPassword(password))
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email
                })
                .FirstOrDefaultAsync();
            if (result == null)
            {
                opResult.AddError("Invalid email or password");
                return opResult;
            }
            return opResult.SetSucceeded(result);

        }

        public async Task<OperationResult<EmployeeDto>> GetEmployeeByEmailAsync(string email)
        {
            var opResult = OperationResult<EmployeeDto>.Failed();
            var data = await _employeeRepository.GetQueryable().FirstOrDefaultAsync(x => x.Email == email);
            if (data == null)
            {
                opResult.AddError("Employee not found");
                return opResult;
            }
            var result = new EmployeeDto
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email
            };
            return opResult.SetSucceeded(result);
        }

      
    }
}