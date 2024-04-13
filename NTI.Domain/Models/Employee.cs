
using NTI.Domain.Models.Core;

namespace NTI.Domain.Models
{
    public class Employee : BaseModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }
}