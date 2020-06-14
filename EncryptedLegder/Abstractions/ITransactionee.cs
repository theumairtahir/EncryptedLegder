namespace EncryptedLegder.Abstractions
{
    public interface ITransactionee<TransactioneeIdType>
    {
        TransactioneeIdType PrimaryKey { get; }
        decimal PreviousBalance { get; }
    }
}
