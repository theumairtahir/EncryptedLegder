using EncryptedLegder.Abstractions;

namespace EncryptedLegder.Internal
{
    internal class EncryptionKey : IEncryptionKey
    {
        private readonly string encryptionKey;
        public EncryptionKey(string encryptionKey)
        {
            this.encryptionKey = encryptionKey;
        }
        public string GetEncryptionKey()
        {
            return encryptionKey;
        }
    }
}
