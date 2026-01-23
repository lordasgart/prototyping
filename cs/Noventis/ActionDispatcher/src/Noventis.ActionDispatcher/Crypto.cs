using System.Security.Cryptography;
using System.Text;

public static class Crypto
{
    public static string Encrypt(string plainText, string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] iv   = RandomNumberGenerator.GetBytes(16);

        using var keyDerivation = new Rfc2898DeriveBytes(
            password,
            salt,
            100_000,
            HashAlgorithmName.SHA256);

        byte[] key = keyDerivation.GetBytes(32); // 256-bit key

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV  = iv;

        using var ms = new MemoryStream();
        ms.Write(salt);
        ms.Write(iv);

        using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cs, Encoding.UTF8))
        {
            sw.Write(plainText);
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string cipherText, string password)
{
    byte[] data = Convert.FromBase64String(cipherText);

    byte[] salt = data[..16];
    byte[] iv   = data[16..32];
    byte[] cipherBytes = data[32..];

    using var keyDerivation = new Rfc2898DeriveBytes(
        password,
        salt,
        100_000,
        HashAlgorithmName.SHA256);

    byte[] key = keyDerivation.GetBytes(32);

    using var aes = Aes.Create();
    aes.Key = key;
    aes.IV  = iv;

    using var ms = new MemoryStream(cipherBytes);
    using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
    using var sr = new StreamReader(cs, Encoding.UTF8);

    return sr.ReadToEnd();
}

}
