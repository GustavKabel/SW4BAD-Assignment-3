using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AarhusSpaceProgram.Api.loggingModels;

public class MissionLog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public int MissionId { get; set; }
    public string? Message { get; set; }
    public DateTime Timestamp { get; set; }
}