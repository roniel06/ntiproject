namespace NTI.Application.Dtos
{
    public record AuthenticatedEmployeeDto(string Token, EmployeeDto EmployeeDto)
    {
        
    }
}