
namespace MrCoconut.WebApp.Utils
{
    public static class Password
    {
        public static string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool PasswordVerification(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
