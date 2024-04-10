using NTI.Application.Dtos.Core;

namespace NTI.Application.Dtos
{
    public class CustomerDto : BaseDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public string? Phone { get; set; }
        public IEnumerable<CustomerItemsDto>? CustomerItems { get; set; }
    }
}