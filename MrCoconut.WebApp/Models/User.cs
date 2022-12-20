using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MrCoconut.WebApp.Models.Enum;
using Flunt.Validations;
using Flunt.Notifications;

namespace MrCoconut.WebApp.Models
{
    public class User : EntityBase
    {
        [BsonRequired]
        public string Name { get; set; } = string.Empty;
        [BsonRequired]
        public string Email { get; set; } = string.Empty;
        [BsonRequired]
        public string Password { get; set; } = string.Empty;
        public UserTypeEnum UserType { get; set; } = UserTypeEnum.User;

        public User(string name, string email, string password)
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsNotNull(name, "Name", "The name field must be filled.")
                .IsEmail(email,"Email", "Incorrect email format")
                .IsGreaterThan(password, 8, "Password", "Password must be 8 characters or more")
                );

            if(IsValid)
            {
                Name = name;
                Email = email;
                Password = password;
                CreationDate = DateTime.UtcNow;
            }
        }
    }
}
