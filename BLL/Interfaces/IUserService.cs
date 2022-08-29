using DATA;
using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUser(User user);
        Task<bool> IsUserExist(string email, string password);
        Task<User> GetUser(Guid userID);
    }
}
