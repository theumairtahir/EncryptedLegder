using EncryptedLegder.Abstractions;
using EncryptedLegder.Processes;

namespace EncryptedLegder.Internal
{
    internal class AccountInfo<TransactioneeIdType> : IAccountInfo<TransactioneeIdType>
    {
        private readonly ILedgerInfo<TransactioneeIdType> ledgerInfo;
        private readonly IPersonBalance<TransactioneeIdType> personBalance;

        public AccountInfo(ILedgerInfo<TransactioneeIdType> ledgerInfo, IPersonBalance<TransactioneeIdType> personBalance)
        {
            this.ledgerInfo = ledgerInfo;
            this.personBalance = personBalance;
        }
        public ILedgerInfo<TransactioneeIdType> EntriesOf(ITransactionee<TransactioneeIdType> transactionee)
        {
            ledgerInfo.PersonId = transactionee.PrimaryKey;
            return ledgerInfo;
        }

        public IPersonBalance<TransactioneeIdType> TheBalanceOf(ITransactionee<TransactioneeIdType> transactionee)
        {
            return personBalance.Person(transactionee.PrimaryKey);
        }
    }
}
