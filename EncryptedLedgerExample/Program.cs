using Autofac;
using EncryptedLegder.Injections;
using EncryptedLegder.Models;
using EncryptedLegder.Processes;
using EncryptedLegder.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EncryptedLedgerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContext dbContext = new DbContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\New folder\\Visual Studio 2019\\projects\\EncryptedLegder\\EncryptedLedgerExample\\Database.mdf\";Integrated Security=True");
            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception)
            {
            }
            var container = new ProgramInjection("786", "1234567890123456").Inject();
            using var scope = container.BeginLifetimeScope();
            var transaction = scope.Resolve<ILedgerTransaction<int>>();
            var crypto = scope.Resolve<ICryptography>();
            var crud = scope.Resolve<ILedgerCrud<int>>();
            //Transaction Code
            //var enc = transaction.DoATransactionOf(1000)
            //            .From(new Transactionee { PreviousBalance = 4000, PrimaryKey = 1 })
            //            .To(new Transactionee { PrimaryKey = 2, PreviousBalance = 8000 })
            //            .On(DateTime.Now)
            //            .DueTo("Other reason")
            //            .With("abc")
            //            .And("xyz")
            //            .And("xyz2")
            //            .And("xyz3")
            //            .Done();
            //enc.ForEach((x) =>
            //{
            //    crud.CreateEntry(x);
            //});
            var enc = crud.ReadEntry(2, out bool isVerified);
            foreach (var line in enc.ToString().Split(","))
            {
                Console.WriteLine(line);
            }
            foreach (var line in enc.ToString().Split(","))
            {
                Console.WriteLine(line);
            }
            Console.WriteLine($"verified: {isVerified}");
            Console.ReadLine();
        }
    }
    class Model : LedgerEntry<int>
    {
        public Currencies Currency { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal ExchangedDebit
        {
            get
            {
                return ExchangeRate * Debit;
            }
        }
        public decimal ExchangedCredit
        {
            get
            {
                return ExchangeRate * Credit;
            }
        }
        public decimal ExchangedBalance
        {
            get
            {
                return ExchangeRate * Balance;
            }
        }
    }
    enum Currencies
    {
        Pkr,
        Dollar
    }
    public class LedgerCRUD : ILedgerCrud<int>
    {
        private readonly DbContext DbContext = new DbContext();
        private readonly ICryptography cryptography;

        public LedgerCRUD(ICryptography cryptography)
        {
            this.cryptography = cryptography;
        }
        public long CreateEntry(EncryptedLedgerEntry<int> ledgerEntry)
        {
            var table = Map(ledgerEntry);
            var result = DbContext
                            .Table
                            .Add(table)
                            .Entity
                            .Id;
            DbContext.SaveChanges();
            return result;
        }

        public bool DeleteEntry(long primaryKey)
        {
            DbContext.Remove(DbContext
                            .Table
                            .Where(x => x.Id == primaryKey)
                            .Single());
            var result = DbContext.SaveChanges();
            return (result > 0);
        }

        public List<LedgerEntry<int>> ExecuteQuery(ILedgerQuery query, out bool isVerified)
        {
            isVerified = false;
            var result = DbContext.Table.FromSqlRaw(query.GetCommand()).ToList();
            List<LedgerEntry<int>> ledgerEntries = new List<LedgerEntry<int>>();
            foreach (var item in result)
            {
                ledgerEntries.Add(cryptography.Decrypt(Map(item), out isVerified));
            }
            return ledgerEntries;
        }

        public LedgerEntry<int> ReadEntry(long primaryKey, out bool isVerified)
        {
            var result = DbContext.Table
                                  .Where(x => x.Id == primaryKey)
                                  .Single();
            var decrypted = cryptography.Decrypt(Map(result), out isVerified);
            return decrypted;
        }
        private EncryptedLedgerEntry<int> Map(Table table)
        {
            var ledgerEntry = new EncryptedLedgerEntry<int>();
            foreach (var prop in typeof(EncryptedLedgerEntry<int>).GetProperties())
            {
                try
                {
                    var ledgerProp = typeof(Table)
                                .GetProperties()
                                .Where(x => x.Name == prop.Name)
                                .Single();
                    prop.SetValue(ledgerEntry,
                        Convert.ChangeType(ledgerProp.GetValue(table),
                        prop.PropertyType));
                }
                catch (InvalidOperationException)
                {

                }
            }
            ledgerEntry.PrimaryKey = table.Id;
            return ledgerEntry;
        }
        private Table Map(EncryptedLedgerEntry<int> ledgerEntry)
        {
            var table = new Table();
            foreach (var prop in typeof(Table).GetProperties())
            {
                try
                {
                    var ledgerProp = typeof(EncryptedLedgerEntry<int>)
                               .GetProperties()
                               .Where(x => x.Name == prop.Name)
                               .Single();
                    prop.SetValue(table,
                        Convert.ChangeType(ledgerProp.GetValue(ledgerEntry),
                        ledgerProp.PropertyType));
                }
                catch (InvalidOperationException)
                {

                }
            }
            table.Id = ledgerEntry.PrimaryKey;
            return table;
        }
    }
    class Transactionee : ITransactionee<int>
    {
        public int PrimaryKey { get; set; }

        public decimal PreviousBalance { get; set; }
    }

    public class ProgramInjection : LedgerInjection<int>
    {
        public ProgramInjection(string salt, string encryptionKey) : base(salt, encryptionKey)
        {
        }

        public override void RegisterInjections()
        {
            base.RegisterInjections();
            RegisterLedgerCRUD<LedgerCRUD>(@"SELECT * FROM TABLE
                                                WHERE TransactioneeId={0} 
                                                AND TransactionDateTime BETWEEN '{1}' AND '{2}'",
                                                @"SELECT MAX(Balance) FROM TABLE
                                                    WHERE TransactioneeId={0}
                                                    AND TransactionDateTime BETWEEN '{1}' AND '{2}'");
            builder.RegisterType<LedgerCRUD>().As<ILedgerCrud<int>>();
        }
    }
}
