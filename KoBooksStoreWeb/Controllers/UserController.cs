using KoBooksStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using KoBooksStoreWeb.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KoBooksStoreWeb.Repository;
using System.Security.Cryptography;
using System.Text;

namespace KoBooksStoreWeb.Controllers
{
    public class UserController : Controller
    {

        private readonly KoBooksStoreDbContext _db;

        public UserController(KoBooksStoreDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<UserLogin> objUserList = _db.UserLogins;
            return View(objUserList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserLogin obj)
        {
            if (ModelState.IsValid)
            {
                obj.Passcode = new HashData(obj.Passcode).passcode;
                _db.UserLogins.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "User created successfully";
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
            var userFromDb = _db.UserLogins.Find(id);

            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserLogin obj)
        {
            if (ModelState.IsValid)
            {
                _db.UserLogins.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "User updated successfully";
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
            var userFromDb = _db.UserLogins.Find(id);
            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.UserLogins.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.UserLogins.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "User deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
