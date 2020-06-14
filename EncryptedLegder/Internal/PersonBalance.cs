using EncryptedLegder.Abstractions;
using EncryptedLegder.Processes;
using System;
using System.Collections.Generic;

namespace EncryptedLegder.Internal
{
    internal class PersonBalance<TransactioneeIdType> : IPersonBalance<TransactioneeIdType>
    {
        private readonly Abstractions.IPersonBalanceQuery<TransactioneeIdType> query;
        private readonly ILedgerCrud<TransactioneeIdType> ledgerCrud;
        private DateTime startDate = DateTime.MinValue, endDate = DateTime.MaxValue;
        private TransactioneeIdType transactionee;
        public PersonBalance(IPersonBalanceQuery<TransactioneeIdType> query, ILedgerCrud<TransactioneeIdType> ledgerCrud)
        {
            this.query = query;
            this.ledgerCrud = ledgerCrud;
        }

        public IPersonBalance<TransactioneeIdType> From(DateTime date)
        {
            startDate = date;
            return this;
        }

        public decimal Is()
        {
            bool isNotVerified;
            List<Models.LedgerEntry<TransactioneeIdType>> entry;
            do
            {
                entry = ledgerCrud.ExecuteQuery(query, out isNotVerified);
                if (!isNotVerified)
                {
                    ledgerCrud.DeleteEntry(entry[0].PrimaryKey);
                }
            } while (isNotVerified);
            decimal balance = entry[0].Balance;
            return balance;
        }

        public IPersonBalance<TransactioneeIdType> Person(TransactioneeIdType transactionee)
        {
            this.transactionee = transactionee;
            return this;
        }

        public IPersonBalance<TransactioneeIdType> To(DateTime date)
        {
            endDate = date;
            return this;
        }
    }
}
