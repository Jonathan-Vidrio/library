using Library.Configuration;
using Library.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Library.Collections;

public class BookCollection : MongoDb
{
    private const string Collection = "book";
    
    public List<BookModel> GetBooks()
    {
        var collection = GetCollection<BookModel>(Collection);
        var books = collection.Find(new BsonDocument()).ToList();
        return books;
    }

    public List<BookModel> GetBooksByAuthor(string authorId)
    {
        var collection = GetCollection<BookModel>(Collection);
        var objectId = new ObjectId(authorId);
        var filter = Builders<BookModel>.Filter.Eq(b => b.Author, objectId);
        return collection.Find(filter).ToList();
    }

    public BookModel GetBook(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<BookModel>.Filter.Eq(b => b.Id, objectId);
        return GetOne(Collection, filter);
    }
    
    public void InsertBook(BookModel book)
    {
        Insert(Collection, book);
    }
    
    public void UpdateBook(string id, BookModel updatedBook)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<BookModel>.Filter.Eq(b => b.Id, objectId);
        var update = Builders<BookModel>.Update
            .Set(b => b.Title, updatedBook.Title)
            .Set(b => b.Author, updatedBook.Author)
            .Set(b => b.Editorial, updatedBook.Editorial)
            .Set(b => b.PublishDate, updatedBook.PublishDate)
        ;
        Update(Collection, filter, update);
    }
    
    public void Delete(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<BookModel>.Filter.Eq(b => b.Id, objectId);
        Delete(Collection, filter);
    }
}