using EncryptedLegder.Abstractions;
using EncryptedLegder.Models;
using EncryptedLegder.Models.InternalModels;
using System;
using System.Collections.Generic;

namespace EncryptedLegder.Processes
{
    internal class LedgerTransaction<TransactioneeIdType> : ILedgerTransaction<TransactioneeIdType>
    {
        private LedgerEntry<TransactioneeIdType> person1Entry, person2Entery;
        private readonly ICryptography cryptography;
        private decimal transactionAmount;
        private string description, comments;
        private PersonWithBalance<TransactioneeIdType> person1, person2;
        private DateTime transactionDate;
        private List<string> tags;
        public LedgerTransaction(ICryptography cryptography)
        {
            tags = new List<string>(3);
            this.cryptography = cryptography;
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
            List<EncryptedLedgerEntry> encrypteds = new List<EncryptedLedgerEntry>(2);
            person1Entry = new LedgerEntry<TransactioneeIdType>
            {
                Comments = comments,
                Description = description,
                Tag1 = tags[0],
                Tag2 = tags[1],
                Tag3 = tags[2],
                TransactionDateTime = transactionDate,
                TransactioneeId = person1.PersonId,
                Debit = 0,
                Credit = transactionAmount,
                Balance = person1.PreviousBalance - transactionAmount
            };
            person2Entery = new LedgerEntry<TransactioneeIdType>
            {
                Comments = comments,
                Description = description,
                Tag1 = tags[0],
                Tag2 = tags[1],
                Tag3 = tags[2],
                TransactionDateTime = transactionDate,
                TransactioneeId = person2.PersonId,
                Debit = transactionAmount,
                Credit = 0,
                Balance = person1.PreviousBalance + transactionAmount
            };
            encrypteds.Add(cryptography.Encrypt(person1Entry));
            encrypteds.Add(cryptography.Encrypt(person2Entery));
            return encrypteds;
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
