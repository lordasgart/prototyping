# Crypto

## Usage

```cs
string password = "correct horse battery staple";
string secret   = "Hello Markus 👋";

string encrypted = Crypto.Encrypt(secret, password);
string decrypted = Crypto.Decrypt(encrypted, password);

Console.WriteLine(encrypted);
Console.WriteLine(decrypted);
```
