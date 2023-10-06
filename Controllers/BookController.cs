using Library.Collections;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

public class BookController : Controller
{
    private readonly ILogger<AuthorController> _logger;
    private readonly BookCollection _collection;
    
    public BookController(ILogger<AuthorController> logger)
    {
        _logger = logger;
        _collection = new BookCollection();
    }
    
    // GET
    public IActionResult Index()
    {
        var books = _collection.GetBooks();
        var authors = new AuthorCollection().GetAuthors();
    
        var authorMap = authors.ToDictionary(author => author.Id.ToString(), author => author.Name + " " + author.PaternalSurname + " " + author.MaternalSurname);

        ViewBag.AuthorMap = authorMap;
    
        return View(books);
    }
    
    public IActionResult Create()
    {
        var authors = new AuthorCollection().GetAuthors();
        var authorsList = authors.Select(a => new 
        { 
            Id = a.Id, 
            FullName = $"{a.Name} {a.PaternalSurname} {a.MaternalSurname}" 
        }).ToList();
        ViewBag.Authors = authorsList;
        
        int currentYear = DateTime.Now.Year;
        List<int> years = Enumerable.Range(1900, currentYear - 1900 + 1).ToList();
        ViewBag.Years = years;
        
        return View();
    }
    
    public IActionResult Edit(string id)
    {
        var authors = new AuthorCollection().GetAuthors();
        var authorsList = authors.Select(a => new 
        { 
            Id = a.Id, 
            FullName = $"{a.Name} {a.PaternalSurname} {a.MaternalSurname}" 
        }).ToList();
        ViewBag.Authors = authorsList;
        var book = _collection.GetBook(id);
        
        int currentYear = DateTime.Now.Year;
        List<int> years = Enumerable.Range(1900, currentYear - 1900 + 1).ToList();
        ViewBag.Years = years;
        
        return View(book);
    }
    
    // HTTP METHODS
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookModel book)
    {
        if (!ModelState.IsValid) return View(book);
        _collection.InsertBook(book);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(string id, BookModel book)
    {
        if (!ModelState.IsValid) return View(book);
        _collection.UpdateBook(id, book);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(string id)
    {
        _collection.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}