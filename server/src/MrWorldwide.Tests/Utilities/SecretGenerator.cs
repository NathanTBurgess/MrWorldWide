using System.Security.Cryptography;

namespace MrWorldwide.Tests.Utilities;

public static class SecretGenerator
{
    public static string GenerateSecret(int length = 32)
    {
        var randomBytes = new byte[length];
        RandomNumberGenerator.Fill(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }
}