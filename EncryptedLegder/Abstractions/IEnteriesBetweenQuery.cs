using System;

namespace EncryptedLegder.Abstractions
{
    internal interface IEnteriesBetweenQuery<TransactioneeIdType> : ILedgerQuery
    {
        IEnteriesBetweenQuery<TransactioneeIdType> Person(TransactioneeIdType transactionee);
        IEnteriesBetweenQuery<TransactioneeIdType> DateStartsFrom(DateTime date);
        IEnteriesBetweenQuery<TransactioneeIdType> ToEndDate(DateTime date);
    }
}
