using EncryptedLegder.Abstractions;
using System;

namespace EncryptedLegder.Internal
{
    internal class EntriesBetweenQuery<TransactioneeIdType> : IEnteriesBetweenQuery<TransactioneeIdType>
    {
        private readonly string commandFormat;
        private DateTime startDate = DateTime.MinValue, endDate = DateTime.MaxValue;
        private TransactioneeIdType transactionee;
        public EntriesBetweenQuery(string commandFormat)
        {
            this.commandFormat = commandFormat;
        }
        public IEnteriesBetweenQuery<TransactioneeIdType> DateStartsFrom(DateTime date)
        {
            startDate = date;
            return this;
        }
        public IEnteriesBetweenQuery<TransactioneeIdType> ToEndDate(DateTime date)
        {
            endDate = date;
            return this;
        }
        public string GetCommand()
        {
            var cmd = string.Format(commandFormat, transactionee, startDate.ToShortDateString(), endDate.ToShortDateString());
            return cmd;
        }

        public IEnteriesBetweenQuery<TransactioneeIdType> Person(TransactioneeIdType transactionee)
        {
            this.transactionee = transactionee;
            return this;
        }
    }
}
