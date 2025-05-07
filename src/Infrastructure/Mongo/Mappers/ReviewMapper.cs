using Domain.Entities;
using Infrastructure.Mongo.Models;

namespace Infrastructure.Mongo.Mappers;

public static class ReviewMapper
{
    public static ReviewDocument ToDocument(Review review) => new()
    {
        Id = review.Id,
        Text = review.Text,
        Rating = review.Rating,
        SellerId = review.SellerId
    };

    public static Review ToDomain(ReviewDocument doc) => new()
    {
        Id = doc.Id,
        Text = doc.Text,
        Rating = doc.Rating,
        SellerId = doc.SellerId
    };
}
