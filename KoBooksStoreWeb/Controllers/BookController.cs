using KoBooksStoreWeb.Data;
using KoBooksStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Dynamic;


namespace KoBooksStoreWeb.Controllers;

public class BookController : Controller
{
    private readonly KoBooksStoreDbContext _db;

    public BookController(KoBooksStoreDbContext db)
    {
        _db = db;
    }

    private IEnumerable<ViewModel> GetAllBooks()
    {
        List<Book> books = _db.Books.ToList();
        List<Author> authors = _db.Authors.ToList();
        List<Genre> genres = _db.Genres.ToList();

        var booksJoinedAuthorsGenres = from bk in books
                                       join au in authors on bk.AuthorIDRef equals au.AuthorID into table1
                                       from au in table1.DefaultIfEmpty().ToList()
                                       join g in genres on bk.GenreIDRef equals g.GenreID into table2
                                       from g in table2.DefaultIfEmpty().ToList()
                                       select new ViewModel
                                       {
                                           book = bk,
                                           author = au,
                                           genre = g
                                       };
        return booksJoinedAuthorsGenres;
    }

    public IActionResult Index()
    {
        return View(GetAllBooks());
    }

    //GET
    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Book obj)
    {
        if (ModelState.IsValid)
        {
            _db.Books.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Book created successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var bookFromDb = _db.Books.Find(id);

        if (bookFromDb == null)
        {
            return NotFound();
        }

        List<Genre> genres = _db.Genres.ToList();

        if (genres.Any())
        {
            ViewBag.genres = genres;
        }

        var authorFromDb = _db.Authors.Find(bookFromDb.AuthorIDRef);

        ViewBag.authorFullName = authorFromDb?.Forename + " " + authorFromDb?.Surname;

        return View(bookFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Book obj)
    {
        if (ModelState.IsValid)
        {
            _db.Books.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Book updated successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var bookFromDb = _db.Books.Find(id);
        //var bookFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //var bookFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);

        if (bookFromDb == null)
        {
            return NotFound();
        }

        return View(bookFromDb);
    }

    //POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _db.Books.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.Books.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Book deleted successfully";
        return RedirectToAction("Index");

    }

    public IActionResult AssignAuthor(int? bookId)
    {
        var bookFromDb = _db.Books.Find(bookId);

        if (bookFromDb == null)
        {
            return NotFound();
        }

        ViewBag.BookID = bookFromDb.BookID; 

        IEnumerable<Author> objAuthorsList = _db.Authors;

        return View(objAuthorsList);
    }

    public IActionResult AssignAuthorToBook(int? id, int? bookId)
    {
        var bookFromDb = _db.Books.Find(bookId);
        var authorFromDb = _db.Authors.Find(id);

        if (bookFromDb == null || authorFromDb == null)
        {
            return NotFound();
        }

        bookFromDb.AuthorIDRef = authorFromDb.AuthorID; 

        _db.Books.Update(bookFromDb);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult DeleteAuthorFromBook(int? id)
    {
        var bookFromDb = _db.Books.Find(id);

        if (bookFromDb == null)
        {
            return NotFound();
        }

        bookFromDb.AuthorIDRef = 0;

        _db.Books.Update(bookFromDb);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult ShopBookIndex()
    {
        return View(GetAllBooks());
    }

    public IActionResult AddBookToCart(int? bookId)
    {
        if (bookId == null || bookId == 0)
        {
            return NotFound();
        }
        List<CartItem> cartItems = _db.CartItems.ToList();

        var itemAdded = cartItems.Find(x => x.BookIDRef == bookId);

        if (itemAdded == null)
        {
            var newItem = new CartItem();
            newItem.BookIDRef = (int)bookId;
            newItem.Quantity = 1; 


            _db.CartItems.Add(newItem);

        }

        else
        {
            itemAdded.Quantity = itemAdded.Quantity + 1;
            _db.CartItems.Update(itemAdded);

        }

        _db.SaveChanges();

        return RedirectToAction("ShopBookIndex");
    }

}

