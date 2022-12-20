using MongoDB.Driver;
using MrCoconut.WebApp.Models;

namespace MrCoconut.WebApp.Infra
{
    public class DatabaseConnection
    {
        public static string ConnectionString { get; set; } = null!;
        public static string DatabaseName { get; set; } = null!;
        public static bool IsSSL { get; set; }
        private IMongoDatabase _database { get; set; }

        public DatabaseConnection()
        {
            try
            {
                MongoClientSettings setting = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL)
                {
                    setting.SslSettings = new SslSettings
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    };
                }

                var mongoClient = new MongoClient(setting);
                _database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception erro)
            {

                throw new Exception($"Não foi possível conectar ao banco de dados. - {erro.Message}");
            }
        }

        public IMongoCollection<User> User
        {
            get
            {
                return _database.GetCollection<User>("User");
            }
        }
        public IMongoCollection<Post> Post
        {
            get
            {
                return _database.GetCollection<Post>("Post");
            }
        }

    }
}
