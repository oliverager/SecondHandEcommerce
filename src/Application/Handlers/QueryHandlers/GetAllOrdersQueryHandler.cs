using Application.Interfaces;
using Application.Queries.Orders;
using Domain.Entities;

namespace Application.Handlers.QueryHandlers;

public class GetAllOrdersQueryHandler
{
    private readonly IOrderRepository _repo;

    public GetAllOrdersQueryHandler(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Order>> HandleAsync(GetAllOrdersQuery query)
    {
        return await _repo.GetAllAsync();
    }
}