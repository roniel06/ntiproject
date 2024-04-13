using NTI.Application.Dtos;

namespace NTI.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<OperationResult<AuthenticatedEmployeeDto>> AuthenticateAsync(string email, string password);
    }
}