using MongoDB.Bson.Serialization.Attributes;
using MrCoconut.WebApp.Models.Enum;

namespace MrCoconut.WebApp.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserTypeEnum UserType { get; set; } = UserTypeEnum.User;
        public List<string> Erros { get; set; } = new List<string>();
    }
}
