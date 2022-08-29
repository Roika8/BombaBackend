using BLL.Interfaces;
using DAL.Interfaces;
using DATA;
using Microsoft.Extensions.DependencyInjection;
using System;
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
        private static bool ValidateUserData(User userData)
        {
            Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(userData.Email);
            return true;
            //return match.Success && userData.Password.Length > 6;
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

        public async Task<bool> RegisterUser(User userData)
        {
            try
            {
                //userData.UserID = Guid.NewGuid();
                IServiceScope scope = _scopeFactory.CreateScope();
                using (scope)
                {
                    IUserRepository userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                    bool userAlreadyExists = await userRepository.CheckEmailExistsAsync(userData.Email);
                    if (userAlreadyExists) throw new Exception("User email already exists");

                    IPortfolioRepository portfolioRepository = scope.ServiceProvider.GetRequiredService<IPortfolioRepository>();

                    if (!ValidateUserData(userData)) throw new Exception("Email or password is not valid");

                    Guid userID = await userRepository.RegisterUserAsync(userData);
                    int portfolioID = await portfolioRepository.CreatePortfolioAsync(userID);
                    return portfolioID != 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


    }
}
