using Domain.Entities;
using Infrastructure.Mongo.Models;

namespace Infrastructure.Mongo.Mappers;

public static class ListingMapper
{
    public static ListingDocument ToDocument(Listing listing) => new()
    {
        Id = listing.Id,
        Title = listing.Title,
        Description = listing.Description,
        Price = listing.Price,
        SellerId = listing.SellerId,
        Category = listing.Category,
        ImageUrls = listing.ImageUrls,
        Status = listing.Status
    };

    public static Listing ToDomain(ListingDocument doc) => new()
    {
        Id = doc.Id,
        Title = doc.Title,
        Description = doc.Description,
        Price = doc.Price,
        SellerId = doc.SellerId,
        Category = doc.Category,
        ImageUrls = doc.ImageUrls,
        Status = doc.Status
    };
}
