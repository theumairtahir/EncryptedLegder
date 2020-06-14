using EncryptedLegder.Abstractions;
using System;

namespace EncryptedLegder.Internal
{
    internal class PersonBalanceQuery<TransactioneeIdType> : IPersonBalanceQuery<TransactioneeIdType>
    {
        private readonly string commandFormat;
        private TransactioneeIdType transactionee;
        private DateTime startDate = DateTime.MinValue, endDate = DateTime.MaxValue;
        public PersonBalanceQuery(string commandFormat)
        {
            this.commandFormat = commandFormat;
        }
        public IPersonBalanceQuery<TransactioneeIdType> DateFrom(DateTime date)
        {
            startDate = date;
            return this;
        }

        public IPersonBalanceQuery<TransactioneeIdType> DateTo(DateTime date)
        {
            endDate = date;
            return this;
        }

        public string GetCommand()
        {
            var query = string.Format(commandFormat, transactionee, startDate.ToShortDateString(), endDate.ToShortDateString());
            return query;
        }

        public IPersonBalanceQuery<TransactioneeIdType> Person(TransactioneeIdType transactionee)
        {
            this.transactionee = transactionee;
            return this;
        }
    }
}
