using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NTI.Application.Dtos;
using NTI.Application.Interfaces.Services;
using NTI.Application.Options;

namespace NTI.Application.Services
{
    public class AuthenticationService : Interfaces.Services.IAuthenticationService
    {
        private readonly IEmployeeService _employeeService;
        private readonly JwtOptions _jwtOptions;
        public AuthenticationService(IEmployeeService employeeService, IOptions<JwtOptions> jwtOptions)
        {
            _employeeService = employeeService;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<OperationResult<AuthenticatedEmployeeDto>> AuthenticateAsync(string email, string password)
        {
            var opResult = OperationResult<AuthenticatedEmployeeDto>.Failed();

            var employeeResult = await _employeeService.EmployeeCanLoginByEmailAndPassword(email, password);
            if (employeeResult.IsSuccessfulWithNoErrors)
            {
                var employee = employeeResult.Payload;
                var token = GenerateJwtToken(employee.Id.ToString(), employee.Email, _jwtOptions.Key, _jwtOptions.Issuer, _jwtOptions.Audience);
                var authenticatedEmployeeDto = new AuthenticatedEmployeeDto(token, employee);
                return opResult.SetSucceeded(authenticatedEmployeeDto);
            }
            return opResult.AddErrors("Invalid email or password");
        }

        private string GenerateJwtToken(string userId, string email, string key, string issuer, string audience)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email)
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}