using KoBooksStoreWeb.Data;
using KoBooksStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace KoBooksStoreWeb.Controllers
{
    public class AuthorController : Controller
    {
        private readonly KoBooksStoreDbContext _db;

        public AuthorController(KoBooksStoreDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Author> objAuthorList = _db.Authors;
            return View(objAuthorList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author obj)
        {
            if (ModelState.IsValid)
            {
                _db.Authors.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Author created successfully";
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
            var authorFromDb = _db.Authors.Find(id);

            if (authorFromDb == null)
            {
                return NotFound();
            }

            return View(authorFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author obj)
        {
            if (ModelState.IsValid)
            {
                _db.Authors.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Author updated successfully";
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
            var authorFromDb = _db.Authors.Find(id);

            if (authorFromDb == null)
            {
                return NotFound();
            }

            return View(authorFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Authors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Authors.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Author deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
