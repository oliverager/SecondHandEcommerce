using Application.Interfaces;
using Domain.Entities;
using SecondHandEcommerce.Application.Queries.Listings;

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
