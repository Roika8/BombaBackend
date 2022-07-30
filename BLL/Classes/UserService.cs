using BLL.Interfaces;
using DAL.Interfaces;
using DATA;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                return await userRepository.CheckUserExistAsync(email, password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RegisterUser(User userData)
        {
            var isSuccess = false;

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                isSuccess= await userRepository.RegisterUserAsync(userData);
                return isSuccess;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return isSuccess;
            }
        }
        #endregion


    }
}
