using Application.Interfaces;
using Application.Queries.Listings;
using Domain.Entities;

namespace Application.Handlers.QueryHandlers;

public class GetListingByIdQueryHandler
{
    private readonly IListingRepository _repository;

    public GetListingByIdQueryHandler(IListingRepository repository)
    {
        _repository = repository;
    }

    public async Task<Listing?> HandleAsync(GetListingByIdQuery query)
    {
        return await _repository.GetByIdAsync(query.Id);
    }
}
