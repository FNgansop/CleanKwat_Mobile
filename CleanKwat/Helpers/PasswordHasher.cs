using System.Security.Cryptography;
using System.Text;

namespace CleanKwat.Helpers;

public static class PasswordHasher
{
    public static string Hash(string value)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes("CleanKwat::" + value));
        return Convert.ToHexString(bytes);
    }
}
