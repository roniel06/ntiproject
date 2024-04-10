using NTI.Application.InputModels.Core;
using NTI.Domain.Enums;

namespace NTI.Application.InputModels.Items
{
    public class ItemInputModel : IIdAble
    {
        public int? Id { get; set; }
        public int ItemNumber { get; set; }
        public string? Description { get; set; }
        public decimal DefaultPrice { get; set; }
        public ItemCategory Category { get; set; }
        public bool IsActive { get; set; }
    }
}