using Application.Commands.Orders;
using Application.Interfaces;

namespace Application.Handlers.CommandHandlers;

public class CancelOrderCommandHandler
{
    private readonly IOrderService _orderService;

    public CancelOrderCommandHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task HandleAsync(CancelOrderCommand command)
    {
        await _orderService.CancelOrderAsync(command.OrderId, command.UserId);
    }
}