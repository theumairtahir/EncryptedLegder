using System;

namespace EncryptedLegder.Abstractions
{
    internal interface IPersonBalanceQuery<TransactioneeIdType> : ILedgerQuery
    {
        IPersonBalanceQuery<TransactioneeIdType> Person(TransactioneeIdType transactionee);
        IPersonBalanceQuery<TransactioneeIdType> DateFrom(DateTime date);
        IPersonBalanceQuery<TransactioneeIdType> DateTo(DateTime date);
    }
}
