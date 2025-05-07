using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Mongo.Models;

public class ListingDocument
{
    [BsonId]
    [BsonElement("_id")]
    public string Id { get; set; } = null!;

    [BsonElement("title")]
    public string Title { get; set; } = null!;

    [BsonElement("description")]
    public string Description { get; set; } = null!;

    [BsonElement("price")]
    public decimal Price { get; set; }

    [BsonElement("sellerId")]
    public string SellerId { get; set; } = null!;

    [BsonElement("category")]
    public List<string> Category { get; set; } = new();

    [BsonElement("imageUrls")]
    public List<string> ImageUrls { get; set; } = new();

    [BsonElement("status")]
    public string Status { get; set; } = "Available"; // Stored as string
}
