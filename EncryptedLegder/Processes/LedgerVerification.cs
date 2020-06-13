using EncryptedLegder.Abstractions;
using EncryptedLegder.Models;

namespace EncryptedLegder.Processes
{
    internal class LedgerVerification<TransactioneeId> :ILedgerVerification<TransactioneeId>
    {
        private readonly IDigitalSigning digitalSigning;
        private readonly ICryptography cryptography;

        public LedgerVerification(IDigitalSigning digitalSigning, ICryptography cryptography)
        {
            this.digitalSigning = digitalSigning;
            this.cryptography = cryptography;
        }
        public bool VerifyEntry(EncryptedLedgerEntry encryptedLedgerEntry)
        {
            var decrypted = cryptography.Decrypt<TransactioneeId>(encryptedLedgerEntry);
            var result = digitalSigning.VerifySignature(decrypted, encryptedLedgerEntry.Signature);
            return result;
        }
    }
}
