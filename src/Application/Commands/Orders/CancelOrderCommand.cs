namespace Application.Commands.Orders;

public class CancelOrderCommand
{
    public string OrderId { get; set; } = null!;
    public string UserId { get; set; } = null!;
}
