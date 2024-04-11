using NTI.Domain.Models.Core;

namespace NTI.Domain.Models;

public class CustomerItem : BaseModel
{

    public int ItemId { get; set; }
    public int CustomerId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public Customer Customer { get; set; }
    public Item Item { get; set; }
}

