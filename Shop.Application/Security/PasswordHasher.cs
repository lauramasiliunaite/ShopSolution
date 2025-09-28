using System.Security.Cryptography;
using System.Text;

namespace Shop.Application.Security;

public static class PasswordHasher
{
    public static string Hash(string input)
    {
        using var sha = SHA256.Create();
        return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(input)));
    }
}
