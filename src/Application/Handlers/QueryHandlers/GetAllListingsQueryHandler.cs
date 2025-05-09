using Application.Interfaces;
using Application.Queries.Listings;
using Domain.Entities;

namespace Application.Handlers.QueryHandlers;

public class GetAllListingsQueryHandler
{
    private readonly IListingRepository _repository;
    private readonly ICacheService _cache;
    private const string CacheKey = "all_listings";

    public GetAllListingsQueryHandler(IListingRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<IEnumerable<Listing>> HandleAsync(GetAllListingsQuery query)
    {
        var cached = await _cache.GetAsync<IEnumerable<Listing>>(CacheKey);
        if (cached != null)
            return cached;

        var listings = await _repository.GetAllAsync();
        await _cache.SetAsync(CacheKey, listings, TimeSpan.FromMinutes(5)); // optional expiry

        return listings;
    }
}
