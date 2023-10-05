using Library.Configuration;
using Library.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Library.Collections;

public class AuthorCollection : MongoDb
{
    private const string Collection = "author";
    
    public List<AuthorModel> GetAuthors()
    {
        var collection = GetCollection<AuthorModel>(Collection);
        var authors = collection.Find(new BsonDocument()).ToList();
        return authors;
    }

    public AuthorModel GetAuthor(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<AuthorModel>.Filter.Eq(a => a.Id, objectId);
        return GetOne(Collection, filter);
    }
    
    public void InsertAuthor(AuthorModel author)
    {
        Insert(Collection, author);
    }
    
    public void UpdateAuthor(string id, AuthorModel updatedAuthor)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<AuthorModel>.Filter.Eq(a => a.Id, objectId);
        var update = Builders<AuthorModel>.Update
            .Set(a => a.Name, updatedAuthor.Name)
            .Set(a => a.PaternalSurname, updatedAuthor.PaternalSurname)
            .Set(a => a.MaternalSurname, updatedAuthor.MaternalSurname)
            .Set(a => a.Phone, updatedAuthor.Phone)
            .Set(a => a.Address, updatedAuthor.Address)
        ;
        Update(Collection, filter, update);
    }
    
    public void DeleteAuthor(string id)
    {
        ObjectId objectId = new ObjectId(id);
        var filter = Builders<AuthorModel>.Filter.Eq(a => a.Id, objectId);
        Delete(Collection, filter);
    }
}