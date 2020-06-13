using EncryptedLegder.Models;

namespace EncryptedLegder.Abstractions
{
    internal interface ICryptography
    {
        string Encrypt(string plainValue);
        string Decrypt(string cipherValue);
        EncryptedLedgerEntry Encrypt<TrsanctioneeIdType>(LedgerEntry<TrsanctioneeIdType> ledgerEntry);
        LedgerEntry<TrsanctioneeIdType> Decrypt<TrsanctioneeIdType>(EncryptedLedgerEntry ledgerEntry);
    }
}
