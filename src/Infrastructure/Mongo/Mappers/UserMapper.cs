using Domain.Entities;
using Infrastructure.Mongo.Models;

namespace Infrastructure.Mongo.Mappers;

public static class UserMapper
{
    public static UserDocument ToDocument(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Rating = user.Rating,
        ReviewIds = user.ReviewIds
    };

    public static User ToDomain(UserDocument doc) => new()
    {
        Id = doc.Id,
        Name = doc.Name,
        Email = doc.Email,
        Rating = doc.Rating,
        ReviewIds = doc.ReviewIds
    };
}
