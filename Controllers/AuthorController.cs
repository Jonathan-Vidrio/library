using Library.Collections;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

public class AuthorController : Controller
{
    private readonly ILogger<AuthorController> _logger;
    private readonly AuthorCollection _collection;
    
    public AuthorController(ILogger<AuthorController> logger)
    {
        _logger = logger;
        _collection = new AuthorCollection();
    }
    
    // GET
    public IActionResult Index(string id)
    {
        var authors = _collection.GetAuthors();
        return View(authors);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    public IActionResult Edit(string id)
    {
        var author = _collection.GetAuthor(id);
        return View(author);
    }
    
    // HTTP METHODS
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AuthorModel author)
    {
        if (!ModelState.IsValid) return View(author);
        _collection.InsertAuthor(author);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(string id, AuthorModel author)
    {
        if (!ModelState.IsValid) return View(author);
        _collection.UpdateAuthor(id, author);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(string id)
    {
        var relatedBooks = new BookCollection().GetBooksByAuthor(id);
        if (relatedBooks.Count > 0)
        {
            ViewBag.Error = "No se puede eliminar un autor que tiene libros asociados";
            return View(nameof(Index), _collection.GetAuthors());
        }
        _collection.DeleteAuthor(id);
        return RedirectToAction(nameof(Index));
    }
}