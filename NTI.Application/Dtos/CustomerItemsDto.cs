using NTI.Application.Dtos.Core;

namespace NTI.Application.Dtos
{
    public class CustomerItemsDto : BaseDto
    {
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public CustomerDto Customer { get; set; }
        public ItemDto Item { get; set; }
    }
}