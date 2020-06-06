using System;
using System.Collections.Generic;
using System.Text;
using EncryptedLegder.Abstractions;
using EncryptedLegder.Models;
using EncryptedLegder.Models.InternalModels;

namespace EncryptedLegder.Processes
{
    internal class LedgerTransaction<TransactioneeIdType> : ILedgerTransaction<TransactioneeIdType>
    {
        private readonly List<EncryptedLedgerEntry> encryptedLedgerEntries;
        private LedgerEntry<TransactioneeIdType> person1Entry, person2Entery;
        private readonly string salt;
        private decimal transactionAmount;
        private string description, comments;
        private PersonWithBalance<TransactioneeIdType> person1, person2;
        private DateTime transactionDate;
        private List<string> tags;
        public LedgerTransaction(ISaltValue salt)
        {
            this.salt = salt.GetSaltValue();
            encryptedLedgerEntries = new List<EncryptedLedgerEntry>();
            tags = new List<string>(3);
        }
        public ILedgerTransaction<TransactioneeIdType> And(string tag)
        {
            if (tags.Count <= 3)
            {
                tags.Add(tag);
            }
            return this;
        }

        public List<EncryptedLedgerEntry> Done()
        {
            throw new NotImplementedException();
        }

        public ILedgerTransaction<TransactioneeIdType> DoATransactionOf(decimal amount)
        {
            transactionAmount = amount;
            return this;
        }

        public ILedgerTransaction<TransactioneeIdType> DueTo(string reason)
        {
            description = reason;
            return this;
        }

        public ILedgerTransaction<TransactioneeIdType> From(TransactioneeIdType person, decimal balance)
        {
            person1 = new PersonWithBalance<TransactioneeIdType>
            {
                PersonId = person,
                PreviousBalance = balance
            };
            return this;
        }

        public ILedgerTransaction<TransactioneeIdType> On(DateTime transactioningDate)
        {
            transactionDate = transactioningDate;
            return this;
        }

        public ILedgerTransaction<TransactioneeIdType> To(TransactioneeIdType person, decimal balance)
        {
            person2 = new PersonWithBalance<TransactioneeIdType>
            {
                PersonId = person,
                PreviousBalance = balance
            };
            return this;
        }

        public ILedgerTransaction<TransactioneeIdType> With(string comments)
        {
            this.comments = comments;
            return this;
        }

    }
}
