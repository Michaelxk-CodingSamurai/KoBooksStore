using KoBooksStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KoBooksStoreWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KoBooksStoreWeb.Data;




namespace KoBooksStoreWeb.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly ILogin _loginUser;
        private readonly IConfiguration _config;
        private readonly KoBooksStoreDbContext _db;

        public UserLoginController(ILogin loguser, IConfiguration config, KoBooksStoreDbContext db)
        {
            _loginUser = loguser;
            _config = config;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string passcode)
        {
            var issuccess = _loginUser.AuthenticateUser(username, passcode);


            if (issuccess.Result != null)
            {
                var userFromDb = _db.UserLogins.Find(username);
                var token = GenerateToken(userFromDb);

                ViewBag.username = string.Format("Successfully logged-in", username);

                TempData["username"] = "Michael";
                Ok(token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.username = string.Format("Login Failed ", username);
                return View();
            }
        }

        // To generate token
        private string GenerateToken(UserLogin user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                //new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
