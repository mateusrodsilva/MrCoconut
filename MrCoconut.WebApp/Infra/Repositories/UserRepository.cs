using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MrCoconut.WebApp.Infra.Repositories.Interfaces;
using MrCoconut.WebApp.Models;
using MrCoconut.WebApp.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MrCoconut.WebApp.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        DatabaseConnection database = new DatabaseConnection();

        public UserRepository()
        {
        }

        public async Task<User> Login(string email, string password)
        {
            try
            {
                var userRegistered = await GetByEmail(email);
                if (userRegistered == null)
                {
                    throw new InvalidOperationException("Email invalid!");
                }

                if (!Password.PasswordVerification(password, userRegistered.Password))
                {
                    throw new InvalidOperationException("Password invalid!");
                }

                return userRegistered;
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
