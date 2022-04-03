using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoExample.Models;

public class Locations {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string locationName { get; set; } = null!;
    public double xCoord { get; set; } = 0;
    public double yCoord { get; set; } = 0;

    

    /*[BsonElement("items")]
    [JsonPropertyName("items")]
    public List<string> areaIds { get; set; } = null!;
    */
}