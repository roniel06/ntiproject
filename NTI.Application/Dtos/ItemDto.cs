using NTI.Application.Dtos.Core;
using NTI.Domain.Enums;

namespace NTI.Application.Dtos
{
    public class ItemDto : BaseDto
    {
        public int ItemNumber { get; set; }
        public string? Description { get; set; }
        public decimal DefaultPrice { get; set; }
        public ItemCategory Category { get; set; }
        public bool IsActive { get; set; }
    }
}