using System.Collections.Generic;
using System.Threading.Tasks;
using KoBooksStoreWeb.Models;


namespace KoBooksStoreWeb.Repository
{
    public interface ILogin
    {
        Task<IEnumerable<UserLogin>> getuser();
        Task<UserLogin> AuthenticateUser(string username, string passcode);
    }
}
