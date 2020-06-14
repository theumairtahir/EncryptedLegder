using System;

namespace EncryptedLegder.Abstractions
{
    public interface IPersonBalance<TransactioneeIdType>
    {
        IPersonBalance<TransactioneeIdType> Person(TransactioneeIdType transactionee);
        decimal Is();
        IPersonBalance<TransactioneeIdType> From(DateTime date);
        IPersonBalance<TransactioneeIdType> To(DateTime date);
    }
}
