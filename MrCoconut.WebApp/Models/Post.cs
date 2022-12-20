using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MrCoconut.WebApp.Models
{
    public class Post
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonRequired]
        public string Name { get; set; } = string.Empty;
        [BsonRequired]
        public string Content { get; set; } = string.Empty;
        [BsonRequired]
        public List<string> Ingredients { get; set; } = new List<string> { string.Empty };
        [BsonRequired]
        public string Method { get; set; } = string.Empty;
        public List<Comment> Comments { get; set; } = new List<Comment> { new Comment() };
        public string Image { get; set; } = string.Empty;
        [BsonRequired]
        public string UserId { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
    }

    public class Comment
    {
        public string Username { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreationDate { get; set;}
    }
}
