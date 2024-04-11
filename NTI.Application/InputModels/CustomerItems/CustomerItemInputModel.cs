using NTI.Application.InputModels.Core;

namespace NTI.Application.InputModels.CustomerItems
{
    public class CustomerItemInputModel : IIdAble, IActivable
    {
        public int? Id { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}