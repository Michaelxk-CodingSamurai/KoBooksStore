using KoBooksStoreWeb.Data;
using KoBooksStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace KoBooksStoreWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly KoBooksStoreDbContext _db;

        public CartController(KoBooksStoreDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            List<Book> books = _db.Books.ToList();
            List<Author> authors = _db.Authors.ToList();
            List<Genre> genres = _db.Genres.ToList();
            List<CartItem> cartItems = _db.CartItems.ToList();

            var carItemJoinedBook = from c in cartItems
                                    join bk in books on c.BookIDRef equals bk.BookID into table1
                                    from bk in table1.DefaultIfEmpty().ToList()
                                    join au in authors on bk.AuthorIDRef equals au.AuthorID into table2
                                    from au in table2.DefaultIfEmpty().ToList()
                                    join g in genres on bk.GenreIDRef equals g.GenreID into table3
                                    from g in table3.DefaultIfEmpty().ToList()
                                    select new ViewModel
                                    {
                                        cartitem = c,
                                        book = bk,
                                        author = au,
                                        genre = g
                                    };
            return View(carItemJoinedBook);
        }

        public IActionResult Delete(int? cartId)
        {
            var obj = _db.CartItems.Find(cartId);
            if (obj == null)
            {
                return NotFound();
            }

            _db.CartItems.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Cart Item deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
