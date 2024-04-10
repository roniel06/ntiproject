using NTI.Domain.Models.Core;

namespace NTI.Domain.Models;

public class Customer : BaseModel
{
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<CustomerItem>? CustomerItems { get; set; }
}

