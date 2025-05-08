using Application.Commands.Orders;
using Application.Interfaces;

namespace Application.Handlers.CommandHandlers;

public class CreateOrderCommandHandler
{
    private readonly IOrderService _orderService;

    public CreateOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<string> HandleAsync(CreateOrderCommand command)
    {
        return await _orderService.PlaceOrderAsync(command.BuyerId, command.ItemId);
    }
}

