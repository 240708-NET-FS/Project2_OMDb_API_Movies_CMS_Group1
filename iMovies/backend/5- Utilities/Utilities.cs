using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;

namespace OMDbProject.Utilities;

public static class Hasher
{
    public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
 
        }
    public static string HashPassword(string password, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        using var hmac = new HMACSHA512(saltBytes);
        byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashBytes);
    }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        byte[] saltBytes = Convert.FromBase64String(storedSalt);
        Console.WriteLine("saltBytes: " + saltBytes);
        using var hmac = new HMACSHA512(saltBytes);
        Console.WriteLine("hmac: " + hmac);
        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        Console.WriteLine("computedHash: " + computedHash);
        string computedHashString = Convert.ToBase64String(computedHash);
        Console.WriteLine("computedHashString:" + computedHashString);
        return computedHashString == storedHash;
    }
    

}
