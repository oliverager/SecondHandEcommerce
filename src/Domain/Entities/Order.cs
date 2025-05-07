namespace Domain.Entities;

public enum OrderStatus
{
    Pending,
    Paid,
    Shipped,
    Delivered,
    Cancelled
}

public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string BuyerId { get; set; } = null!;
    public string ItemId { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
}
