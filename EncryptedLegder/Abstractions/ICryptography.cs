namespace EncryptedLegder.Abstractions
{
    internal interface ICryptography
    {
        string Encrypt(string plainValue);
        string Decrypt(string cipherValue);
    }
}
