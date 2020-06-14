using EncryptedLegder.Models;

namespace EncryptedLegder.Abstractions
{
    public interface ICryptography
    {
        string Encrypt(string plainValue);
        string Decrypt(string cipherValue);
        EncryptedLedgerEntry<TrsanctioneeIdType> Encrypt<TrsanctioneeIdType>(LedgerEntry<TrsanctioneeIdType> ledgerEntry);
        LedgerEntry<TrsanctioneeIdType> Decrypt<TrsanctioneeIdType>(EncryptedLedgerEntry<TrsanctioneeIdType> ledgerEntry, out bool verificationFlag);
    }
}
