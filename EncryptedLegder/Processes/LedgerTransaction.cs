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
        private ITransactionee<TransactioneeIdType> person1, person2;
        private DateTime transactionDate;
        private readonly List<string> tags;
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

        public List<EncryptedLedgerEntry<TransactioneeIdType>> Done()
        {
            List<EncryptedLedgerEntry<TransactioneeIdType>> encrypteds = new List<EncryptedLedgerEntry<TransactioneeIdType>>(2);
            person1Entry = new LedgerEntry<TransactioneeIdType>
            {
                Comments = comments,
                Description = description,
                Tag1 = tags[0],
                Tag2 = tags[1],
                Tag3 = tags[2],
                TransactionDateTime = transactionDate,
                TransactioneeId = person1.PrimaryKey,
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
                TransactioneeId = person2.PrimaryKey,
                Debit = transactionAmount,
                Credit = 0,
                Balance = person2.PreviousBalance + transactionAmount
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

        public ILedgerTransaction<TransactioneeIdType> From(ITransactionee<TransactioneeIdType> transactionee)
        {
            person1 = transactionee;
            return this;
        }

        public ILedgerTransaction<TransactioneeIdType> On(DateTime transactioningDate)
        {
            transactionDate = transactioningDate;
            return this;
        }

        public ILedgerTransaction<TransactioneeIdType> To(ITransactionee<TransactioneeIdType> transactionee)
        {
            person2 = transactionee;
            return this;
        }

        public ILedgerTransaction<TransactioneeIdType> With(string comments)
        {
            this.comments = comments;
            return this;
        }

    }
}
