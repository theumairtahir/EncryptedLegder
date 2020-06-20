using EncryptedLegder.Abstractions;
using EncryptedLegder.Models;
using System;
using System.Collections.Generic;

namespace EncryptedLegder.Processes
{
    public interface ILedgerTransaction<PersonIdType>
    {
        ILedgerTransaction<PersonIdType> DoATransactionOf(decimal amount);
        ILedgerTransaction<PersonIdType> From(ITransactionee<PersonIdType> transactionee);
        ILedgerTransaction<PersonIdType> To(ITransactionee<PersonIdType> transactionee);
        ILedgerTransaction<PersonIdType> On(DateTime transactioningDate);
        ILedgerTransaction<PersonIdType> DueTo(string reason);
        ILedgerTransaction<PersonIdType> With(string comments);
        ILedgerTransaction<PersonIdType> And(string tag);
        List<EncryptedLedgerEntry<PersonIdType>> Done();
    }
    public interface ILedgerVerification<PersonIdType>
    {
        bool VerifyEntry(EncryptedLedgerEntry<PersonIdType> encryptedLedgerEntry);
    }
    public interface ILedgerCrud<PersonIdType>
    {
        long CreateEntry(EncryptedLedgerEntry<PersonIdType> ledgerEntry);
        LedgerEntry<PersonIdType> ReadEntry(long primaryKey, out bool isVerified);
        bool DeleteEntry(long primaryKey);
        List<LedgerEntry<PersonIdType>> ExecuteQuery(ILedgerQuery query, out bool isVerified);
    }
    public interface IAccountInfo<PersonIdType>
    {
        IPersonBalance<PersonIdType> TheBalanceOf(ITransactionee<PersonIdType> transactionee);
        ILedgerInfo<PersonIdType> EntriesOf(ITransactionee<PersonIdType> transactionee);
    }

}
