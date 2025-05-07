using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Mongo.Models;

public class ReviewDocument
{
    [BsonId]
    [BsonElement("_id")]
    public string Id { get; set; } = null!;

    [BsonElement("text")]
    public string Text { get; set; } = null!;

    [BsonElement("rating")]
    public double Rating { get; set; }

    [BsonElement("sellerId")]
    public string SellerId { get; set; } = null!;
}
