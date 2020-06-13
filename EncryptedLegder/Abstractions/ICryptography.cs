using EncryptedLegder.Models;

namespace EncryptedLegder.Abstractions
{
    public interface ICryptography
    {
        string Encrypt(string plainValue);
        string Decrypt(string cipherValue);
        EncryptedLedgerEntry Encrypt<TrsanctioneeIdType>(LedgerEntry<TrsanctioneeIdType> ledgerEntry);
        LedgerEntry<TrsanctioneeIdType> Decrypt<TrsanctioneeIdType>(EncryptedLedgerEntry ledgerEntry, out bool verificationFlag);
    }
}
