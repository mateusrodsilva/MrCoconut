using Flunt.Notifications;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MrCoconut.WebApp.Models
{
    public abstract class EntityBase : Notifiable<Notification>
    {
        public EntityBase()
        {

        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
    }
}
