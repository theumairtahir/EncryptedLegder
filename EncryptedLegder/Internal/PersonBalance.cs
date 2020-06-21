using EncryptedLegder.Abstractions;
using EncryptedLegder.Processes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EncryptedLegder.Internal
{
    internal class PersonBalance<TransactioneeIdType> : IPersonBalance<TransactioneeIdType>
    {
        private readonly IPersonBalanceQuery<TransactioneeIdType> query;
        private readonly ILedgerCrud<TransactioneeIdType> ledgerCrud;
        private DateTime startDate = Common.MIN_DATE, endDate = DateTime.MaxValue;
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
            query.Person(transactionee).DateFrom(startDate).DateTo(endDate);
            List<Models.LedgerEntry<TransactioneeIdType>> entry;
            do
            {
                entry = ledgerCrud.ExecuteQuery(query, out isNotVerified)
                                  .OrderByDescending(x => x.TransactionDateTime)
                                  .ToList();
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
