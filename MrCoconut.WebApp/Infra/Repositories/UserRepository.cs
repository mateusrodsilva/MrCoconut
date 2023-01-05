using Flunt.Notifications;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MrCoconut.WebApp.Infra.Repositories.Interfaces;
using MrCoconut.WebApp.Models;
using MrCoconut.WebApp.Utils;
using MrCoconut.WebApp.ViewModels;
using System.Diagnostics.Contracts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MrCoconut.WebApp.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        DatabaseConnection database = new DatabaseConnection();

        public UserRepository()
        {
        }

        public async Task<UserViewModel> Login(string email, string password)
        {
            try
            {
                var userViewModel = new UserViewModel();

                var userRegistered = await GetByEmail(email);
                if (userRegistered == null)
                {
                    userViewModel.Erros.Add("Invalid email!");
                }

                if (!Password.PasswordVerification(password, userRegistered.Password))
                {
                    userViewModel.Erros.Add("Invalid password!");
                }

                userViewModel.Name = userRegistered.Name;
                userViewModel.Email = userRegistered.Email;
                userViewModel.Password = userRegistered.Password;
                userViewModel.UserType = userRegistered.UserType;

                return userViewModel;
            }
            catch (Exception error)
            {
                throw error;
            }
            
        }

        public async Task<string> CreateUser(User newUser)
        {
            try
            {
                if (!newUser.IsValid)
                {
                    return newUser.Notifications.FirstOrDefault().ToString();
                }

                var userExist = GetByEmail(newUser.Email);
                if (userExist.Result != null)
                {
                    return "Email already registered, Inform another email";
                }

                //Encrypting Password
                newUser.Password = Password.Encrypt(newUser.Password);

                await database.User.InsertOneAsync(newUser);
                return "User Created";

            }
            catch (Exception error)
            {
                return $"Error during user creation - {error.Message}";
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            try
            {
                var user = await database.User.Find(x => x.Email == email).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
