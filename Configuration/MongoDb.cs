using MongoDB.Driver;

namespace Library.Configuration;

public class MongoDb
{
    private readonly MongoClient _client;
    private IMongoDatabase _database;

    protected MongoDb()
    {
        _client = new MongoClient("mongodb+srv://admin:admin@clusterprueba.q4e8yuc.mongodb.net/?retryWrites=true&w=majority");
        _database = _client.GetDatabase("library");
    }
    
    // GET ALL
    protected IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
    
    // GET ONE
    protected T GetOne<T>(string collectionName, FilterDefinition<T> filter)
    {
        var collection = GetCollection<T>(collectionName);
        return collection.Find(filter).FirstOrDefault();
    }

    
    // POST
    protected void Insert<T>(string collectionName, T document)
    {
        var collection = GetCollection<T>(collectionName);
        collection.InsertOne(document);
    }
    
    // PUT
    protected UpdateResult Update<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update)
    {
        var collection = GetCollection<T>(collectionName);
        return collection.UpdateOne(filter, update);
    }

    // DELETE
    protected DeleteResult Delete<T>(string collectionName, FilterDefinition<T> filter)
    {
        var collection = GetCollection<T>(collectionName);
        return collection.DeleteOne(filter);
    }
    
}