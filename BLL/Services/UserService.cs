﻿using BLL.Interfaces;
using DAL.Interfaces;
using DATA;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private static bool ValidateEmailFormat(string email)
        {
            Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
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
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var existUser = await userManager.GetEmailAsync(userData);

                    if (existUser != null) throw new Exception("User email already exists");

                    IPortfolioRepository portfolioRepository = scope.ServiceProvider.GetRequiredService<IPortfolioRepository>();

                    if (!ValidateEmailFormat(userData.Email))
                        throw new Exception("Email or password is not valid");
                    //TODO Check the password string validation 
                    var result = await userManager.CreateAsync(userData, userData.PasswordHash);
                    if (result.Succeeded)
                    {

                    }
                    else
                    {

                    }
                    //TODO Change it 
                    int portfolioID = await portfolioRepository.CreatePortfolioAsync(new Guid());
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
