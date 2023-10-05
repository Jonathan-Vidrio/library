using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.Models;

public class BookModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    
    [BsonElement("isbn")]
    public string ISBN { get; set; }
    
    [BsonElement("title")]
    public string Title { get; set; }
    
    [BsonElement("author")]
    public ObjectId Author { get; set; }
    
    [BsonElement("editorial")]
    public string Editorial { get; set; }
    
    [BsonElement("publishDate")]
    public DateTime PublishDate { get; set; }
}