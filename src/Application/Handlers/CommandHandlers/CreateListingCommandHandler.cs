using Application.Interfaces;
using Domain.Entities;
using SecondHandEcommerce.Application.Commands.Listings;

namespace Application.Handlers.CommandHandlers;

public class CreateListingCommandHandler
{
    private readonly IListingRepository _repository;
    private readonly ICacheService _cache;

    public CreateListingCommandHandler(IListingRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<string> HandleAsync(CreateListingCommand command)
    {
        var listing = new Listing
        {
            Title = command.Title,
            Description = command.Description,
            Price = command.Price,
            SellerId = command.SellerId,
            Category = command.Category,
            ImageUrls = command.ImageUrls,
            Status = ListingStatus.Available
        };

        await _repository.CreateAsync(listing);
        await _cache.RemoveAsync("all_listings");

        return listing.Id;
    }
}
