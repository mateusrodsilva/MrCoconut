using MrCoconut.WebApp.Models;

namespace MrCoconut.WebApp.Infra.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Login(string username, string password);
        Task<string> CreateUser(User newUser);
        Task<User> GetByEmail(string email);
    }
}
