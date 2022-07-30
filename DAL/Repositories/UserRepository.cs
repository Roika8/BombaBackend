﻿using DAL.Interfaces;
using DATA;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dbContext;

        public UserRepository(DataContext dbContext)
        {
            this._dbContext = dbContext;
        }
        #region Private methoods

        private async Task<bool> RegisterUser(User userData)
        {
            try
            {
                var foundUser = _dbContext.Users.Select(user => user.Email == userData.Email).ToList().FirstOrDefault();

                if (!foundUser)
                {
                    await _dbContext.Users.AddAsync(userData);
                    return _dbContext.SaveChanges() > 0;
                }
                else throw new Exception("User already exists");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
        private User GetUserData(Guid userID)
        {
            try
            {
                User user = _dbContext.Users.Where(user => user.UserID == userID).ToListAsync().Result.FirstOrDefault();
                if (user == null)
                {
                    throw new ArgumentOutOfRangeException("User id could not be found");
                }
                else return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        private bool CheckUserExist(string email, string password)
        {
            try
            {
                User foundUser = _dbContext.Users.Where(user => user.Email == email && user.Password == password)
                                  .ToList().FirstOrDefault();
                
                return foundUser != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public Task<User> GetUserDataAsync(Guid userID)
        {
            try
            {
                return Task.Run(() => GetUserData(userID));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Task<bool> RegisterUserAsync(User userData)
        {
            try
            {
                return Task.Run(() => RegisterUser(userData));
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public Task<bool> CheckUserExistAsync(string email, string password)
        {
            try
            {
                return Task.Run(() => CheckUserExist(email, password));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
