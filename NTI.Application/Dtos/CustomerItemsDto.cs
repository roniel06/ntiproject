using NTI.Application.Dtos.Core;
using NTI.Application.InputModels.Core;

namespace NTI.Application.Dtos
{
    public class CustomerItemsDto : BaseDto, IActivable
    {
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public CustomerDto Customer { get; set; }
        public ItemDto Item { get; set; }
    }
}