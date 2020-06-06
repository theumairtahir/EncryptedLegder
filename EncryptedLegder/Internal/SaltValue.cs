using EncryptedLegder.Abstractions;

namespace EncryptedLegder.Internal
{
    internal class SaltValue : ISaltValue
    {
        private readonly string _salt;
        public SaltValue(string salt)
        {
            _salt = salt;
        }
        public string GetSaltValue()
        {
            return _salt;
        }
    }
}
