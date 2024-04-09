using NTI.Domain.Enums;
using NTI.Domain.Models.Core;

namespace NTI.Domain.Models;

public class Item : BaseModel
{
    public int ItemNumber { get; set; }
    public string? Description { get; set; }
    public decimal DefaultPrice { get; set; }
    public ItemCategory Category { get; set; }
    public bool IsActive { get; set; }
}

