using EncryptedLegder.Models;
using System;
using System.Collections.Generic;

namespace EncryptedLegder.Abstractions
{
    public interface ILedgerInfo<TransactioneeIdType>
    {
        List<LedgerEntry<TransactioneeIdType>> Are();
        ILedgerInfo<TransactioneeIdType> From(DateTime date);
        ILedgerInfo<TransactioneeIdType> To(DateTime date);
    }
}
