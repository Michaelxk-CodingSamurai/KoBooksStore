using KoBooksStoreWeb.Models;
using KoBooksStoreWeb.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;


namespace KoBooksStoreWeb.Repository
{
    public class AuthenticateLogin : ILogin
    {
        private readonly KoBooksStoreDbContext _context;

        public AuthenticateLogin(KoBooksStoreDbContext context)
        {
            _context = context;
        }
        public async Task<UserLogin> AuthenticateUser(string username, string passcode)
        {
            var succeeded = await _context.UserLogins.FirstOrDefaultAsync(authUser => authUser.UserName == username &&
            authUser.Passcode == new HashData(passcode).passcode);
            return succeeded;
        }

        public async Task<IEnumerable<UserLogin>> getuser()
        {
            return await _context.UserLogins.ToListAsync();
        }

    }
}
