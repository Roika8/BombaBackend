using DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> RegisterUserAsync(User userData);
        Task<User> GetUserDataAsync(Guid userID);
        Task<bool> CheckUserDataExistAsync(string email, string password);
        Task<bool> CheckEmailExistsAsync(string email);
    }
}
