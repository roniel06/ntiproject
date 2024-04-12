namespace NTI.Application.Dtos
{
    public class CustomerWithExpensiveItemDto
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerLastName { get; set; }
        public int CustomerItemId { get; set; }
        public string? ItemDescription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}