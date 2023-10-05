using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.Models;

public class AuthorModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    
    [BsonElement("name")]
    public string Name { get; set; }
    
    [BsonElement("paternalSurname")]
    public string PaternalSurname { get; set; }
    
    [BsonElement("maternalSurname")]
    public string MaternalSurname { get; set; }
    
    [BsonElement("phone")]
    public string Phone { get; set; }
    
    [BsonElement("address")]
    public string Address { get; set; }
}