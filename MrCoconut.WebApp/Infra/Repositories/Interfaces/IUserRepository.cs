using MrCoconut.WebApp.Models;
using MrCoconut.WebApp.ViewModels;

namespace MrCoconut.WebApp.Infra.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserViewModel> Login(string username, string password);
        Task<string> CreateUser(User newUser);
        Task<User> GetByEmail(string email);
    }
}
