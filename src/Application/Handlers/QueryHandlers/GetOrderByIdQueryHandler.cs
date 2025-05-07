using Application.Interfaces;
using Application.Queries.Orders;
using Domain.Entities;

namespace Application.Handlers.QueryHandlers;

public class GetOrderByIdQueryHandler
{
    private readonly IOrderRepository _repo;

    public GetOrderByIdQueryHandler(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<Order?> HandleAsync(GetOrderByIdQuery query)
    {
        return await _repo.GetByIdAsync(query.Id);
    }
}