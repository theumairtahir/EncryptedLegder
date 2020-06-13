using EncryptedLegder.Models;

namespace EncryptedLegder.Abstractions
{
    internal interface IDigitalSigning
    {
        string GetSignature<TrsanctioneeIdType>(LedgerEntry<TrsanctioneeIdType> ledgerEntry);
        bool VerifySignature<TrsanctioneeIdType>(LedgerEntry<TrsanctioneeIdType> ledgerEntry, string signature);
    }
}
