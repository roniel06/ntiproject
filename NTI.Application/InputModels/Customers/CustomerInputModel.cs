using NTI.Application.InputModels.Core;

namespace NTI.Application.InputModels.Customers
{
    public class CustomerInputModel : IIdAble, IActivable
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
    }
}