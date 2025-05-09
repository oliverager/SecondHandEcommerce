namespace Application.Commands.Orders;

public class CreateOrderCommand
{
    public string BuyerId { get; set; } = null!;
    public string ItemId { get; set; } = null!;
}
