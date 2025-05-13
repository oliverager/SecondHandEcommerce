using Application.Interfaces;
using Application.Queries.Orders;
using Domain.Entities;


namespace Application.Handlers.QueryHandlers;

//ceff6245-6b3a-4bab-ae43-a1124a5e95fd

public class GetOrderByIdQueryHandler
{
    private readonly IOrderRepository _repo;
    private readonly ICacheService _cache; 
    public GetOrderByIdQueryHandler(IOrderRepository repo, ICacheService cache)
    {
        _repo = repo;
        _cache = cache; // Inject the CacheService
    }

    public async Task<Order?> HandleAsync(GetOrderByIdQuery query)
    {
        string cacheKey = $"order:{query.Id}"; // Define a cache key for the order

        // Check if the order is cached
        var cachedOrder = await _cache.GetAsync<Order>(cacheKey);
        if (cachedOrder != null)
        {
            return cachedOrder; // Return the cached order if available
        }

        // If not cached, get it from the repository
        var order = await _repo.GetByIdAsync(query.Id);
        if (order != null)
        {
            // Cache the order for future use (set the expiration as needed)
            await _cache.SetAsync(cacheKey, order, TimeSpan.FromMinutes(10));
        }

        return order;
    }
}
