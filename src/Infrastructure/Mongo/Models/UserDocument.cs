using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Mongo.Models;

public class UserDocument
{
    [BsonId]
    [BsonElement("_id")]
    public string Id { get; set; } = null!;

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("email")]
    public string Email { get; set; } = null!;

    [BsonElement("rating")]
    public double Rating { get; set; }

    [BsonElement("reviewIds")]
    public List<string> ReviewIds { get; set; } = new();
}
