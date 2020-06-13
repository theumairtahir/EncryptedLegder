using EncryptedLegder.Models;
using System;
using System.Collections.Generic;

namespace EncryptedLegder.Processes
{
    public interface ILedgerTransaction<PersonIdType>
    {
        ILedgerTransaction<PersonIdType> DoATransactionOf(decimal amount);
        ILedgerTransaction<PersonIdType> From(PersonIdType person, decimal balance);
        ILedgerTransaction<PersonIdType> To(PersonIdType person, decimal balance);
        ILedgerTransaction<PersonIdType> On(DateTime transactioningDate);
        ILedgerTransaction<PersonIdType> DueTo(string reason);
        ILedgerTransaction<PersonIdType> With(string comments);
        ILedgerTransaction<PersonIdType> And(string tag);
        List<EncryptedLedgerEntry> Done();
    }
    public interface ILedgerVerification<PersonIdType>
    {
        bool VerifyEntry(EncryptedLedgerEntry encryptedLedgerEntry);
    }
}
