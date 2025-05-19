using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class CriptografiaHelper
{
    //  gera a chave AES
    private static readonly string senhaSecreta = "MinhaSenhaSuperSecreta123!";

    // Gera a chave de 32 bytes 
    private static readonly byte[] Key = GenerateKey(senhaSecreta);

   
    private static readonly byte[] IV = new byte[16];

    private static byte[] GenerateKey(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public static string Criptografar(string texto)
    {
        if (string.IsNullOrEmpty(texto))
            return texto;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new())
            {
                using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new(csEncrypt))
                {
                    swEncrypt.Write(texto);
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    public static string Descriptografar(string textoCriptografado)
    {
        if (string.IsNullOrEmpty(textoCriptografado))
            return textoCriptografado;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new(Convert.FromBase64String(textoCriptografado)))
            using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
}
