using EncryptedLegder.Abstractions;
using EncryptedLegder.Models;
using HashLib;
using System.Text;

namespace EncryptedLegder.Internal
{
    internal class DigitalSigning : IDigitalSigning
    {
        private readonly string salt;
        public DigitalSigning(ISaltValue salt)
        {
            this.salt = salt.GetSaltValue();
        }
        public string GetSignature<TrsanctioneeIdType>(LedgerEntry<TrsanctioneeIdType> ledgerEntry)
        {
            var str = ledgerEntry.ToString() + "_" + salt;
            var result = GetHasher().ComputeString(str, Encoding.ASCII);
            var hash = result.ToString();
            return hash;
        }
        public bool VerifySignature<TrsanctioneeIdType>(LedgerEntry<TrsanctioneeIdType> ledgerEntry, string signature)
        {
            var calculatedHash = GetSignature(ledgerEntry);
            return (string.Compare(calculatedHash, signature) == 0);
        }
        private IHash GetHasher()
        {
            IHash hash = HashFactory.Crypto.CreateSHA512();
            return hash;
        }
    }
}
