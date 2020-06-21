using EncryptedLegder.Abstractions;
using EncryptedLegder.Models;
using EncryptedLegder.Processes;
using System;
using System.Collections.Generic;

namespace EncryptedLegder.Internal
{
    internal class LedgerInfoBuilder<TransactioneeIdType> : ILedgerInfo<TransactioneeIdType>
    {
        private readonly ILedgerCrud<TransactioneeIdType> ledger;
        private readonly IEnteriesBetweenQuery<TransactioneeIdType> query;
        private DateTime startDate = Common.MIN_DATE;
        private DateTime endDate = DateTime.MaxValue;

        public LedgerInfoBuilder(ILedgerCrud<TransactioneeIdType> ledger, IEnteriesBetweenQuery<TransactioneeIdType> query)
        {
            this.ledger = ledger;
            this.query = query;
        }

        public TransactioneeIdType PersonId { get; set; }

        public List<LedgerEntry<TransactioneeIdType>> Are()
        {
            var result = ledger.ExecuteQuery(query
                                            .DateStartsFrom(startDate)
                                            .ToEndDate(endDate), out bool isVerified);
            return isVerified ? result : null;
        }

        public ILedgerInfo<TransactioneeIdType> From(DateTime date)
        {
            startDate = date;
            return this;
        }

        public ILedgerInfo<TransactioneeIdType> To(DateTime date)
        {
            endDate = date;
            return this;
        }
    }
}
