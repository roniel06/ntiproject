using NTI.Application.Dtos.Core;

namespace NTI.Application.Dtos
{
    public class EmployeeDto : BaseDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }

}