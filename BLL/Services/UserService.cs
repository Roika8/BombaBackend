using BLL.Interfaces;
using DAL.Interfaces;
using DATA;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Classes
{
    public class UserService : IUserService
    {

        private readonly IServiceScopeFactory _scopeFactory;

        public UserService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        #region Private methoods
        private static bool ValidateEmailFormat(string email)
        {
            Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }
        #endregion
        #region Public methoods

        public async Task<User> GetUser(Guid userID)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                return await userRepository.GetUserDataAsync(userID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsUserExist(string email, string password)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                return await userRepository.CheckUserDataExistAsync(email, password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> RegisterUser(User user)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
