using Application.Commands.Orders;
using Application.Handlers.CommandHandlers;
using Application.Handlers.QueryHandlers;
using Application.Queries.Orders;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly CreateOrderCommandHandler _createHandler;
    private readonly GetOrderByIdQueryHandler _getByIdHandler;
    private readonly GetAllOrdersQueryHandler _getAllHandler;

    public OrderController(
        CreateOrderCommandHandler createHandler,
        GetOrderByIdQueryHandler getByIdHandler,
        GetAllOrdersQueryHandler getAllHandler)
    {
        _createHandler = createHandler;
        _getByIdHandler = getByIdHandler;
        _getAllHandler = getAllHandler;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Create(CreateOrderCommand command)
    {
        var id = await _createHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetById(string id)
    {
        var order = await _getByIdHandler.HandleAsync(new GetOrderByIdQuery { Id = id });
        return order is null ? NotFound() : Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAll()
    {
        var orders = await _getAllHandler.HandleAsync(new GetAllOrdersQuery());
        return Ok(orders);
    }
}